using System;
using System.Web.Mvc;

using BLL.Abstract.Manager.Projection;
using BLL.Manager;
using BLL.Projection;
using BLL.Status;

using DAL.Status;

using UI.Models;
using UI.Models.Concrete;

namespace UI.Controllers
{
  public class LoginController : Controller
  {
    private readonly IUserManager _userManager = new UserManager();

    [HttpGet]
    public ViewResult Index() => View(viewName: nameof(Index));

    [HttpPost]
    public ActionResult Index(LoginVM model)
    {
      if (!ModelState.IsValid)
      {
        TempData["message"] = new AlertMessage(message: "Unesite ispravne podatke za login");
        return View(viewName: nameof(Index), model: model);
      }

      UserProjection projection = _userManager.Login(email: model.Email, password: model.Password);
      if (!(projection is null))
      {
        TempData["message"] = new InfoMessage(message: $"Pozdrav, {projection.FName} {projection.LName}");

        // TODO: redirect admin and user to dashborad -> replace "Book" with "Dashboard"
        return RedirectToAction(actionName: "Index", controllerName: "Book");
      }

      TempData["message"] = new AlertMessage(message: "Unijeli ste pogrešnu kombinaciju Emaila i Zaporke");
      return View(viewName: nameof(Index), model: model);
    }

    [HttpPost]
    public ViewResult RequestResetPassword(String email)
    {
      if (String.IsNullOrWhiteSpace(email))
      {
        TempData["message"] = new AlertMessage(message: "Email nije važeći");
        return View(viewName: "Index");
      }

      RequestResetPasswordStatus status = _userManager.RequestResetPassword(email: email);
      switch (status)
      {
        case RequestResetPasswordStatus.SUCCESS:
          TempData["message"] = new InfoMessage(message: "Zahtjev za promijenu lozinke poslan je na Vaš E-mail");
          break;
        case RequestResetPasswordStatus.INVALID_EMAIL:
          TempData["message"] = new AlertMessage(message: "Ne postoji korisnički račun sa unesenim E-mailom");
          break;
        case RequestResetPasswordStatus.RESET_PENDING:
          TempData["message"] = new AlertMessage(message: "Već je zatražena obnova zaporke");
          break;
        default:
          throw new InvalidOperationException();
      }

      return View(viewName: "Index");
    }

    [HttpGet]
    public ActionResult ResetPassword(String id)
    {
      if (id is null)
        return RedirectToAction(actionName: "Index", controllerName: "Login");

      var resetPasswordVM = new ResetPasswordVM
      {
        GUID = new Guid(id)
      };
      ResetPasswordStatus status = _userManager.CheckResetPasswordStatus(GUID: resetPasswordVM.GUID);

      switch (status)
      {
        case ResetPasswordStatus.INVALID:
          TempData["message"] = new AlertMessage(message: "Link koji ste koristili nije valjani");
          return RedirectToAction(actionName: "Index", controllerName: "Login");
        case ResetPasswordStatus.VALID:
          return View(viewName: "ResetPassword", model: resetPasswordVM);
        case ResetPasswordStatus.APPROVED:
          TempData["message"] = new AlertMessage(message: "Link koji ste koristili je već iskorišten za obnovu zaporke");
          return RedirectToAction(actionName: "Index", controllerName: "Login");
        case ResetPasswordStatus.TIMEOUT:
          TempData["message"] = new AlertMessage(message: "Link koji ste koristili je istekao");
          return RedirectToAction(actionName: "Index", controllerName: "Login");
        default:
          throw new InvalidOperationException();
      }
    }

    [HttpPost]
    public ActionResult ResetPassword(ResetPasswordVM model)
    {
      if (!ModelState.IsValid)
        return View(viewName: nameof(ResetPassword), model: model);

      _ = _userManager.ResetPassword(GUID: model.GUID, password: model.Password);

      TempData["message"] = new InfoMessage(message: "Uspješno ste promijenili zaporku");
      return RedirectToAction(actionName: "Index", controllerName: "Login");
    }
  }
}