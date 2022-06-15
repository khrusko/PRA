using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using BLL.Projection;

using UI.Models.Abstract;

namespace UI.Controllers
{
  public abstract class BaseController : Controller
  {
    public UserProjection LoggedInUser
    {
      get => Session["user"] as UserProjection;
      set => Session["user"] = value;
    }

    public IMessage Message
    {
      get => TempData["message"] as IMessage;
      set => TempData["message"] = value;
    }
  }
}