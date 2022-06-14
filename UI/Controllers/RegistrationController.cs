using System;
using System.Web.Mvc;

using BLL.Abstract.Manager.Projection;
using BLL.Manager;
using BLL.Projection;

using DAL.Status;

using UI.Models;
using UI.Models.Concrete;

namespace UI.Controllers
{
  public class RegistrationController : Controller
  {
    private readonly IUserManager _userManager = new UserManager();

    [HttpGet]
    public ViewResult Index() => View(viewName: nameof(Index));

    [HttpPost]
    public ViewResult Index(RegisterVM model)
    {
      if (!ModelState.IsValid) return View(nameof(Index), model: model);

      UserProjection projection = _userManager.Register(fName: model.FName,
                                                        lName: model.LName,
                                                        email: model.Email,
                                                        password: model.Password,
                                                        isAdmin: false);
      TempData["message"] = !(projection is null)
        ? new InfoMessage(message: $"Link za potvrdu registracije je poslan na E-Mail: {projection.Email}")
        : (Object)new AlertMessage(message: $"Registracija nije uspjela, pokušajte ponovo");

      return View(viewName: nameof(Index));
    }

    [HttpGet]
    public RedirectToRouteResult UserVerification(String id)
    {
      if (id is null)
        return RedirectToAction(actionName: "Index", controllerName: "Registration");

      var GUID = new Guid(id);
      RegistrationStatus status = _userManager.CheckRegistrationStatus(GUID: GUID);
      switch (status)
      {
        case RegistrationStatus.INVALID:
          TempData["message"] = new AlertMessage(message: "Link koji ste koristili nije valjani");
          return RedirectToAction(actionName: "Index", controllerName: "Login");
        case RegistrationStatus.VALID:
          TempData["message"] = new InfoMessage(message: "Registracija uspješna, molimo Vas da se ulogirate");
          _ = _userManager.ConfirmRegistration(GUID: GUID);
          return RedirectToAction(actionName: "Index", controllerName: "Login");
        case RegistrationStatus.APPROVED:
          TempData["message"] = new AlertMessage(message: "Korisnički račun s danim parametrima već postoji");
          return RedirectToAction(actionName: "Index", controllerName: "Login");
        case RegistrationStatus.TIMEOUT:
          TempData["message"] = new AlertMessage(message: "Link koji ste koristili je istekao");
          return RedirectToAction(actionName: "Index", controllerName: "Login");
        default:
          throw new InvalidOperationException();
      }
    }
  }
}