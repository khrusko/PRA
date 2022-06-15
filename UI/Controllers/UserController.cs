using System.Web.Mvc;

using BLL.Abstract.Manager.Projection;
using BLL.Manager;

namespace UI.Controllers
{
  public class UserController : BaseController
  {
    private readonly IUserManager _userManager = new UserManager();
  }
}