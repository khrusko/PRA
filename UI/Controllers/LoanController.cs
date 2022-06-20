using System;
using System.Web.Mvc;

using BLL.Abstract.Manager.Projection;
using BLL.Manager;
using BLL.Projection;

using UI.Infrastructure;
using UI.Models;

namespace UI.Controllers
{
  [UserAuthenticate]
  public class LoanController : BaseController
  {
    private readonly IBookManager _bookManager = new BookManager();
    private readonly IAuthorManager _authorManager = new AuthorManager();
    private readonly ILoanManager _loanManager = new LoanManager();
    private readonly IPublisherManager _publisherManager = new PublisherManager();
    private readonly IBookStoreManager _bookStoreManager = new BookStoreManager();

    [HttpGet]
    public ActionResult Loan(Int32 id)
    {
      BookProjection book = _bookManager.GetByID(ID: id);
      var loan = new LoanProjection();
      return book is null
        ? new HttpStatusCodeResult(404)
        : (ActionResult)View(viewName: nameof(Loan),
                             model: new LoanBookVM
                             {
                               BookInfo = new FullBookInfoVM
                               {
                                 Book = book,
                                 Author = _authorManager.GetByID(ID: book.AuthorFK),
                                 Publisher = _publisherManager.GetByID(ID: book.PublisherFK)
                               },
                               BookStore = _bookStoreManager.GetBookStore(),
                               BookID = book.ID
                             });
    }

    [HttpPost]
    public ActionResult Loan(LoanBookVM model)
    {
      BookProjection book = _bookManager.GetByID(ID: model.BookID);
      return book is null
        ? new HttpStatusCodeResult(404)
        : !ModelState.IsValid
          ? Loan(model.BookID)
          : RedirectToAction(actionName: "Pay",
                             controllerName: "PayPal",
                             routeValues: new
                             {
                               id = model.BookID,
                               loanDays = model.LoanDays,
                               operation = "Loan"
                             });
    }
  }
}