using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

using BLL.Abstract.Manager.Projection;
using BLL.Manager;

using UI.Infrastructure;
using UI.Models;
using UI.Models.Enums;

namespace UI.Controllers
{
  [UserAuthenticate]
  public class DashboardController : BaseController
  {
    private const Int32 PAGE_SIZE = 3;

    private readonly IBookManager _bookManager = new BookManager();
    private readonly IPurchaseManager _purchaseManager = new PurchaseManager();
    private readonly IAuthorManager _authorManager = new AuthorManager();
    private readonly IUserManager _userManager = new UserManager();
    private readonly ILoanManager _loanManager = new LoanManager();

    [HttpGet]
    [UserAuthenticate]
    public ActionResult Index(LoanFilterType loanFilterType = 0,
                              LoanSortType loanSortType = 0,
                              SortDirection sortDirection = 0,
                              Int32 page = 1)
    {
      IEnumerable<LoanVM> loans;
      if (LoggedInUser.IsAdmin)
      {
        loans = from loan in _loanManager.GetAll()
                join book in _bookManager.GetAll()
                  on loan.BookFK equals book.ID
                join user in _userManager.GetAll()
                  on loan.UserFK equals user.ID
                where loan.ReturnDate == DateTime.MinValue
                select new LoanVM
                {
                  Loan = loan,
                  Book = book,
                  User = user
                };

        switch (loanFilterType)
        {
          case LoanFilterType.NONE:
            break;
          case LoanFilterType.DELAY_DAYS_SUCCESS:
            loans = loans.Where(predicate: model => (model.Loan.PlannedReturnDate - model.Loan.LoanDate).Days > 3);
            break;
          case LoanFilterType.DELAY_DAYS_WARNING:
            loans = loans.Where(predicate: model => (model.Loan.PlannedReturnDate - model.Loan.LoanDate).Days <= 3 &&
                                                    (model.Loan.PlannedReturnDate - model.Loan.LoanDate).Days > 0);
            break;
          case LoanFilterType.DELAY_DAYS_DANGER:
            loans = loans.Where(predicate: model => (model.Loan.PlannedReturnDate - model.Loan.LoanDate).Days <= 0);
            break;
          default:
            throw new InvalidOperationException();
        }

        switch (loanSortType)
        {
          case LoanSortType.LOAN_DATE:
            loans = loans.SortBy(keySelector: model => model.Loan.LoanDate, sortDirection: sortDirection);
            break;
          case LoanSortType.PLANNED_RETURN_DATE:
            loans = loans.SortBy(keySelector: model => model.Loan.PlannedReturnDate, sortDirection: sortDirection);
            break;
          default:
            throw new InvalidOperationException();
        }
      }
      else
      {
        loans = from loan in _loanManager.GetActiveByUserFK(LoggedInUser.ID)
                join book in _bookManager.GetAll()
                  on loan.BookFK equals book.ID
                select new LoanVM
                {
                  Loan = loan,
                  Book = book,
                  User = LoggedInUser
                };
      }

      return View(viewName: nameof(Index),
                  model: new LoanSearchVM
                  {
                    Loans = loans.Skip(count: (page - 1) * PAGE_SIZE)
                                 .Take(count: PAGE_SIZE),
                    PagingInfo = new PagingInfo
                    {
                      CurrentPage = page,
                      ItemsPerPage = PAGE_SIZE,
                      TotalItems = loans.Count()
                    },
                    LoanFilterType = loanFilterType,
                    LoanSortType = loanSortType,
                    SortDirection = sortDirection
                  });
    }
  }
}