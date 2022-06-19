using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using BLL.Abstract.Manager.Projection;
using BLL.Manager;
using BLL.Projection;

using UI.Infrastructure;
using UI.Models;
using UI.Models.Concrete;

namespace UI.Controllers
{
  [UserAuthorize]
  public class BookStoreController : BaseController
  {
    private readonly IBookStoreManager _bookStoreManager = new BookStoreManager();

    [HttpGet]
    public ActionResult Edit()
    {
      BookStoreProjection projection = _bookStoreManager.GetBookStore();
      return projection is null
        ? new HttpStatusCodeResult(404)
        : (ActionResult)View(viewName: nameof(Edit),
                             model: new BookStoreVM
                             {
                               ID = projection.ID,
                               Name = projection.Name,
                               OIB = projection.OIB,
                               DelayPricePerDay = projection.DelayPricePerDay,
                               Address = projection.Address,
                               Telephone = projection.Telephone,
                               Mobile = projection.Mobile,
                               Email = projection.Email
                             });
    }

    [HttpPost]
    public ActionResult Edit(BookStoreVM model)
    {
      if (!ModelState.IsValid)
        return View(viewName: nameof(Edit), model: model);

      Int32 updatedCount = _bookStoreManager.Update(projection: new BookStoreProjection
      {
        ID = model.ID,
        Name = model.Name,
        OIB = model.OIB,
        DelayPricePerDay = model.DelayPricePerDay,
        Address = model.Address,
        Telephone = model.Telephone,
        Mobile = model.Mobile,
        Email = model.Email
      },
                               updatedBy: LoggedInUser.ID);

      if (updatedCount == 0)
      {
        Message = new AlertMessage(message: "Promijena podataka nije uspješna, pokušajte ponovo");
        return View(viewName: nameof(Edit), model: model);
      }

      Message = new InfoMessage(message: "Promijena podataka je uspješna");
      return RedirectToAction(actionName: "Contact",
                              controllerName: "Home");
    }
  }
}