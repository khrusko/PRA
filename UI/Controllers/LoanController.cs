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
    private const Int32 PAGE_SIZE = 8;

    private readonly IBookManager _bookManager = new BookManager();
    private readonly IAuthorManager _authorManager = new AuthorManager();
    private readonly ILoanManager _loanManager = new LoanManager();
    private readonly IPublisherManager _publisherManager = new PublisherManager();
    private readonly IBookStoreManager _bookStoreManager = new BookStoreManager();
    private readonly IUserManager _userManager = new UserManager();

    [HttpGet]
    [UserAuthenticate]
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
    [UserAuthenticate]
    public ActionResult Loan(LoanBookVM model)
    {
      BookProjection book = _bookManager.GetByID(ID: model.BookID);
      if (book is null)
        return new HttpStatusCodeResult(404);

      if (!ModelState.IsValid)
        return Loan(model.BookID);

      if (!model.AcceptRules)
      {
        Message = new AlertMessage(message: "Molimo Vas da prihvatite pravila korištenja usluge i naplate zakasnina");
        return Loan(model.BookID);
      }

      Int32 loanCount = _loanManager.GetActiveByUserFK(LoggedInUser.ID).Count();

      if (loanCount == 3)
      {
        Message = new InfoMessage(message: "Imate posuđene 3 knjige, vratite jednu kako biste mogli posuditi još");
        return Loan(model.BookID);
      }

      return RedirectToAction(actionName: "Pay",
                              controllerName: "PayPal",
                              routeValues: new
                              {
                                id = model.BookID,
                                loanDays = model.LoanDays,
                                operation = "Loan"
                              });
    }

    [HttpGet]
    [UserAuthorize]
    public ActionResult Return(Int32 id)
    {
      Int32 deletedCount = _loanManager.Return(ID: id, updatedBy: LoggedInUser.ID);
      if (deletedCount == 0)
        return new HttpStatusCodeResult(404);

      Message = new InfoMessage(message: "Posuđena knjiga je vraćena");
      return RedirectToAction(actionName: "Index", controllerName: "Dashboard");
    }

    [HttpGet]
    [UserAuthenticate]
    public ActionResult History(LoanSortType loanSortType = 0,
                                SortDirection sortDirection = 0,
                                Int32 page = 1)
    {
      IEnumerable<LoanVM> loans = LoggedInUser.IsAdmin
        ? (from loan in _loanManager.GetAll()
           join book in _bookManager.GetAll(availabilityCheck: false)
             on loan.BookFK equals book.ID
           join user in _userManager.GetAll(availabilityCheck: false)
             on loan.UserFK equals user.ID
           where loan.ReturnDate != DateTime.MinValue
           select new LoanVM
           {
             Book = book,
             Loan = loan,
             User = user
           })
        : (from loan in _loanManager.GetByUserFK(userFK: LoggedInUser.ID)
           join book in _bookManager.GetAll(availabilityCheck: false)
             on loan.BookFK equals book.ID
           where loan.ReturnDate != DateTime.MinValue
           select new LoanVM
           {
             Book = book,
             Loan = loan,
             User = LoggedInUser
           });

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

      return View(viewName: nameof(History),
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
                    LoanSortType = loanSortType,
                    SortDirection = sortDirection
                  });
    }

    [HttpGet]
    [UserAuthenticate]
    public ActionResult Details(Int32 id = 0)
    {
      LoanProjection loan = _loanManager.GetByID(ID: id);

      return loan is null || (!LoggedInUser.IsAdmin && loan.UserFK != LoggedInUser.ID)
        ? new HttpStatusCodeResult(404)
        : (ActionResult)View(viewName: nameof(Details),
                                 model: new LoanVM
                                 {
                                   Loan = loan,
                                   User = LoggedInUser.IsAdmin ? _userManager.GetByID(ID: loan.UserFK, availabilityCheck: false) : LoggedInUser,
                                   Book = _bookManager.GetByID(ID: loan.BookFK, availabilityCheck: false)
                                 });
    }

    [HttpGet]
    public EmptyResult CheckTimeout()
    {
      var list = from loan in _loanManager.GetLoansInTimeout()
                 join book in _bookManager.GetAll()
                   on loan.BookFK equals book.ID
                 join user in _userManager.GetAll()
                   on loan.UserFK equals user.ID
                 select new
                 {
                   loanID = loan.ID,
                   Book = book,
                   User = user
                 };

      foreach (var element in list)
      {
        try
        {
          if (element.Book.Quantity > 0)
          {
            _loanManager.NotifyTimeout(ID: element.loanID,
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