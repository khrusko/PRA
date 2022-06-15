using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

using BLL.Abstract.Manager.Projection;
using BLL.Manager;
using BLL.Projection;

using UI.Models;
using UI.Models.Enums;

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
    public ViewResult Search(String bookQuery,
                             String authorQuery,
                             Boolean availableOnly = false,
                             BookSortType bookSortType = 0,
                             SortDirection sortDirection = 0,
                             Int32 page = 1)
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

      switch (bookSortType)
      {
        case BookSortType.TITLE:
          books = books.SortBy(keySelector: obj => obj.Book.Title, sortDirection: sortDirection);
          break;
        case BookSortType.AUTHOR:
          books = books.SortBy(keySelector: obj => $"{obj.Author.FName} {obj.Author.LName}", sortDirection: sortDirection);
          break;
        case BookSortType.PURCHASE_PRICE:
          books = books.SortBy(keySelector: obj => obj.Book.PurchasePrice, sortDirection: sortDirection);
          break;
        default:
          throw new InvalidOperationException();
      }

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
                    AuthorQuery = authorQuery,
                    BookSortType = bookSortType,
                    SortDirection = sortDirection
                  });
    }
  }
}