using System.Web.Mvc;

using BLL.Abstract.Manager.Projection;
using BLL.Manager;

namespace UI.Controllers
{
  public class UserController : BaseController
  {
    private readonly IUserManager _userManager = new UserManager();

    [HttpGet]
    public ActionResult Index() => 
      LoggedInUser is null
      ? RedirectToAction(actionName: "Login", controllerName: "Account")
      : (ActionResult)View(viewName: nameof(Index), model: _userManager.GetByID(LoggedInUser.ID));
  }
}