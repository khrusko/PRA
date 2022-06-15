using System.Web.Mvc;
using System.Web.Mvc.Filters;
using System.Web.Routing;

namespace UI.Infrastructure
{
  public class UserAuthenticateAttribute : FilterAttribute, IAuthenticationFilter
  {
    public void OnAuthentication(AuthenticationContext filterContext)
    {
      if (filterContext.HttpContext.Session["user"] is null)
      {
        filterContext.Result = new HttpUnauthorizedResult();
      }
    }

    public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
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