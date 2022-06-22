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
    private readonly IUserManager _userManager = new UserManager();
    private readonly ILoanManager _loanManager = new LoanManager();

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
                join user in _userManager.GetAll()
                  on loan.UserFK equals user.ID
                select new LoanVM
                {
                  Loan = loan,
                  Book = book,
                  User = user
                };

        return View(viewName: nameof(Index), model: loans);
      }
      else
      {
        loans = from loan in _loanManager.GetAll()
                join book in _bookManager.GetAll()
                  on loan.BookFK equals book.ID
                join user in _userManager.GetAll()
                  on loan.UserFK equals user.ID
                select new LoanVM
                {
                  Loan = loan,
                  Book = book,
                  User = user
                };

        return View(viewName: nameof(Index), model: loans);
      }
    }
  }
}