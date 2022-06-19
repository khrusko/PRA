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
using UI.Models.Enums;


namespace UI.Controllers
{

  public class PurchaseController : BaseController
    {
    private readonly IBookManager _bookManager = new BookManager();
    private readonly IAuthorManager _authorManager = new AuthorManager();
    private readonly IPurchaseManager _purchaseManager = new PurchaseManager();

    [UserAuthenticate]
    [HttpGet]
    public ActionResult Purchase(Int32 id)
    {
      BookProjection book = _bookManager.GetByID(ID: id);
      return book is null
        ? new HttpStatusCodeResult(404)
        : (ActionResult)View(viewName: nameof(Purchase),
                             model: new PurchaseBookVM
                             {
                               Book = book,
                               Purchase = null,
                               Author = _authorManager.GetByID(ID: book.AuthorFK, availabilityCheck: false)
                             });
    }

    [UserAuthenticate]
    [HttpPost]
    public ActionResult Purchase(PurchaseBookVM model)
    {
      if (!ModelState.IsValid)
      {
        var Message = new AlertMessage(message: "Kupnja nije uspjela. Pokušajte ponovno");
        return View(viewName: nameof(Purchase), model: model.Book.ID);
      }

      var buyBook = _purchaseManager.Purchase(model.Book.ID, LoggedInUser.ID, 1);
      if (buyBook == 0) Message = new AlertMessage(message: "Kupnja nije uspjela. Pokušajte ponovno");

      Message = new InfoMessage(message: "Uspješna kupnja provjerite vaš E-mail");
      return View(nameof(Purchase), model: model);
    }

  }
}