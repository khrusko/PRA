using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System;
using BLL.Abstract.Manager.Projection;
using BLL.Manager;
using BLL.Projection;

using UI.Infrastructure;
using UI.Models;
using UI.Models.Concrete;
namespace UI.Controllers
{
  public class DashboardController : BaseController
  {

    private readonly IBookManager _bookManager = new BookManager();
    private readonly IPurchaseManager _purchaseManager = new PurchaseManager();
    private readonly IMessageManager _messageManager = new MessageManager();
    private readonly IAuthorManager _authorManager = new AuthorManager();
    private readonly ILoanManager _loanManager = new LoanManager();
    private readonly IBranchOfficeManager _branchOfficeManager = new BranchOfficeManager();

    //    Authorize - IsAdmin
    //Authenticate  - IsLoggedIn

    [HttpGet]
    [UserAuthenticate]
    public ActionResult Index()
    {
      if (!ModelState.IsValid) return RedirectToAction(actionName: "NotFound", controllerName: "HttpErrors");
      if (LoggedInUser == null) return RedirectToAction(actionName: "NotFound", controllerName: "HttpErrors");
      IEnumerable<LoanBookVM> loans;
      if (!LoggedInUser.IsAdmin)
      {
        loans = from loan in _loanManager.GetActiveByUserFK(LoggedInUser.ID)
                join book in _bookManager.GetAll()
                  on loan.BookFK equals book.ID
                join author in _authorManager.GetAll()
                  on book.AuthorFK equals author.ID

                select new LoanBookVM
                {
                  Book = book,
                  Author = author,
                  Loan = loan
                };
        return View("Index", loans);
      }
      else if (LoggedInUser.IsAdmin)
      {
        loans = from loan in _loanManager.GetAll()
                join book in _bookManager.GetAll()
                  on loan.BookFK equals book.ID
                join author in _authorManager.GetAll()
                  on book.AuthorFK equals author.ID

                select new LoanBookVM
                {
                  Book = book,
                  Author = author,
                  Loan = loan
                };
        return View("Index", loans);
      }
      return RedirectToAction(actionName: "NotFound", controllerName: "HttpErrors");
    }
    [UserAuthenticate]
    public ActionResult LoanHistory()
    {
      IEnumerable<LoanBookVM> loans = from loan in _loanManager.GetByUserFK(LoggedInUser.ID)
                                      join book in _bookManager.GetAll()
                                        on loan.BookFK equals book.ID
                                      join author in _authorManager.GetAll()
                                        on book.AuthorFK equals author.ID

                                      select new LoanBookVM
                                      {
                                        Book = book,
                                        Author = author,
                                        Loan = loan
                                      };
      return View("LoanHistory", loans);
    }

    [UserAuthenticate]
    public ActionResult ShoppingHistory()
    {
      IEnumerable<PurchaseBookVM> purchases = from purchase in _purchaseManager.GetByUserFK(LoggedInUser.ID)
                                              join book in _bookManager.GetAll()
                                                on purchase.BookFK equals book.ID
                                              join author in _authorManager.GetAll()
                                                on book.AuthorFK equals author.ID

                                              select new PurchaseBookVM
                                              {
                                                Book = book,
                                                Author = author,
                                                Purchase = purchase
                                              };
      return View("ShoppingHistory", purchases);

    }
    [HttpGet]
    [UserAuthorize]
    public ActionResult EditBranchOfficeDetails()
    {
      BranchOfficeProjection BranchOfficeDetails = _branchOfficeManager.GetByID(1);
      EditBranchOfficeDetailsVM vM = new EditBranchOfficeDetailsVM()
      {
        Address = BranchOfficeDetails.Address,
        Email = BranchOfficeDetails.Email,
        Telephone = BranchOfficeDetails.Telephone
      };
      return View(vM);
    }

    [HttpPost]
    [UserAuthorize]
    public ActionResult EditBranchOfficeDetails(EditBranchOfficeDetailsVM model)
    {
      //BranchOfficeProjection projection = _branchOfficeManager.GetByID(1);
      //if (projection is null)
      //  return new HttpStatusCodeResult(404);
      //if (!ModelState.IsValid)
      //  return View(viewName: nameof(EditBranchOfficeDetails), model: model);
      //try
      //{
      //  Int32 updatedCount = _branchOfficeManager.Update(projection: new BranchOfficeProjection
      //  {
      //    Address = model.Address,
      //    Email = model.Email,
      //    Telephone = model.Telephone
      //  },
      //  updatedBy: LoggedInUser.ID);

      //  if (updatedCount == 0)
      //  {
      //    Message = new AlertMessage(message: "Promijena podataka nije uspješna, pokušajte ponovo");
      //    return View(viewName: nameof(EditBranchOfficeDetails), model: model);
      //  }

      //  Message = new InfoMessage(message: "Promijena podataka je uspješna");
      //  return RedirectToAction(actionName: "Contact", controllerName: "Home");
      //}
      //catch (ArgumentException ex)
      //{
      //  Message = new AlertMessage(message: ex.Message);
      //  return View(viewName: nameof(EditBranchOfficeDetails), model: model);
      //}

      return RedirectToAction(controllerName: "Home", actionName: "Contact");
    }

    [UserAuthorize]
    public ActionResult SalesHistory()
    {
      IEnumerable<PurchaseBookVM> purchases = from purchase in _purchaseManager.GetAll()
                                              join book in _bookManager.GetAll()
                                                on purchase.BookFK equals book.ID
                                              join author in _authorManager.GetAll()
                                                on book.AuthorFK equals author.ID

                                              select new PurchaseBookVM
                                              {
                                                Book = book,
                                                Author = author,
                                                Purchase = purchase
                                              };
      return View("SalesHistory", purchases);
    }
    [UserAuthorize]
    public ActionResult UserMessages()
    {
      IEnumerable<MessageProjection> messages = _messageManager.GetAll();
      return View("UserMessages", messages);
    }
  }
}