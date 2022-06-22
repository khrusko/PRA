using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

using BLL.Abstract.Manager.Projection;
using BLL.Manager;
using BLL.Projection;

using UI.Infrastructure;
using UI.Models;
using UI.Models.Concrete;

namespace UI.Controllers
{

  [UserAuthenticate]
  public class PurchaseController : BaseController
  {
    private readonly IBookManager _bookManager = new BookManager();
    private readonly IAuthorManager _authorManager = new AuthorManager();
    private readonly IPublisherManager _publisherManager = new PublisherManager();
    private readonly IPurchaseManager _purchaseManager = new PurchaseManager();
    private readonly IUserManager _userManager = new UserManager();

    [HttpGet]
    public ActionResult Purchase(Int32 id)
    {
      BookProjection book = _bookManager.GetByID(ID: id);
      return (book is null)
        ? new HttpStatusCodeResult(404)
        : (ActionResult)View(viewName: nameof(Purchase),
                             model: new PurchaseBookVM
                             {
                               BookInfo = new FullBookInfoVM
                               {
                                 Book = book,
                                 Author = _authorManager.GetByID(ID: book.AuthorFK,
                                                                 availabilityCheck: false),
                                 Publisher = _publisherManager.GetByID(ID: book.PublisherFK,
                                                                       availabilityCheck: false)
                               },
                               BookID = book.ID,
                               Quantity = 1
                             });
    }

    [HttpPost]
    public ActionResult Purchase(PurchaseBookVM model)
    {
      BookProjection book = _bookManager.GetByID(ID: model.BookID);
      if (book is null)
        return new HttpStatusCodeResult(404);

      if (!ModelState.IsValid)
      {
        Message = new AlertMessage(message: "Kupnja nije uspjela, pokušajte ponovno");
        return View(viewName: nameof(Purchase),
                    model: new PurchaseBookVM
                    {
                      BookInfo = new FullBookInfoVM
                      {
                        Book = book,
                        Author = _authorManager.GetByID(ID: book.AuthorFK,
                                                        availabilityCheck: false),
                        Publisher = _publisherManager.GetByID(ID: book.PublisherFK,
                                                              availabilityCheck: false)
                      },
                      BookID = model.BookID,
                      Quantity = model.Quantity
                    });
      }

      if (model.Quantity > book.Quantity || model.Quantity <= 0)
      {
        Message = new AlertMessage(message: "Tražena količina nije valjana");
        return Purchase(model.BookID);
      }

      return RedirectToAction(actionName: "Pay",
                              controllerName: "PayPal",
                              routeValues: new
                              {
                                id = model.BookID,
                                quantity = model.Quantity,
                                operation = "Purchase"
                              });
    }

    [HttpGet]
    public ActionResult History()
    {
      IEnumerable<PurchaseVM> purchases = LoggedInUser.IsAdmin
        ? (from purchase in _purchaseManager.GetAll()
           join book in _bookManager.GetAll()
             on purchase.BookFK equals book.ID
           join user in _userManager.GetAll()
             on purchase.BookFK equals user.ID
           select new PurchaseVM
           {
             Purchase = purchase,
             Book = book,
             User = LoggedInUser
           })
        : (from purchase in _purchaseManager.GetByUserFK(userFK: LoggedInUser.ID)
           join book in _bookManager.GetAll()
             on purchase.BookFK equals book.ID
           join user in _userManager.GetAll()
             on purchase.BookFK equals user.ID
           select new PurchaseVM
           {
             Purchase = purchase,
             Book = book,
             User = LoggedInUser
           });

      return View(viewName: nameof(History), model: purchases);
    }

    [HttpGet]
    public ActionResult Details(Int32 id)
    {
      PurchaseProjection purchase = _purchaseManager.GetByID(ID: id);

      return purchase is null || (!LoggedInUser.IsAdmin && purchase.UserFK != LoggedInUser.ID)
        ? new HttpStatusCodeResult(404)
        : (ActionResult)View(viewName: nameof(Details),
                                 model: new PurchaseVM
                                 {
                                   Purchase = purchase,
                                   User = LoggedInUser.IsAdmin ? _userManager.GetByID(ID: purchase.UserFK, availabilityCheck: false) : LoggedInUser,
                                   Book = _bookManager.GetByID(ID: purchase.BookFK, availabilityCheck: false)
                                 });
    }
  }
}