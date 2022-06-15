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
  public class DashboardController : Controller
  {

    private readonly IBookManager _bookManager = new BookManager();
    private readonly IMessageManager _messageManager = new MessageManager();
    private readonly IAuthorManager _authorManager = new AuthorManager();
    private readonly ILoanManager _loanManager = new LoanManager();

    [HttpGet]
    public ViewResult Index()
    {
      IEnumerable<LoanBookVM> loans = from loan in _loanManager.GetAll()
                                          join book in _bookManager.GetAll()
                                            on loan.BookFK equals book.ID
                                          join author in _authorManager.GetAll()
                                            on book.PublisherFK equals author.ID

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
      return View();
    }

    public ActionResult Notifications()
    {
      return View();
    }
    public ActionResult ShoppingHistory()
    {
      return View();
    }

  }
}