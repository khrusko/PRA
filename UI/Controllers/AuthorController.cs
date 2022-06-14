using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL.Manager;
using BLL.Abstract.Manager.Projection;
using BLL.Projection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace UI.Controllers
{
  public class AuthorController : Controller
  {
    // GET: Author
    private readonly IAuthorManager _authorManager = new AuthorManager();


    public ViewResult Index()
    {
      var allAuthors = _authorManager.GetAll().ToList();
      return View();
    }
  }
}