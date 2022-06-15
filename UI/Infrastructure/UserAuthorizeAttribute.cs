using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

using BLL.Projection;

namespace UI.Infrastructure
{
  public class UserAuthorizeAttribute : AuthorizeAttribute
  {
    protected override Boolean AuthorizeCore(HttpContextBase httpContext) => httpContext.Session["user"] is UserProjection user && user.IsAdmin;

    protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
    {
      if (filterContext.Result is null || filterContext.Result is HttpUnauthorizedResult)
      {
        filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary
        {
          { "controller", "Account" },
          { "action", "Login" }
        });
      }
    }
  }
}