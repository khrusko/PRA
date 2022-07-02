using System;

using DAL.Model;

namespace DAL.Abstract.Repository.Model
{
  public interface IMessageRepository : IModelRepository<MessageModel, Int32>
  {
    Int32 Send(MessageModel entity);
    Int32 Send(String SenderFName, String SenderLName, String SenderEmail, String SenderMessage);
    Int32 Respond(MessageModel entity);
    Int32 Respond(Int32 ID, Int32 ResponderUserFK, String ResponderMessage);
  }
}
