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
                          defaults: new
                          {
                            controller = "Account",
                            action = "ResetPassword",
                            id = UrlParameter.Optional
                          },
                          constraints: new
                          {
                            id = new GuidRouteConstraint()
                          });

      _ = routes.MapRoute(name: "UserVerification",
                          url: "Account/UserVerification/{id}",
                          defaults: new
                          {
                            controller = "Account",
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
                            action = "Details",
                            id = UrlParameter.Optional
                          },
                          constraints: new
                          {
                            id = new CompoundRouteConstraint(constraints: new List<IRouteConstraint>
                            {
                              new IntRouteConstraint(),
                              new MinRouteConstraint(min: 1)
                            })
                          });

      _ = routes.MapRoute(name: "Edit",
                          url: "{controller}/Edit/{id}",
                          defaults: new
                          {
                            action = "Edit",
                            id = UrlParameter.Optional
                          },
                          constraints: new
                          {
                            id = new CompoundRouteConstraint(constraints: new List<IRouteConstraint>
                            {
                              new IntRouteConstraint(),
                              new MinRouteConstraint(min: 1)
                            })
                          });

      _ = routes.MapRoute(name: "Delete",
                           url: "{controller}/Delete/{id}",
                           defaults: new
                           {
                             action = "Delete",
                             id = UrlParameter.Optional
                           },
                           constraints: new
                           {
                             id = new CompoundRouteConstraint(constraints: new List<IRouteConstraint>
                             {
                              new IntRouteConstraint(),
                              new MinRouteConstraint(min: 1)
                             })
                           });

      _ = routes.MapRoute(name: "Create",
                           url: "{controller}/Create",
                           defaults: new
                           {
                             action = "Create",
                             id = UrlParameter.Optional
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
                          defaults: new
                          {
                            controller = "Home",
                            action = "Index"
                          }
      );
    }
  }
}
