using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

using BLL.Abstract.Manager.Projection;
using BLL.Manager;
using BLL.Projection;

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

    [HttpGet]
    public ViewResult Index()
    {
      IEnumerable<LoanBookVM> loans = from loan in _loanManager.GetActiveByUserFK(LoggedInUser.ID)
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

  }
}