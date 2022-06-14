using System.Linq;
using System.Web.Mvc;

using BLL.Abstract.Manager.Projection;
using BLL.Manager;

namespace UI.Controllers
{
  public class AuthorController : Controller
  {
    private readonly IAuthorManager _authorManager = new AuthorManager();

    public ViewResult Index()
    {
      var allAuthors = _authorManager.GetAll().ToList();
      return View();
    }
  }
}