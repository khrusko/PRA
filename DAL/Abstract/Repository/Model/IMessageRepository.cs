using System;

using DAL.Model;

namespace DAL.Abstract.Repository.Model
{
  public interface IMessageRepository : IModelRepository<MessageModel, Int32>
  {
    Int32 Send(MessageModel entity);
    Int32 Send(MessageModel entity, Int32 CreatedBy);
    Int32 Send(Int32 SenderUserFK, String SenderMessage, Int32 CreatedBy);
    Int32 Respond(MessageModel entity);
    Int32 Respond(MessageModel entity, Int32 UpdatedBy);
    Int32 Respond(Int32 ID, Int32 ResponderUserFK, String ResponderMessage, Int32 UpdatedBy);
  }
}
