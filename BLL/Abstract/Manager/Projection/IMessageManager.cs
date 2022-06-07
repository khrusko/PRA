using System;

using BLL.Projection;

using DAL.Model;

namespace BLL.Abstract.Manager.Projection
{
  public interface IMessageManager : IProjectionManager<MessageModel, MessageProjection, Int32>
  {
    Int32 Send(MessageProjection projection);
    Int32 Send(Int32 SenderUserFK, String SenderMessage);
    Int32 Respond(MessageProjection projection);
    Int32 Respond(Int32 ID, Int32 ResponderUserFK, String ResponderMessage);
  }
}
