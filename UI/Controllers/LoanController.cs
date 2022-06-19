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
  public class LoanController : BaseController
  {
    private readonly IBookManager _bookManager = new BookManager();
    private readonly IAuthorManager _authorManager = new AuthorManager();
    private readonly ILoanManager _loanManager = new LoanManager();
    private readonly IBookStoreManager _bookstoreManager = new BookStoreManager();

    [UserAuthenticate]
    [HttpGet]
    public ActionResult Loan(Int32 id)
    {
      BookProjection book = _bookManager.GetByID(ID: id);
      LoanProjection loan = new LoanProjection();
      return book is null
        ? new HttpStatusCodeResult(404)
        : (ActionResult)View(viewName: nameof(Loan),
                             model: new UserLoanBookVM
                             {
                               Book = book,
                               Loan = null,
                               Bookstore = _bookstoreManager.GetBookStore(),
                               Author = _authorManager.GetByID(ID: book.AuthorFK, availabilityCheck: false)
                             });

    }
  }
}