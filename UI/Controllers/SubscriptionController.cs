using System;
using System.Linq;
using System.Web.Mvc;

using BLL.Abstract.Manager.Projection;
using BLL.Manager;

using UI.Infrastructure;
using UI.Models.Abstract;
using UI.Models.Concrete;

namespace UI.Controllers
{
  public class SubscriptionController : BaseController
  {
    private readonly ISubscriptionManager _subscriptionManager = new SubscriptionManager();
    private readonly IBookManager _bookManager = new BookManager();
    private readonly IUserManager _userManager = new UserManager();

    [HttpGet]
    [UserAuthenticate]
    public ActionResult Index(Int32 id)
    {
      Int32 updatedCount = _subscriptionManager.Subscribe(id, LoggedInUser.ID);

      Message = updatedCount == 0
        ? new AlertMessage(message: "Već postoji zahtijev za obavijest Vašeg računa za odabranu knjigu.")
        : (IMessage)new InfoMessage(message: "Obavijest će biti poslana na Vaš Email kad knjiga bude dostupna.");
      return RedirectToAction(actionName: "Details", controllerName: "Book", routeValues: new { id });
    }

    [HttpGet]
    public EmptyResult Resolve()
    {
      var list = from subscription in _subscriptionManager.GetAllUnresolved()
                 join book in _bookManager.GetAll()
                   on subscription.BookFK equals book.ID
                 join user in _userManager.GetAll()
                   on subscription.UserFK equals user.ID
                 select new
                 {
                   SubscriptionID = subscription.ID,
                   Book = book,
                   User = user
                 };

      foreach (var element in list)
      {
        try
        {
          if (element.Book.Quantity > 0)
          {
            _ = _subscriptionManager.Resolve(ID: element.SubscriptionID,
                                             book: element.Book,
                                             user: element.User);
          }
        }
        catch (Exception)
        { }
      }

      return new EmptyResult();
    }
  }
}