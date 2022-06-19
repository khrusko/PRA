using System;
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

    [HttpGet]
    public ActionResult Purchase(Int32 id)
    {
      BookProjection book = _bookManager.GetByID(ID: id);
      return (book is null)
        ? new HttpStatusCodeResult(404)
        : (ActionResult)View(viewName: nameof(Purchase),
                             model: new PurchaseBookVM
                             {
                               BookInfo = new FullBookInfoVM {
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

      if (model.Quantity > book.Quantity)
      {
        Message = new AlertMessage(message: "Tražena količina je veća od dostupne količine knjiga");
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
  }
}