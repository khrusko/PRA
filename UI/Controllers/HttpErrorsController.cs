using System.Web.Mvc;

namespace UI.Controllers
{
  public class HttpErrorsController : Controller
  {
    public ViewResult NotFound()
    {
      Response.StatusCode = 404;
      return View(viewName: nameof(NotFound));
    }

    public ViewResult BadRequest()
    {
      Response.StatusCode = 400;
      return View(viewName: nameof(BadRequest));
    }
  }
}