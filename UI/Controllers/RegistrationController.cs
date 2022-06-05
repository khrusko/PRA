using BLL.Abstract.Manager.Projection;
using BLL.Manager;
using BLL.Projection;
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
        // GET: Registration
        public ActionResult UserVerification()
        {
            return View();
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