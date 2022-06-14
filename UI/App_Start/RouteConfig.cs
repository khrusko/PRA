using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Routing.Constraints;
using System.Web.Routing;

namespace UI
{
  public class RouteConfig
  {
    public static void RegisterRoutes(RouteCollection routes)
    {
      routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

      _ = routes.MapRoute(name: "ResetPassword",
                          url: "Account/ResetPassword/{id}",
                          defaults: new {
                            controller = "Login",
                            action = "ResetPassword",
                            id = UrlParameter.Optional
                          },
                          constraints: new {
                            id = new GuidRouteConstraint()
                          });

      _ = routes.MapRoute(name: "UserVerification",
                          url: "Account/UserVerification/{id}",
                          defaults: new
                          {
                            controller = "Registration",
                            action = "UserVerification",
                            id = UrlParameter.Optional
                          },
                          constraints: new
                          {
                            id = new GuidRouteConstraint()
                          });

      _ = routes.MapRoute(name: "Details",
                          url: "{controller}/Details/{id}",
                          defaults: new
                          {
                            action = "Details"
                          },
                          constraints: new
                          {
                            id = new CompoundRouteConstraint(constraints: new List<IRouteConstraint>
                            {
                              new IntRouteConstraint(),
                              new MinRouteConstraint(min: 1)
                            })
                          });

      _ = routes.MapRoute(name: "Default",
                          url: "{controller}/{action}",
                          defaults: new { 
                            controller = "Book",
                            action = "Index" }
      );
    }
  }
}
