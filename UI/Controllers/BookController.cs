using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using BLL.Abstract.Manager.Projection;
using BLL.Manager;

using UI.Models;

namespace UI.Controllers
{
  public class BookController : Controller
  {

    // GET: Book
    private readonly IBookManager _bookManager = new BookManager();
    private readonly IPublisherManager _publisherManager = new PublisherManager();
    public ActionResult Index()
    {
      var allPublishers = _publisherManager.GetAll();
      var allBooks = _bookManager.GetAll();
      IEnumerable<BookPublisherVM> enumerable = allBooks.Join(allPublishers, x => x.PublisherFK, x => x.ID, (x, y) => new BookPublisherVM { Book = x, Publisher = y });

      return View(enumerable);
    }

    public ActionResult Details(int id)
    {
      if (id == 0) RedirectToAction(actionName: "BadRequest", controllerName: "HttpErrors");
      var specificBook = _bookManager.GetByID(id);
      int publisherFKTemp = specificBook.PublisherFK;
      ViewBag.Publisher = _publisherManager.GetByID(publisherFKTemp).Name;
      return View(specificBook);
    }

    [HttpGet]
    public ActionResult SearchPage()
    {
      var allPublishers = _publisherManager.GetAll();
      var allBooks = _bookManager.GetAll();
      IEnumerable<BookPublisherVM> enumerable = allBooks.Join(allPublishers, x => x.PublisherFK, x => x.ID, (x, y) => new BookPublisherVM { Book = x, Publisher = y });
      return View(enumerable);
    }

    [HttpPost]
    public ActionResult SearchPage(bool checkbox)
    {
      var allPublishers = _publisherManager.GetAll();
      var allBooks = _bookManager.GetAll();
      IEnumerable<BookPublisherVM> enumerable = allBooks.Join(allPublishers, x => x.PublisherFK, x => x.ID, (x, y) => new BookPublisherVM { Book = x, Publisher = y });
      if (checkbox) return View(enumerable.Where(x => x.Book.Quantity > 0));
      else return View(enumerable);
    }



  }
}