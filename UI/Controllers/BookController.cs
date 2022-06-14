using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

using BLL.Abstract.Manager.Projection;
using BLL.Manager;
using BLL.Projection;

using UI.Models;

namespace UI.Controllers
{
  public class BookController : Controller
  {
    private readonly IBookManager _bookManager = new BookManager();
    private readonly IPublisherManager _publisherManager = new PublisherManager();

    [HttpGet]
    public ViewResult Index()
    {
      IEnumerable<BookPublisherVM> bookPublishers = from book in _bookManager.GetAll()
                                                    join publisher in _publisherManager.GetAll()
                                                      on book.PublisherFK equals publisher.ID
                                                    select new BookPublisherVM { Book = book, Publisher = publisher };

      return View(viewName: nameof(Index), model: bookPublishers);
    }

    [HttpGet]
    public ViewResult Details(int id)
    {
      BookProjection book = _bookManager.GetByID(ID: id);
      PublisherProjection publisher = _publisherManager.GetByID(ID: book.PublisherFK);
      return View(viewName: nameof(Details),
                  model: new BookPublisherVM
                  {
                    Book = book,
                    Publisher = publisher
                  });
    }

    [HttpGet]
    public ViewResult SearchPage()
    {
      IEnumerable<BookPublisherVM> bookPublishers = from book in _bookManager.GetAll()
                                                    join publisher in _publisherManager.GetAll()
                                                      on book.PublisherFK equals publisher.ID
                                                    select new BookPublisherVM { Book = book, Publisher = publisher };

      return View(viewName: nameof(SearchPage), model: bookPublishers);
    }

    [HttpPost]
    public ActionResult SearchPage(bool isAvailable, string searchByAuthor, string searchbyBook)
    {
      return View();
    }

  }
}