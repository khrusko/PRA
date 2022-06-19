using System;
using System.Web.Mvc;

using BLL.Abstract.Manager.Projection;
using BLL.Manager;
using BLL.Projection;

using UI.Infrastructure;
using UI.Models;
using UI.Models.Concrete;

namespace UI.Controllers
{
  [UserAuthenticate]
  public class UserController : BaseController
  {
    private readonly IUserManager _userManager = new UserManager();

    [HttpGet]
    public ActionResult Index()
      => View(viewName: nameof(Index), model: _userManager.GetByID(LoggedInUser.ID));

    [HttpGet]
    public ActionResult Edit()
    {
      UserProjection projection = _userManager.GetByID(LoggedInUser.ID);

      return View(viewName: nameof(Edit),
                  model: new UserEditVM
                  {
                    ID = projection.ID,
                    FName = projection.FName,
                    LName = projection.LName,
                    Address = projection.Address,
                    ImagePath = projection.ImagePath
                  });
    }

    [HttpGet]
    [UserAuthorize]
    public ViewResult Create()
      => View(viewName: nameof(Create),
              model: new RegisterVM());

    [HttpPost]
    [UserAuthorize]
    public ActionResult Create(RegisterVM model)
    {
      if (!ModelState.IsValid)
        return View(viewName: nameof(Create), model: model);

      UserProjection projection = _userManager.CreateAdmin(fName: model.FName,
                                                           lName: model.LName,
                                                           email: model.Email,
                                                           password: model.Password);
      if (projection is null)
      {
        Message = new AlertMessage(message: "Nije moguće dodati djelatnika s danim podacima, korisnik već postoji");
        return View(viewName: nameof(Create), model: model);
      }

      Message = new InfoMessage(message: "Djelatnik je uspješno dodan");
      return RedirectToAction(actionName: "Index", controllerName: "Dashboard");
    }

    [HttpPost]
    public ActionResult Edit(UserEditVM model)
    {
      if (!ModelState.IsValid)
        return View(viewName: nameof(Edit), model: model);

      try
      {
        Int32 updatedCount = _userManager.Update(ID: model.ID,
                                                 fName: model.FName,
                                                 lName: model.LName,
                                                 file: model.Image,
                                                 address: model.Address);

        if (updatedCount == 0)
        {
          Message = new AlertMessage(message: "Promijena podataka nije uspješna, pokušajte ponovo");
          return View(viewName: nameof(Edit), model: model);
        }

        LoggedInUser = _userManager.GetByID(LoggedInUser.ID);

        Message = new InfoMessage(message: "Promijena podataka je uspješna");
        return RedirectToAction(actionName: nameof(Index), controllerName: "User");
      }
      catch (ArgumentException ex)
      {
        Message = new AlertMessage(message: ex.Message);
        return View(viewName: nameof(Edit), model: model);
      }
    }

    [HttpGet]
    public RedirectToRouteResult Delete()
    {
      _ = _userManager.Remove(LoggedInUser.ID);
      LoggedInUser = null;
      return RedirectToAction(actionName: "Login", controllerName: "Account");
    }
  }
}