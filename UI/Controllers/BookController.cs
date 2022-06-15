using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

using BLL.Abstract.Manager.Projection;
using BLL.Manager;
using BLL.Projection;

using UI.Models;

namespace UI.Controllers
{
  public class BookController : BaseController
  {
    private const Int32 PAGE_SIZE = 6;

    private readonly IBookManager _bookManager = new BookManager();
    private readonly IPublisherManager _publisherManager = new PublisherManager();
    private readonly IAuthorManager _authorManager = new AuthorManager();

    [HttpGet]
    public ViewResult Details(Int32 id)
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
    public ViewResult Search(Boolean availableOnly = false, String bookQuery = null, String authorQuery = null, Int32 page = 1)
    {
      IEnumerable<FullBookInfoVM> books = from book in _bookManager.GetAll()
                                          join publisher in _publisherManager.GetAll()
                                            on book.PublisherFK equals publisher.ID
                                          join author in _authorManager.GetAll()
                                            on book.PublisherFK equals author.ID
                                          where !availableOnly || book.Quantity > 0
                                          where book.Title.ToLower().Contains(value: bookQuery?.ToLower() ?? String.Empty)
                                          where $"{author.FName} {author.LName}".ToLower().Contains(value: authorQuery?.ToLower() ?? String.Empty)
                                          select new FullBookInfoVM
                                          {
                                            Book = book,
                                            Publisher = publisher,
                                            Author = author
                                          };

      return View(viewName: nameof(Search),
                  model: new BookSearchVM
                  {
                    BookPublishers = books.Skip(count: (page - 1) * PAGE_SIZE)
                                          .Take(count: PAGE_SIZE),
                    PagingInfo = new PagingInfo
                    {
                      CurrentPage = page,
                      ItemsPerPage = PAGE_SIZE,
                      TotalItems = books.Count()
                    },
                    AvailableOnly = availableOnly,
                    BookQuery = bookQuery,
                    AuthorQuery = authorQuery
                  });
    }

    [HttpPost]
    public ActionResult Search(BookSearchVM model)
    {
      if (!ModelState.IsValid)
        return Search();

      IEnumerable<FullBookInfoVM> books = from book in _bookManager.GetAll()
                                          join publisher in _publisherManager.GetAll()
                                            on book.PublisherFK equals publisher.ID
                                          join author in _authorManager.GetAll()
                                            on book.PublisherFK equals author.ID
                                          where !model.AvailableOnly || book.Quantity > 0
                                          where book.Title.ToLower().Contains(value: model.BookQuery?.ToLower() ?? String.Empty)
                                          where $"{author.FName} {author.LName}".ToLower().Contains(value: model.AuthorQuery?.ToLower() ?? String.Empty)
                                          select new FullBookInfoVM
                                          {
                                            Book = book,
                                            Publisher = publisher,
                                            Author = author
                                          };

      return View(viewName: nameof(Search),
                  model: new BookSearchVM
                  {
                    BookPublishers = books.Take(count: PAGE_SIZE),
                    PagingInfo = new PagingInfo
                    {
                      CurrentPage = 1,
                      ItemsPerPage = PAGE_SIZE,
                      TotalItems = books.Count()
                    },
                    AvailableOnly = model.AvailableOnly,
                    BookQuery = model.BookQuery,
                    AuthorQuery = model.AuthorQuery
                  });
    }
  }
}