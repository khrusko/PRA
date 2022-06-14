using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace UI
{
  public class MvcApplication : System.Web.HttpApplication
  {
    protected void Application_Start()
    {
      AreaRegistration.RegisterAllAreas();
      RouteConfig.RegisterRoutes(RouteTable.Routes);
      BundleConfig.RegisterBundles(BundleTable.Bundles);
    }

    protected void Application_Error()
    {
      // Sets 404 HTTP exceptions to be handled via IIS (behavior is specified in the "httpErrors" section in the Web.config file)
      var error = Server.GetLastError();
      if ((error as HttpException)?.GetHttpCode() == 404)
      {
        Server.ClearError();
        Response.StatusCode = 404;
      }
      else if ((error as HttpException)?.GetHttpCode() == 400)
      {
        Server.ClearError();
        Response.StatusCode = 400;
      }
    }
  }


}
