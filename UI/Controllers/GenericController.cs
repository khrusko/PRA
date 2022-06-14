using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL.Manager;
using BLL.Projection;

namespace UI.Controllers
{
    public class GenericController : Controller
    {
        readonly BranchOfficeManager _branchOfficeManager = new BranchOfficeManager();
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
            BranchOfficeProjection branchOfficeDetails = _branchOfficeManager.GetByID(1);
            return View(branchOfficeDetails);
        }
    }
}