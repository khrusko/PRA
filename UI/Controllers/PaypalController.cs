//using System;
//using System.Collections.Generic;
//using System.Threading.Tasks;
//using System.Web;
//using System.Web.Mvc;

//using PayPalCheckoutSdk.Core;
//using PayPalCheckoutSdk.Orders;

//namespace UI.Controllers
//{
//  public class PaypalController : Controller
//  {
//    private const String PAYPAL_ENVIRONMENT = "sandbox";
//    private const String CLIENT_ID = "AZo9paZVs90AO6dVQA4fiLCQvmk8Y2Uvy5q11grQvLE2GqaDkV71GO7LEzJxs2blduspdL9pO_bB1yyS";
//    private const String SECRET = "ENPa67REEmr5ltkZytesUPHKDsK29PxgyBVY5RDZBCMgg9PaRo3-OV5NIMca00ShodvKjUc8rATuJuAX";

//    public ViewResult Index() => Index();

//    public async Task<ActionResult> Execute(String cancel = null)
//    {
//      return null;
//    }
//  }

//  public class MyPaypalPayment
//  {
//    public static PayPalHttpClient Client(MyPayPalSetup payPalSetup)
//      => new PayPalHttpClient((payPalSetup.Environment == "live")
//          ? new LiveEnvironment(payPalSetup.ClientId, payPalSetup.Secret)
//          : (PayPalEnvironment)new SandboxEnvironment(payPalSetup.ClientId, payPalSetup.Secret));

//    public async static Task<HttpResponse> CreateOrder(MyPayPalSetup payPalSetup)
//    {
//      HttpResponse response = null;

//      try
//      {
//        var order = new OrderRequest
//        {
//          CheckoutPaymentIntent = "CAPTURE",
//          PurchaseUnits = new List<PurchaseUnitRequest>
//          {
//            new PurchaseUnitRequest()
//            {
//              Items = new List<Item>
//              {
//                new Item()
//                {
//                  Quantity = "1",
//                  Name = "Knjiga 1",
//                  Description = "Autor knjige 1",
//                  Sku = "sku",
//                  Tax = new Money
//                  {
//                    CurrencyCode = "HRK",
//                    Value = "0.05",
//                  },
//                  UnitAmount = new Money
//                  {
//                    CurrencyCode = "HRK",
//                    Value = "129.05",
//                  }
//                }
//              }
//            }
//          }
//        };
//      }
//      catch (Exception)
//      {

//        throw;
//      }
//    }
//  }

//  public class MyPayPalSetup
//  {
//    public String Environment { get; set; }
//    public String ClientId { get; set; }
//    public String Secret { get; set; }
//  }
//}