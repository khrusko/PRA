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
  public class HomeController : BaseController
  {
    private readonly IBookManager _bookManager = new BookManager();
    private readonly IPublisherManager _publisherManager = new PublisherManager();
    private readonly IBranchOfficeManager _branchOfficeManager = new BranchOfficeManager();
    private readonly IMessageManager _messageManager = new MessageManager();
    private readonly IBookStoreManager _bookStoreManager = new BookStoreManager();
    private readonly IAuthorManager _authorManager = new AuthorManager();

    [HttpGet]
    public ViewResult Index()
    {
      IEnumerable<FullBookInfoVM> books = from book in _bookManager.GetAll()
                                          join publisher in _publisherManager.GetAll()
                                            on book.PublisherFK equals publisher.ID
                                          join author in _authorManager.GetAll()
                                            on book.PublisherFK equals author.ID
                                          select new FullBookInfoVM
                                          {
                                            Book = book,
                                            Publisher = publisher,
                                            Author = author
                                          };

      return View(viewName: nameof(Index),
                  model: new HomeVM
                  {
                    Books = books,
                    BookStore = _bookStoreManager.GetBookStore()
                  });
    }

    [HttpGet]
    public ViewResult Privacy() => View(viewName: nameof(Privacy));

    [HttpGet]
    public ViewResult TermsOfService() => View(viewName: nameof(TermsOfService));

    [HttpGet]
    public ViewResult Contact() => View(viewName: nameof(Contact), model: _branchOfficeManager.GetByID(1));

    [HttpGet]
    public ViewResult ContactUs() => View(viewName: nameof(ContactUs));

    [HttpPost]
    public ActionResult ContactUs(ContactFormVM model)
    {
      if (!ModelState.IsValid)
      {
        return View(viewName: nameof(ContactUs), model: model);
      }

      _ = _messageManager.Send(model.FName, model.LName, model.Email, model.Message);

      Message = new InfoMessage(message: "Upit je uspješno poslan");
      return RedirectToAction(actionName: "Index", controllerName: "Book");
    }
  }
}