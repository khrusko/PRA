using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UI.Controllers
{
    public class GenericController : Controller
    {
        // GET: Generic
        public ActionResult Privacy()
        {
            return View();
        }

        public ActionResult TermsOfService()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }
    }
}