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

namespace UI.Controllers
{
  public class AuthorController : BaseController
  {
    private readonly IAuthorManager _authorManager = new AuthorManager();
    private readonly IBookManager _bookManager = new BookManager();
    private readonly IPublisherManager _publisherManager = new PublisherManager();

    [HttpGet]
    public ActionResult Details(Int32 id = 0)
    {
      AuthorProjection author = _authorManager.GetByID(id);
      if (author is null) return new HttpStatusCodeResult(404);

      IEnumerable<FullBookInfoVM> books = from book in _bookManager.GetBooksByAuthorFK(authorFK: author.ID)
                                          join publisher in _publisherManager.GetAll(availabilityCheck: false)
                                            on book.PublisherFK equals publisher.ID
                                          select new FullBookInfoVM
                                          {
                                            Book = book,
                                            Publisher = publisher,
                                            Author = author
                                          };
      return View(viewName: nameof(Details),
                  model: new AuthorVM
                  {
                    Author = author,
                    Books = books
                  });
    }

    [HttpGet]
    [UserAuthorize]
    public ViewResult Create()
      => View(viewName: nameof(Create),
              model: new AuthorEditVM() { BirthDate = DateTime.Now });

    [HttpPost]
    [UserAuthorize]
    public ActionResult Create(AuthorEditVM model)
    {
      if (!ModelState.IsValid)
        return View(viewName: nameof(Create), model: model);

      try
      {
        Int32 id = _authorManager.Create(projection: new AuthorProjection
        {
          FName = model.FName,
          LName = model.LName,
          BirthDate = model.BirthDate,
          Biography = model.Biography,
          ImagePath = model.ImagePath
        },
                                         file: model.Image,
                                         createdBy: LoggedInUser.ID);

        if (id == 0)
        {
          Message = new AlertMessage(message: "Autor nije uspješno dodan, pokušajte ponovo");
          return View(viewName: nameof(Create), model: model);
        }

        Message = new InfoMessage(message: "Autor je uspješno dodan");
        return RedirectToAction(actionName: nameof(Details),
                                controllerName: "Author",
                                routeValues: new { id });
      }
      catch (ArgumentException ex)
      {
        Message = new AlertMessage(message: ex.Message);
        return View(viewName: nameof(Create), model: model);
      }
    }

    [HttpGet]
    [UserAuthorize]
    public ActionResult Edit(Int32 id = 0)
    {
      AuthorProjection projection = _authorManager.GetByID(id);

      return projection is null
        ? new HttpStatusCodeResult(404)
        : (ActionResult)View(viewName: nameof(Edit),
                             model: new AuthorEditVM
                             {
                               ID = projection.ID,
                               FName = projection.FName,
                               LName = projection.LName,
                               BirthDate = projection.BirthDate,
                               Biography = projection.Biography,
                               ImagePath = projection.ImagePath
                             });
    }

    [HttpPost]
    [UserAuthorize]
    public ActionResult Edit(AuthorEditVM model)
    {
      AuthorProjection projection = _authorManager.GetByID(model.ID);
      if (projection is null)
        return new HttpStatusCodeResult(404);

      if (!ModelState.IsValid)
        return View(viewName: nameof(Edit), model: model);

      try
      {
        Int32 updatedCount = _authorManager.Update(projection: new AuthorProjection
        {
          ID = model.ID,
          FName = model.FName,
          LName = model.LName,
          BirthDate = model.BirthDate,
          Biography = model.Biography,
          ImagePath = model.ImagePath
        },
                                                   file: model.Image,
                                                   updatedBy: LoggedInUser.ID);

        if (updatedCount == 0)
        {
          Message = new AlertMessage(message: "Promijena podataka nije uspješna, pokušajte ponovo");
          return View(viewName: nameof(Edit), model: model);
        }

        Message = new InfoMessage(message: "Promijena podataka je uspješna");
        return RedirectToAction(actionName: nameof(Details),
                                controllerName: "Author",
                                routeValues: new
                                {
                                  id = model.ID
                                });
      }
      catch (ArgumentException ex)
      {
        Message = new AlertMessage(message: ex.Message);
        return View(viewName: nameof(Edit), model: model);
      }
    }

    [HttpGet]
    [UserAuthorize]
    public ActionResult Delete(Int32 id = 0)
    {
      Int32 deletedCount = _authorManager.Remove(ID: id, deletedBy: LoggedInUser.ID);
      if (deletedCount == 0)
        return new HttpStatusCodeResult(404);

      Message = new InfoMessage(message: "Autor je uspješno obrisan");
      return RedirectToAction(actionName: "Search", controllerName: "Book");
    }
  }
}