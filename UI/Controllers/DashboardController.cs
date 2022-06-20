using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System;
using BLL.Abstract.Manager.Projection;
using BLL.Manager;

using UI.Infrastructure;
using UI.Models;
namespace UI.Controllers
{
  [UserAuthenticate]
  public class DashboardController : BaseController
  {

    private readonly IBookManager _bookManager = new BookManager();
    private readonly IPurchaseManager _purchaseManager = new PurchaseManager();
    private readonly IAuthorManager _authorManager = new AuthorManager();
    private readonly ILoanManager _loanManager = new LoanManager();

    //    Authorize - IsAdmin
    //Authenticate  - IsLoggedIn

    [HttpGet]
    [UserAuthenticate]
    public ActionResult Index()
    {
      IEnumerable<LoanVM> loans;

      if (!LoggedInUser.IsAdmin)
      {
        loans = from loan in _loanManager.GetActiveByUserFK(LoggedInUser.ID)
                join book in _bookManager.GetAll()
                  on loan.BookFK equals book.ID
                join author in _authorManager.GetAll()
                  on book.AuthorFK equals author.ID
                select new LoanVM
                {
                  Book = book,
                  Author = author,
                  Loan = loan
                };

        return View(viewName: nameof(Index), model: loans);
      }
      else
      {
        loans = from loan in _loanManager.GetAll()
                join book in _bookManager.GetAll()
                  on loan.BookFK equals book.ID
                join author in _authorManager.GetAll()
                  on book.AuthorFK equals author.ID
                select new LoanVM
                {
                  Book = book,
                  Author = author,
                  Loan = loan
                };

        return View(viewName: nameof(Index), model: loans);
      }
    }

    [HttpGet]
    [UserAuthorize]
    public ActionResult LoanHistory()
    {
      IEnumerable<LoanVM> loans = from loan in _loanManager.GetByUserFK(LoggedInUser.ID)
                                      join book in _bookManager.GetAll()
                                        on loan.BookFK equals book.ID
                                      join author in _authorManager.GetAll()
                                        on book.AuthorFK equals author.ID
                                      select new LoanVM
                                      {
                                        Book = book,
                                        Author = author,
                                        Loan = loan
                                      };

      return View("LoanHistory", loans);
    }

    [HttpGet]
    public ActionResult ShoppingHistory()
    {
      IEnumerable<PurchaseVM> purchases = from purchase in _purchaseManager.GetByUserFK(LoggedInUser.ID)
                                              join book in _bookManager.GetAll()
                                                on purchase.BookFK equals book.ID
                                              join author in _authorManager.GetAll()
                                                on book.AuthorFK equals author.ID

                                              select new PurchaseVM
                                              {
                                                Book = book,
                                                Author = author,
                                                Purchase = purchase
                                              };
      return View("ShoppingHistory", purchases);

    }

    [HttpGet]
    [UserAuthorize]
    public ActionResult SalesHistory()
    {
      IEnumerable<PurchaseVM> purchases = from purchase in _purchaseManager.GetAll()
                                              join book in _bookManager.GetAll()
                                                on purchase.BookFK equals book.ID
                                              join author in _authorManager.GetAll()
                                                on book.AuthorFK equals author.ID

                                              select new PurchaseVM
                                              {
                                                Book = book,
                                                Author = author,
                                                Purchase = purchase
                                              };
      return View("SalesHistory", purchases);
    }
  }
}