using System.Web.Mvc;

using BLL.Abstract.Manager.Projection;
using BLL.Manager;

namespace UI.Controllers
{
  public class UserController : Controller
  {
    private readonly IUserManager _userManager = new UserManager();
  }
}