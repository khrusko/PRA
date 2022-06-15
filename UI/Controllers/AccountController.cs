using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using BLL.Abstract.Manager.Projection;
using BLL.Manager;
using BLL.Projection;
using BLL.Status;
using DAL.Status;
using UI.Models.Concrete;
using UI.Models;
using UI.Models.Abstract;

namespace UI.Controllers
{
  public class AccountController : BaseController
  {
    private readonly IUserManager _userManager = new UserManager();

    [HttpGet]
    public ViewResult Login() => View(viewName: nameof(Login));

    [HttpPost]
    public ActionResult Login(LoginVM model)
    {
      if (!ModelState.IsValid)
      {
        Message = new AlertMessage(message: "Unesite ispravne podatke za login");
        return View(viewName: nameof(Login), model: model);
      }

      UserProjection projection = _userManager.Login(email: model.Email, password: model.Password);
      if (!(projection is null))
      {
        LoggedInUser = projection;

        // TODO: redirect admin and user to dashborad -> replace "Book" with "Dashboard"
        Message = new InfoMessage(message: $"Pozdrav, {projection.FName} {projection.LName}");
        return RedirectToAction(actionName: "Index", controllerName: "Home");
      }

      Message = new AlertMessage(message: "Unijeli ste pogrešnu kombinaciju Emaila i Zaporke");
      return View(viewName: nameof(Login), model: model);
    }

    [HttpGet]
    public RedirectToRouteResult Logout()
    {
      LoggedInUser = null;
      return RedirectToAction(actionName: "Index", controllerName: "Home");
    }

    [HttpPost]
    public ViewResult RequestResetPassword(String email)
    {
      if (String.IsNullOrWhiteSpace(email))
      {
        Message = new AlertMessage(message: "Email nije važeći");
        return View(viewName: nameof(Login));
      }

      RequestResetPasswordStatus status = _userManager.RequestResetPassword(email: email);
      switch (status)
      {
        case RequestResetPasswordStatus.SUCCESS:
          Message = new InfoMessage(message: "Zahtjev za promijenu lozinke poslan je na Vaš E-mail");
          break;
        case RequestResetPasswordStatus.INVALID_EMAIL:
          Message = new AlertMessage(message: "Ne postoji korisnički račun sa unesenim E-mailom");
          break;
        case RequestResetPasswordStatus.RESET_PENDING:
          Message = new AlertMessage(message: "Već je zatražena obnova zaporke");
          break;
        default:
          throw new InvalidOperationException();
      }

      return View(viewName: nameof(Login));
    }

    [HttpGet]
    public ActionResult ResetPassword(String id)
    {
      if (String.IsNullOrEmpty(id))
        return RedirectToAction(actionName: nameof(Login));

      var resetPasswordVM = new ResetPasswordVM
      {
        GUID = new Guid(id)
      };
      ResetPasswordStatus status = _userManager.CheckResetPasswordStatus(GUID: resetPasswordVM.GUID);

      switch (status)
      {
        case ResetPasswordStatus.INVALID:
          Message = new AlertMessage(message: "Link koji ste koristili nije valjani");
          return RedirectToAction(actionName: nameof(Login));
        case ResetPasswordStatus.VALID:
          return View(viewName: nameof(ResetPassword), model: resetPasswordVM);
        case ResetPasswordStatus.APPROVED:
          Message = new AlertMessage(message: "Link koji ste koristili je već iskorišten za obnovu zaporke");
          return RedirectToAction(actionName: nameof(Login));
        case ResetPasswordStatus.TIMEOUT:
          Message = new AlertMessage(message: "Link koji ste koristili je istekao");
          return RedirectToAction(actionName: nameof(Login));
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

      Message = new InfoMessage(message: "Uspješno ste promijenili zaporku");
      return RedirectToAction(actionName: nameof(Login));
    }

    [HttpGet]
    public ViewResult Register() => View(viewName: nameof(Register));

    [HttpPost]
    public ViewResult Register(RegisterVM model)
    {
      if (!ModelState.IsValid) return View(nameof(Register), model: model);

      UserProjection projection = _userManager.Register(fName: model.FName,
                                                        lName: model.LName,
                                                        email: model.Email,
                                                        password: model.Password,
                                                        isAdmin: false);
      Message = !(projection is null)
        ? new InfoMessage(message: $"Link za potvrdu registracije je poslan na E-Mail: {projection.Email}")
        : (IMessage)new AlertMessage(message: $"Registracija nije uspjela, pokušajte ponovo");

      return View(viewName: nameof(Register));
    }

    [HttpGet]
    public ActionResult UserVerification(String id)
    {
      if (id is null)
        return RedirectToAction(actionName: nameof(Register));

      var GUID = new Guid(id);
      RegistrationStatus status = _userManager.CheckRegistrationStatus(GUID: GUID);
      switch (status)
      {
        case RegistrationStatus.INVALID:
          Message = new AlertMessage(message: "Link koji ste koristili nije valjani");
          return RedirectToAction(actionName: nameof(Register));
        case RegistrationStatus.VALID:
          Message = new InfoMessage(message: "Registracija uspješna, molimo Vas da se ulogirate");
          _ = _userManager.ConfirmRegistration(GUID: GUID);
          return View(viewName: nameof(Login));
        case RegistrationStatus.APPROVED:
          Message = new AlertMessage(message: "Korisnički račun s danim parametrima već postoji");
          return RedirectToAction(actionName: nameof(Register));
        case RegistrationStatus.TIMEOUT:
          Message = new AlertMessage(message: "Link koji ste koristili je istekao");
          return RedirectToAction(actionName: nameof(Register));
        default:
          throw new InvalidOperationException();
      }
    }
  }
}