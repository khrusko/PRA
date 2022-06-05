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
            Guid ConfirmationGUID = new Guid(id);
            RegistrationStatus status = _userManager.CheckRegistrationStatus(ConfirmationGUID);
            switch (status)
            {
              case RegistrationStatus.INVALID:
                break;
              case RegistrationStatus.VALID:
                _userManager.ConfirmRegistration(ConfirmationGUID);
                break;
              case RegistrationStatus.APPROVED:
                break;
              case RegistrationStatus.TIMEOUT:
                break;
              default:
                break;
            }
          return RedirectToAction("Index", "Login");
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(RegisterVM registerVM)
        {
            if (!ModelState.IsValid) return Index(registerVM);

            _userManager.Register(registerVM.FName, registerVM.LName, registerVM.Email, registerVM.Password, false);


            return RedirectToAction("ConfirmationSent");
        }

        public ActionResult ConfirmationSent()
        {
            return View();
        }
  }
}