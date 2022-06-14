using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using BLL.Abstract.Manager.Projection;
using BLL.Manager;

namespace UI.Controllers
{
  public class UserController : Controller
  {
    // GET: User
    private readonly IUserManager _userManager = new UserManager();
    public ActionResult Login()
    {

      return View();
    }
  }
}