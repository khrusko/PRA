using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using BLL.Abstract.Manager.Projection;
using BLL.Manager;
using BLL.Projection;

using UI.Infrastructure;
using UI.Models;
using UI.Models.Concrete;

namespace UI.Controllers
{
  [UserAuthorize]
  public class MessageController : BaseController
  {
    private readonly IMessageManager _messageManager = new MessageManager();
    private readonly IUserManager _userManager = new UserManager();

    [HttpGet]
    public ViewResult List()
    {
      IEnumerable<MessageProjection> messages = _messageManager.GetAll();
      return View(viewName: nameof(List),
                  model: new MessageListVM
                  { 
                    RespondedMessages = messages.Where(predicate: message => message.ResponderDate != DateTime.MinValue),
                    NotRespondedMessages = messages.Where(predicate: message => message.ResponderDate == DateTime.MinValue),
                  });
    }

    [HttpGet]
    public ActionResult Details(Int32 id = 0)
    {
      MessageProjection projection = _messageManager.GetByID(id);
      return (projection is null)
        ? new HttpStatusCodeResult(404)
        : (ActionResult)View(viewName: nameof(Details),
                             model: new MessageDetailsVM
                             {
                               Message = projection,
                               Responder = _userManager.GetByID(projection.ResponderUserFK)
                             });
    }

    [HttpGet]
    public ActionResult Respond(Int32 id = 0)
    {
      MessageProjection projection = _messageManager.GetByID(id);
      return (projection is null || projection.ResponderDate != DateTime.MinValue)
        ? new HttpStatusCodeResult(404)
        : (ActionResult)View(viewName: nameof(Respond),
                             model: new MessageRespondVM
                             {
                               ID = id,
                               SenderFName = projection.SenderFName,
                               SenderLName = projection.SenderLName,
                               SenderDate = projection.SenderDate,
                               SenderEmail = projection.SenderEmail,
                               SenderMessage = projection.SenderMessage
                             });
    }

    [HttpPost]
    public ActionResult Respond(MessageRespondVM model)
    {
      MessageProjection projection = _messageManager.GetByID(model.ID);
      if (projection.ResponderDate != DateTime.MinValue)
        return new HttpStatusCodeResult(404);

      if (!ModelState.IsValid)
      {
        model.SenderFName = projection.SenderFName;
        model.SenderLName = projection.SenderLName;
        model.SenderDate = projection.SenderDate;
        model.SenderEmail = projection.SenderEmail;
        model.SenderMessage = projection.SenderMessage;
        return View(viewName: nameof(Respond), model: model);
      }

      Int32 updatedCount = _messageManager.Respond(ID: model.ID,
                                                   responderUserFK: LoggedInUser.ID,
                                                   responderMessage: model.ResponderMessage);
      if (updatedCount == 0)
      {
        model.SenderFName = projection.SenderFName;
        model.SenderLName = projection.SenderLName;
        model.SenderDate = projection.SenderDate;
        model.SenderEmail = projection.SenderEmail;
        model.SenderMessage = projection.SenderMessage;
        Message = new AlertMessage("Odgovor na poruku nije uspješno poslan");
        return View(viewName: nameof(Respond), model: model);
      }

      return RedirectToAction(actionName: nameof(List), controllerName: "Message");
    }
  }
}