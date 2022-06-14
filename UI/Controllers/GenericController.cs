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

    public ViewResult Privacy() => View(viewName: nameof(Privacy));

    public ViewResult TermsOfService() => View(viewName: nameof(TermsOfService));

    public ViewResult Contact() => View(viewName: nameof(Contact), model: _branchOfficeManager.GetByID(1));
  }
}