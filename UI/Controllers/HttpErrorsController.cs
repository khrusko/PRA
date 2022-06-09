using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UI.Controllers
{
    public class HttpErrorsController : Controller
    {
        // GET: HttpErrors
        public ActionResult NotFound()
        {
            Response.StatusCode = 404;
            return View();
        }

        public ActionResult BadRequest()
        {
            Response.StatusCode = 400;
            return View();
        }
    }
}