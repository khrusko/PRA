using BLL.Abstract.Manager.Projection;
using BLL.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UI.Models;
using BLL.Projection;
using BLL.Status;
using DAL.Status;

namespace UI.Controllers
{
  public class LoginController : Controller
  {
    private readonly IUserManager _userManager = new UserManager();


    [HttpGet]
    public ActionResult Index(string LoginErrorMessage)
    {
      ViewBag.LoginErrorMessage = LoginErrorMessage;
      return View();
    }
    public ActionResult Modal()
    {
      return PartialView("_ForgotPasswordModal");
    }
    [HttpPost]
    public ActionResult RequestResetPassword(string email)
    {

      RequestResetPasswordStatus status = _userManager.RequestResetPassword(email);
      switch (status)
      {
        case RequestResetPasswordStatus.SUCCESS:
          return RedirectToAction(actionName: "ConfirmationSent", controllerName: "Login");
        //break;
        case RequestResetPasswordStatus.INVALID_EMAIL:
          return RedirectToAction(actionName: "ResetPasswordError", controllerName: "Login", new { errorMessage = "Ne postoji korisnički račun sa unesenim E - mailom" });
        //break;
        case RequestResetPasswordStatus.RESET_PENDING:
          return RedirectToAction(actionName: "ResetPasswordError", controllerName: "Login", new { errorMessage = "Već je zatražena obnova zaporke" });
          // break;
      }
      return Index("");
    }

    [HttpGet]
    public ActionResult ConfirmationSent()
    {
      return View();
    }
    [HttpGet]
    public ActionResult ResetPassword(string id)
    {

      //if (!ModelState.IsValid) return RedirectToAction(actionName: "Index", controllerName: "Login");
      ResetPasswordVM resetPasswordVM = new ResetPasswordVM();

      //Handle null exception
      if (id == null) return RedirectToAction(actionName: "Index", controllerName: "Login");
      resetPasswordVM.GUID = new Guid(id);
      ResetPasswordStatus status = _userManager.CheckResetPasswordStatus(resetPasswordVM.GUID);

      switch (status)
      {
        case ResetPasswordStatus.INVALID:
          return RedirectToAction(actionName: "ResetPasswordError", controllerName: "Login", new { errorMessage = "Link koji ste koristili nije valjani" });
        case ResetPasswordStatus.VALID:
          return View(resetPasswordVM);
        case ResetPasswordStatus.APPROVED:
          return RedirectToAction(actionName: "ResetPasswordError", controllerName: "Login", new { errorMessage = "Link koji ste koristili je već iskorišten za obnovu zaporke" });
        case ResetPasswordStatus.TIMEOUT:
          return RedirectToAction(actionName: "ResetPasswordError", controllerName: "Login", new { errorMessage = "Link koji ste koristili je istekao" });
      }


      return View(resetPasswordVM);
    }

    [HttpGet]
    public ActionResult ResetPasswordError(string errorMessage)
    {
      ViewBag.ErrorMessage = errorMessage;
      return View();
    }

    [HttpPost]
    public ActionResult ResetPassword(ResetPasswordVM resetPasswordVM)
    {
      if (!ModelState.IsValid) return View(resetPasswordVM);
      _userManager.ResetPassword(resetPasswordVM.GUID, resetPasswordVM.Password);

      return RedirectToAction(actionName: "Index", controllerName: "Login", new { LoginErrorMessage = "Uspješno ste promijenili zaporku" });

    }
    [HttpPost]
    public ActionResult Index(LoginVM loginVM)
    {
      if (!ModelState.IsValid) return Index("");
      UserProjection userProjection = _userManager.Login(loginVM.Email, loginVM.Password);
      if (userProjection != null)
      {
        //Successful login - User
        if (userProjection.IsAdmin == false)
          return RedirectToAction("Index", "Book");
        //Successful login - Admin
        if (userProjection.IsAdmin == true)
          return RedirectToAction("Index", "Registration");
      }

      //Login failed
      return Index("Unijeli ste pogrešnu kombinaciju Emaila i Zaporke");
    }
  }
}