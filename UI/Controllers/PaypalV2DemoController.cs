using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;
using System.Threading.Tasks;
using PayPalCheckoutSdk.Core;
using PayPalCheckoutSdk.Orders;
using PayPalHttp;
using UI.Controllers;
using UI.Infrastructure;
using BLL.Manager;
using BLL.Projection;
using System.Globalization;

///Reference to PayPalCheckoutSdk.dll is added. This dll is built by downloading checkout .NET SDK from github
namespace Udreka.Controllers
{
  public class PaypalV2DemoController : BaseController
  {
    private readonly BookManager _bookManager = new BookManager();
    #region ClientID_and_secret
    private readonly String _paypalEnvironment = "sandbox";//live
    private readonly String _clientId = "AST-S4Sd3FejsjKBGDr4865GFCXZ535pw0ryxYBosib8sp4TuTAwYYANwbcIEnkAQ5XElz7IWFVb8rDX";
    private readonly String _secret = "EGBWOGkJmJa8YqhFcxeL-mRfO0c3h9C_XcAx-rwTGOhnaopHhrTeWRInedRWjtIiX2f6hV0AFgCaJDnl";

    #endregion

    [UserAuthenticate]
    public ActionResult Index()
    {
      return View();
    }

    public async Task<ActionResult> Paypalvtwo(Int32 id=0, string Cancel = null)
    {
      #region local_variables
      
      //setup paypal environment to save some essential varaibles
      MyPaypalPayment.MyPaypalSetup payPalSetup
          = new MyPaypalPayment.MyPaypalSetup { Environment = _paypalEnvironment, ClientId = _clientId, Secret = _secret };

      //a list if string to collect messages to be displayed to the payer
      List<string> paymentResultList = new List<string>();

      #endregion

      #region check_payment_cancellation

      //check if payer has cancelled the transaction, if yes, do nothing. Let the payer know about his actions
      if (!string.IsNullOrEmpty(Cancel) && Cancel.Trim().ToLower() == "true")
      {
        paymentResultList.Add("Otkazali ste transakciju.");
        return View("Index", paymentResultList);
      }

      #endregion

      payPalSetup.PayerApprovedOrderId = Request["token"];
      string PayerID = Request["PayerID"];

      //when payerID is null it means order is not approved by the payer.            
      if (string.IsNullOrEmpty(PayerID))
      {
        #region order_creation
        //Create order and display it to the payer to approve. 
        //This is the first PayPal screen where payer signin using his PayPal credentials

        try
        {
          //redirect URL. when approved or cancelled on PayPal, PayPal uses this URL to redirect to your app/website.
          payPalSetup.RedirectUrl = Request.Url.Scheme + "://" + Request.Url.Authority + "/PaypalV2Demo/Paypalvtwo?";
          payPalSetup.bookProjection = _bookManager.GetByID(id);
          HttpResponse response = await MyPaypalPayment.createOrder(payPalSetup);

          var statusCode = response.StatusCode;
          Order result = response.Result<Order>();
          Console.WriteLine("Status: {0}", result.Status);
          Console.WriteLine("Order Id: {0}", result.Id);
          Console.WriteLine("Intent: {0}", result.CheckoutPaymentIntent);
          Console.WriteLine("Links:");
          foreach (PayPalCheckoutSdk.Orders.LinkDescription link in result.Links)
          {
            Console.WriteLine("\t{0}: {1}\tCall Type: {2}", link.Rel, link.Href, link.Method);
            if (link.Rel.Trim().ToLower() == "approve")
            {
              payPalSetup.ApproveUrl = link.Href;
            }
          }

          if (!string.IsNullOrEmpty(payPalSetup.ApproveUrl))
            return Redirect(payPalSetup.ApproveUrl);
        }
        catch (Exception ex)
        {
          paymentResultList.Add("Javila se greška u obradi vaše transakcije");
          paymentResultList.Add("Detalji: " + ex.Message);
        }

        #endregion
      }
      else
      {
        #region order_execution

        //this is where actual transaction is carried out
        HttpResponse response = await MyPaypalPayment.captureOrder(payPalSetup);
        try
        {
          var statusCode = response.StatusCode;
          Order result = response.Result<Order>();
          Console.WriteLine("Status: {0}", result.Status);
          Console.WriteLine("Capture Id: {0}", result.Id);

          //update view bag so user/payer gets to know the status
          if (result.Status.Trim().ToUpper() == "COMPLETED")
            paymentResultList.Add("Plaćanje je bilo uspješno. Hvala.");
          paymentResultList.Add("Status: ZAVRŠENO");
          paymentResultList.Add("Identifikator plaćanja: " + result.Id);

          #region null_checks
          if (result.PurchaseUnits != null && result.PurchaseUnits.Count > 0 &&
              result.PurchaseUnits[0].Payments != null && result.PurchaseUnits[0].Payments.Captures != null &&
              result.PurchaseUnits[0].Payments.Captures.Count > 0)
            #endregion
            paymentResultList.Add("Identifikator transakcije: " + result.PurchaseUnits[0].Payments.Captures[0].Id);
        }
        catch (Exception ex)
        {
          paymentResultList.Add("Javila se greška u obradi vaše transakcije");
          paymentResultList.Add("Detalji: " + ex.Message);
        }

        #endregion

      }
      return View("Index", paymentResultList);
    }



    /// <summary>
    /// This is your own custom class. NOT from paypal SDK. Write it the way you want it
    /// </summary>
    public class MyPaypalPayment
    {
      /// <summary>
      /// Initiates Paypal client. Must ensure correct environment.
      /// </summary>
      /// <param name="paypalEnvironment">provide value sandbox for testing,  provide value live for live environments</param>            
      /// <returns>PayPalHttp.HttpClient</returns>
      public static PayPalHttpClient client(MyPaypalSetup paypalEnvironment)
      {
        PayPalEnvironment environment = null;

        if (paypalEnvironment.Environment == "live")
        {
          // Creating a live environment
          environment = new LiveEnvironment(paypalEnvironment.ClientId, paypalEnvironment.Secret);
        }
        else
        {
          // Creating a sandbox environment
          environment = new SandboxEnvironment(paypalEnvironment.ClientId, paypalEnvironment.Secret);
        }

        // Creating a client for the environment
        PayPalHttpClient client = new PayPalHttpClient(environment);
        return client;
      }


      //### Creating an Order
      //This will create an order and print order id for the created order

      public async static Task<HttpResponse> createOrder(MyPaypalSetup paypalSetup)
      {
        HttpResponse response = null;

        try
        {
          // Construct a request object and set desired parameters
          // Here, OrdersCreateRequest() creates a POST request to /v2/checkout/orders
          #region order_creation
          var order = new OrderRequest()
          {
            CheckoutPaymentIntent = "CAPTURE",
            PurchaseUnits = new List<PurchaseUnitRequest>()
                        {
                            new PurchaseUnitRequest()
                            {
                                Items = new List<Item>()
                                {
                                    new Item()
                                    {
                                        Quantity = "1",
                                        Name = "Knjiga",
                                        Description = paypalSetup.bookProjection.Title,
                                        Tax = new PayPalCheckoutSdk.Orders.Money(){ CurrencyCode = "EUR", Value = "0.0" },
                                        UnitAmount = new PayPalCheckoutSdk.Orders.Money(){ CurrencyCode = "EUR", Value = paypalSetup.bookProjection.PurchasePrice.ToString("0.00", CultureInfo.InvariantCulture) }
                                    }
                                },

                                AmountWithBreakdown = new AmountWithBreakdown()
                                {
                                    CurrencyCode = "EUR",
                                    Value = paypalSetup.bookProjection.PurchasePrice.ToString("0.00", CultureInfo.InvariantCulture),

                                    AmountBreakdown = new AmountBreakdown()
                                    {
                                        TaxTotal = new PayPalCheckoutSdk.Orders.Money()
                                        {
                                            CurrencyCode = "EUR",
                                            Value = "0.00"
                                        },
                                        Shipping = new PayPalCheckoutSdk.Orders.Money()
                                        {
                                            CurrencyCode = "EUR",
                                            Value = "0.00"
                                        },
                                        ItemTotal = new PayPalCheckoutSdk.Orders.Money()
                                        {
                                            CurrencyCode = "EUR",
                                            Value = paypalSetup.bookProjection.PurchasePrice.ToString("0.00", CultureInfo.InvariantCulture)
                                        }
                                    }
                                }
                            }
                        },
            ApplicationContext = new ApplicationContext()
            {
              ReturnUrl = paypalSetup.RedirectUrl,
              CancelUrl = paypalSetup.RedirectUrl + "&Cancel=true"
            }
          };

          #endregion

          //IMPORTANT
          ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;


          // Call API with your client and get a response for your call
          var request = new OrdersCreateRequest();
          request.Prefer("return=representation");
          request.RequestBody(order);
          PayPalHttpClient paypalHttpClient = client(paypalSetup);
          response = await paypalHttpClient.Execute(request);

        }
        catch (Exception ex)
        {
          Console.WriteLine("Exception: {0}", ex.Message);
          throw;
        }
        return response;
      }


      //### Capturing an Order
      //Before capturing an order, order should be approved by the buyer using the approve link in create order response

      public async static Task<HttpResponse> captureOrder(MyPaypalSetup paypalSetup)
      {
        // Construct a request object and set desired parameters
        // Replace ORDER-ID with the approved order id from create order
        var request = new OrdersCaptureRequest(paypalSetup.PayerApprovedOrderId);
        request.RequestBody(new OrderActionRequest());
        PayPalHttpClient paypalHttpClient = client(paypalSetup);
        HttpResponse response = await paypalHttpClient.Execute(request);
        return response;
      }

      public class MyPaypalSetup
      {
        /// <summary>
        /// Provide value sandbox for testing,  provide value live for real money
        /// </summary>
        public String Environment { get; set; }
        /// <summary>
        /// Client id as provided by Paypal on dashboard. Ensure you use correct value based on your environment selection
        /// Use sandbox accounts for sandbox testing
        /// </summary>
        public String ClientId { get; set; }
        /// <summary>
        /// Secret as provided by Paypal on dashboard. Ensure you use correct value based on your environment selection
        /// Use sandbox accounts for sandbox testing
        /// </summary>
        public String Secret { get; set; }

        /// <summary>
        /// This is the URL that you will pass to paypal which paypal will use to redirect payer back to your website.
        /// So essentially it is the same controller URL that you must pass
        /// </summary>
        public String RedirectUrl { get; set; }

        /// <summary>
        /// Once order is created on Paypal, it redirects control to your app with a URL that shows order details. Your website must take the payer to this page
        /// so the payer approved the payment. Store this URL in this property
        /// </summary>
        public String ApproveUrl { get; set; }

        /// <summary>
        /// When paypal redirects control to your website it provides a Approved Order ID which we then pass it back to paypal to execute the order.
        /// Store this approved order ID in this property
        /// </summary>
        public String PayerApprovedOrderId { get; set; }

        public BookProjection bookProjection { get; set; }
      }

    }
  }
}