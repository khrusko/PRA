using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

using BLL.Abstract.Manager.Projection;
using BLL.Manager;
using BLL.Projection;

using PayPalCheckoutSdk.Core;
using PayPalCheckoutSdk.Orders;

using PayPalHttp;

using UI.Controllers;
using UI.Infrastructure;
using UI.Models.Concrete;

namespace Udreka.Controllers
{
  [UserAuthenticate]
  public class PayPalController : BaseController
  {
    private readonly IBookManager _bookManager = new BookManager();
    private readonly IPurchaseManager _purchaseManager = new PurchaseManager();
    private readonly ILoanManager _loanManager = new LoanManager();

    private readonly String _paypalEnvironment = "sandbox";
    private readonly String _clientId = "AZo9paZVs90AO6dVQA4fiLCQvmk8Y2Uvy5q11grQvLE2GqaDkV71GO7LEzJxs2blduspdL9pO_bB1yyS";
    private readonly String _secret = "ENPa67REEmr5ltkZytesUPHKDsK29PxgyBVY5RDZBCMgg9PaRo3-OV5NIMca00ShodvKjUc8rATuJuAX";

    public async Task<ActionResult> Pay(Int32 id = 0, Int32 quantity = 0, Int32 loanDays = 0, String operation = null, String cancel = null)
    {
      var setup = new PayPalPayment.PayPalSetup
      {
        Environment = _paypalEnvironment,
        ClientId = _clientId,
        Secret = _secret
      };

      if (!String.IsNullOrEmpty(cancel) && cancel.Trim().ToLower() == "true")
      {
        Message = new InfoMessage("Otkazali ste transakciju");
        return RedirectToAction(actionName: "Details", controllerName: "Book", routeValues: new { id });
      }

      setup.PayerApprovedOrderId = Request["token"];
      String PayerID = Request["PayerID"];

      if (String.IsNullOrEmpty(PayerID))
      {
        try
        {
          setup.RedirectUrl = Request.Url.Scheme + "://" + Request.Url.Authority + "/PayPal/Pay?";
          setup.Book = _bookManager.GetByID(id);
          setup.Operation = operation;
          setup.Quantity = quantity;
          setup.LoanDays = loanDays;
          HttpResponse response = await PayPalPayment.CreateOrder(setup);

          HttpStatusCode statusCode = response.StatusCode;
          Order result = response.Result<Order>();
          foreach (LinkDescription link in result.Links)
          {
            if (link.Rel.Trim().ToLower() == "approve")
            {
              setup.ApproveUrl = link.Href;
            }
          }

          if (!String.IsNullOrEmpty(setup.ApproveUrl))
            return Redirect(setup.ApproveUrl);
        }
        catch (Exception)
        {
          Message = new AlertMessage(message: "Javila se greška u obradi vaše transakcije");
        }
      }
      else
      {
        HttpResponse response = await PayPalPayment.CaptureOrder(setup);
        try
        {
          HttpStatusCode statusCode = response.StatusCode;
          Order result = response.Result<Order>();

          if (result.Status.Trim().ToUpper() == "COMPLETED")
          {
            if (!(operation is null))
            {
              if (operation == "Purchase")
              {
                _ = _purchaseManager.Purchase(id, LoggedInUser.ID, quantity);
              }
              else if (operation == "Loan")
              {
                _ = _loanManager.Loan(id, LoggedInUser.ID, DateTime.Now.AddDays(loanDays));
              }
            }
            else
            {
              throw new Exception();
            }

            Message = new InfoMessage(message: "Plaćanje je bilo uspješno");
          }
        }
        catch (Exception)
        {
          Message = new AlertMessage(message: "Javila se greška u obradi vaše transakcije");
        }
      }

      return RedirectToAction(actionName: "Details", controllerName: "Book", routeValues: new { id });
    }

    public class PayPalPayment
    {
      public static PayPalHttpClient GetClient(PayPalSetup paypalEnvironment)
        => new PayPalHttpClient((paypalEnvironment.Environment == "live")
          ? new LiveEnvironment(paypalEnvironment.ClientId, paypalEnvironment.Secret)
          : (PayPalEnvironment)new SandboxEnvironment(paypalEnvironment.ClientId, paypalEnvironment.Secret));

      public async static Task<HttpResponse> CreateOrder(PayPalSetup setup)
      {
        Decimal price = (setup.Operation == "Purchase" ? setup.Book.PurchasePrice : setup.Book.LoanPrice) / 7.6M;
        String priceString = setup.Quantity > 1
          ? (price * setup.Quantity).ToString("0.00", CultureInfo.InvariantCulture)
          : price.ToString("0.00", CultureInfo.InvariantCulture);

        var order = new OrderRequest()
        {
          CheckoutPaymentIntent = "CAPTURE",
          PurchaseUnits = new List<PurchaseUnitRequest>
          {
            new PurchaseUnitRequest
            {
              Items = new List<Item>
              {
                new Item
                {
                  Quantity = setup.Quantity > 1
                    ? setup.Quantity.ToString()
                    : "1",
                  Name = setup.Book.Title,
                  Description = $"{setup.Book.Title}",
                  Tax = new Money
                  {
                    CurrencyCode = "EUR",
                    Value = "0.0"
                  },
                  UnitAmount = new Money
                  {
                    CurrencyCode = "EUR",
                    Value = price.ToString("0.00", CultureInfo.InvariantCulture)
                  }
                }
              },
              AmountWithBreakdown = new AmountWithBreakdown
              {
                CurrencyCode = "EUR",
                Value = priceString,
                AmountBreakdown = new AmountBreakdown
                {
                  TaxTotal = new Money
                  {
                    CurrencyCode = "EUR",
                    Value = "0.00"
                  },
                  Shipping = new Money
                  {
                    CurrencyCode = "EUR",
                    Value = "0.00"
                  },
                  ItemTotal = new Money
                  {
                    CurrencyCode = "EUR",
                    Value = priceString
                  }
                }
              }
            }
          },
          ApplicationContext = new ApplicationContext
          {
            ReturnUrl = setup.RedirectUrl + $"&id={setup.Book.ID}&operation={setup.Operation}&{(setup.Operation == "Purchase" ? $"quantity={setup.Quantity}" : $"loanDays={setup.LoanDays}")}",
            CancelUrl = setup.RedirectUrl + $"&id={setup.Book.ID}&cancel=true"
          }
        };

        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

        var request = new OrdersCreateRequest();
        _ = request.Prefer("return=representation");
        _ = request.RequestBody(order);

        PayPalHttpClient paypalHttpClient = GetClient(setup);
        return await paypalHttpClient.Execute(request);
      }

      public async static Task<HttpResponse> CaptureOrder(PayPalSetup setup)
        => await GetClient(setup).Execute(new OrdersCaptureRequest(setup.PayerApprovedOrderId).RequestBody(new OrderActionRequest()));

      public class PayPalSetup
      {
        public String Environment { get; set; }
        public String ClientId { get; set; }
        public String Secret { get; set; }
        public String RedirectUrl { get; set; }
        public String ApproveUrl { get; set; }
        public String PayerApprovedOrderId { get; set; }
        public BookProjection Book { get; set; }
        public Int32 Quantity { get; set; }
        public Int32 LoanDays { get; set; }
        public String Operation { get; set; }
      }
    }
  }
}