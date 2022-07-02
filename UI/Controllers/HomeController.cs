using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

using BLL.Abstract.Manager.Projection;
using BLL.Manager;

using UI.Models;
using UI.Models.Concrete;

namespace UI.Controllers
{
  public class HomeController : BaseController
  {
    private readonly IBookManager _bookManager = new BookManager();
    private readonly IPublisherManager _publisherManager = new PublisherManager();
    private readonly IMessageManager _messageManager = new MessageManager();
    private readonly IBookStoreManager _bookStoreManager = new BookStoreManager();
    private readonly IAuthorManager _authorManager = new AuthorManager();

    [HttpGet]
    public ViewResult Index()
    {
      IEnumerable<BookCardVM> books = from book in _bookManager.GetAll()
                                          join publisher in _publisherManager.GetAll(availabilityCheck: false)
                                            on book.PublisherFK equals publisher.ID
                                          join author in _authorManager.GetAll(availabilityCheck: false)
                                            on book.AuthorFK equals author.ID
                                          select new BookCardVM
                                          {
                                            Book = book,
                                            Author = $"{author.FName} {author.LName}"
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
    public ViewResult Contact() => View(viewName: nameof(Contact), model: _bookStoreManager.GetBookStore());

    [HttpGet]
    public ViewResult ContactUs()
      => View(viewName: nameof(ContactUs),
              model: !(LoggedInUser is null)
                ? new ContactFormVM
                {
                  FName = LoggedInUser.FName,
                  LName = LoggedInUser.LName,
                  Email = LoggedInUser.Email
                }
                : new ContactFormVM());

    [HttpPost]
    public ActionResult ContactUs(ContactFormVM model)
    {
      if (!ModelState.IsValid)
      {
        return View(viewName: nameof(ContactUs), model: model);
      }

      _ = _messageManager.Send(model.FName, model.LName, model.Email, model.Message);

      Message = new InfoMessage(message: "Upit je uspješno poslan");
      return RedirectToAction(actionName: "Index", controllerName: "Home");
    }
  }
}