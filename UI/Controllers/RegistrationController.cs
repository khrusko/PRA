using BLL.Abstract.Manager.Projection;
using BLL.Manager;
using BLL.Projection;

using DAL.Status;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using UI.Models;

namespace UI.Controllers
{
  public class RegistrationController : Controller
  {
    private readonly IUserManager _userManager = new UserManager();


    [HttpGet]
    public ActionResult UserVerification(string id)
    {
      Guid GUID = new Guid(id);

      RegistrationStatus status = _userManager.CheckRegistrationStatus(GUID);
      switch (status)
      {

        case RegistrationStatus.INVALID:
          return Index("Link koji ste koristili nije valjani");
        //break;
        case RegistrationStatus.VALID:
          _userManager.ConfirmRegistration(GUID);
          break;
        case RegistrationStatus.APPROVED:
          return Index("Korisnički račun s danim parametrima već postoji");
        //break;
        case RegistrationStatus.TIMEOUT:
          return Index("Link koji ste koristili je istekao");
          //break;
      }
      return RedirectToAction("Index", "Login");
    }

    [HttpGet]
    public ActionResult Index()
    {
      return View();
    }

    public ActionResult Index(string RegistrationErrorMessage)
    {
      ViewBag.RegistrationErrorMessage = RegistrationErrorMessage;
      return View("Index");
    }

    [HttpPost]
    public ActionResult Index(RegisterVM registerVM)
    {
      if (!ModelState.IsValid) return Index();

      UserProjection userProjection = _userManager.Register(registerVM.FName, registerVM.LName, registerVM.Email, registerVM.Password, false);

      if (userProjection != null)
        return RedirectToAction("ConfirmationSent", userProjection.GUID);
      else
        return RedirectToAction("Index");
    }

    public ActionResult ConfirmationSent()
    {
      return View();
    }
  }
}