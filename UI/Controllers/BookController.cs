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
  public class BookController : BaseController
  {
    private const Int32 PAGE_SIZE = 8;

    private readonly IBookManager _bookManager = new BookManager();
    private readonly IBookStoreManager _bookstoreManager = new BookStoreManager();
    private readonly IPublisherManager _publisherManager = new PublisherManager();
    private readonly IAuthorManager _authorManager = new AuthorManager();
    private readonly ILoanManager _loanManager = new LoanManager();
    private readonly IPurchaseManager _purchaseManager = new PurchaseManager();

    [HttpGet]
    public ActionResult Details(Int32 id = 0)
    {
      BookProjection book = _bookManager.GetByID(ID: id);
      return book is null
        ? new HttpStatusCodeResult(404)
        : (ActionResult)View(viewName: nameof(Details),
                             model: new FullBookInfoVM
                             {
                               Book = book,
                               Publisher = _publisherManager.GetByID(ID: book.PublisherFK, availabilityCheck: false),
                               Author = _authorManager.GetByID(ID: book.AuthorFK, availabilityCheck: false),
                               LoanCount = _loanManager.CountByBookFK(bookFK: book.ID)
                             });
    }

    [HttpGet]
    [UserAuthorize]
    public ViewResult Create()
      => View(viewName: nameof(Create),
              model: new BookVM()
              {
                Publishers = _publisherManager.GetAll().OrderBy(publisher => publisher.Name),
                Authors = _authorManager.GetAll(),
              });

    [HttpPost]
    [UserAuthorize]
    public ActionResult Create(BookVM model)
    {
      if (!ModelState.IsValid)
      {
        model.Authors = _authorManager.GetAll();
        model.Publishers = _publisherManager.GetAll();
        return View(viewName: nameof(Create), model: model);
      }

      try
      {
        Int32 id = _bookManager.Create(projection: new BookProjection
        {
          ID = model.ID,
          ISBN = model.ISBN,
          Title = model.Title,
          Summary = model.Summary,
          Description = model.Description,
          IsNew = model.IsNew,
          PublisherFK = model.PublisherFK,
          AuthorFK = model.AuthorFK,
          PageCount = model.PageCount,
          PurchasePrice = model.PurchasePrice,
          LoanPrice = model.LoanPrice,
          Quantity = model.Quantity,
          ImagePath = model.ImagePath
        },
                                         file: model.Image,
                                         createdBy: LoggedInUser.ID);

        if (id == 0)
        {
          model.Authors = _authorManager.GetAll();
          model.Publishers = _publisherManager.GetAll();
          Message = new AlertMessage(message: "Knjiga nije uspješno dodana, pokušajte ponovo");
          return View(viewName: nameof(Create), model: model);
        }

        Message = new InfoMessage(message: "Knjiga je uspješno dodana");
        return RedirectToAction(actionName: nameof(Details),
                                controllerName: "Book",
                                routeValues: new { id });
      }
      catch (ArgumentException ex)
      {
        model.Authors = _authorManager.GetAll();
        model.Publishers = _publisherManager.GetAll();
        Message = new AlertMessage(message: ex.Message);
        return View(viewName: nameof(Create), model: model);
      }
    }

    [HttpGet]
    [UserAuthorize]
    public ActionResult Edit(Int32 id = 0)
    {
      BookProjection projection = _bookManager.GetByID(id);

      return projection is null
        ? new HttpStatusCodeResult(404)
        : (ActionResult)View(viewName: nameof(Edit),
                             model: new BookVM
                             {
                               ID = projection.ID,
                               ISBN = projection.ISBN,
                               Title = projection.Title,
                               Summary = projection.Summary,
                               Description = projection.Description,
                               IsNew = projection.IsNew,
                               PublisherFK = projection.PublisherFK,
                               AuthorFK = projection.AuthorFK,
                               PageCount = projection.PageCount,
                               PurchasePrice = projection.PurchasePrice,
                               LoanPrice = projection.LoanPrice,
                               Quantity = projection.Quantity,
                               ImagePath = projection.ImagePath,
                               Publishers = _publisherManager.GetAll().OrderBy(publisher => publisher.Name),
                               Authors = _authorManager.GetAll(),
                             });
    }

    [HttpPost]
    [UserAuthorize]
    public ActionResult Edit(BookVM model)
    {
      BookProjection projection = _bookManager.GetByID(model.ID);
      if (projection is null)
        return new HttpStatusCodeResult(404);

      if (!ModelState.IsValid)
      {
        model.Authors = _authorManager.GetAll();
        model.Publishers = _publisherManager.GetAll();
        return View(viewName: nameof(Edit), model: model);
      }

      try
      {
        Int32 updatedCount = _bookManager.Update(projection: new BookProjection
        {
          ID = model.ID,
          ISBN = model.ISBN,
          Title = model.Title,
          Summary = model.Summary,
          Description = model.Description,
          IsNew = model.IsNew,
          PublisherFK = model.PublisherFK,
          AuthorFK = model.AuthorFK,
          PageCount = model.PageCount,
          PurchasePrice = model.PurchasePrice,
          LoanPrice = model.LoanPrice,
          Quantity = model.Quantity,
          ImagePath = model.ImagePath
        },
                                                 file: model.Image,
                                                 updatedBy: LoggedInUser.ID);

        if (updatedCount == 0)
        {
          model.Authors = _authorManager.GetAll();
          model.Publishers = _publisherManager.GetAll();
          Message = new AlertMessage(message: "Promijena podataka nije uspješna, pokušajte ponovo");
          return View(viewName: nameof(Edit), model: model);
        }

        Message = new InfoMessage(message: "Promijena podataka je uspješna");
        return RedirectToAction(actionName: nameof(Details),
                                controllerName: "Book",
                                routeValues: new
                                {
                                  id = model.ID
                                });
      }
      catch (ArgumentException ex)
      {
        model.Authors = _authorManager.GetAll();
        model.Publishers = _publisherManager.GetAll();
        Message = new AlertMessage(message: ex.Message);
        return View(viewName: nameof(Edit), model: model);
      }
    }

    [HttpGet]
    [UserAuthorize]
    public ActionResult Delete(Int32 id = 0)
    {
      Int32 deletedCount = _bookManager.Remove(ID: id, deletedBy: LoggedInUser.ID);
      if (deletedCount == 0)
        return new HttpStatusCodeResult(404);

      Message = new InfoMessage(message: "Knjiga je uspješno obrisana");
      return RedirectToAction(actionName: "Search", controllerName: "Book");
    }

    [HttpGet]
    public ViewResult Search(String bookQuery,
                             String authorQuery,
                             Boolean availableOnly = false,
                             BookSortType bookSortType = 0,
                             SortDirection sortDirection = 0,
                             Int32 page = 1)
    {
      IEnumerable<BookCardVM> books = from book in _bookManager.GetAll(availabilityCheck: false)
                                      let author = _authorManager.GetByID(book.AuthorFK)
                                      where !availableOnly || book.Quantity > 0
                                      where book.Title.ToLower().Contains(value: bookQuery?.ToLower() ?? String.Empty)
                                      where $"{author.FName} {author.LName}".ToLower().Contains(value: authorQuery?.ToLower() ?? String.Empty)
                                      select new BookCardVM
                                      {
                                        Book = book,
                                        Author = $"{author.FName} {author.LName}"
                                      };

      switch (bookSortType)
      {
        case BookSortType.TITLE:
          books = books.SortBy(keySelector: obj => obj.Book.Title, sortDirection: sortDirection);
          break;
        case BookSortType.AUTHOR:
          books = books.SortBy(keySelector: obj => obj.Author, sortDirection: sortDirection);
          break;
        case BookSortType.PURCHASE_PRICE:
          books = books.SortBy(keySelector: obj => obj.Book.PurchasePrice, sortDirection: sortDirection);
          break;
        case BookSortType.CREATE_DATE:
          books = books.SortBy(keySelector: obj => obj.Book.CreateDate, sortDirection: sortDirection);
          break;
        case BookSortType.PAGE_COUNT:
          books = books.SortBy(keySelector: obj => obj.Book.PageCount, sortDirection: sortDirection);
          break;
        case BookSortType.LOAN_COUNT:
          books = books.SortBy(keySelector: obj => _loanManager.CountByBookFK(obj.Book.ID), sortDirection: sortDirection);
          break;
        default:
          throw new InvalidOperationException();
      }

      return View(viewName: nameof(Search),
                  model: new BookSearchVM
                  {
                    Books = books.Skip(count: (page - 1) * PAGE_SIZE)
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