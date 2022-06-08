using System;
using System.Collections.Generic;
using System.Linq;

using BLL.Abstract.Manager.Projection;
using BLL.Projection;

using DAL.Abstract.Repository;
using DAL.Abstract.Repository.Model;
using DAL.Factory;
using DAL.Model;

namespace BLL.Manager
{
  public class MessageManager : IMessageManager
  {
    public IRepository<MessageModel> Repository { get; } = MessageRepositoryFactory.GetRepository();

    public MessageProjection Project(MessageModel model)
      => new MessageProjection
      {
        ID = model.ID,
        SenderUserFK = model.SenderUserFK,
        SenderDate = model.SenderDate,
        SenderMessage = model.SenderMessage,
        ResponderUserFK = model.ResponderUserFK,
        ResponderDate = model.ResponderDate,
        ResponderMessage = model.ResponderMessage
      };

    public MessageProjection GetByID(Int32 ID)
    {
      MessageModel model = (Repository as IMessageRepository).Read(ID);
      return model is null ? null : Project(model);
    }

    public IEnumerable<MessageProjection> GetAll()
      => (Repository as IMessageRepository).Read().Select(Project);

    public Int32 Send(MessageProjection projection)
      => Send(projection.SenderUserFK, projection.SenderMessage);
    public Int32 Send(Int32 SenderUserFK, String SenderMessage)
      => (Repository as IMessageRepository).Send(SenderUserFK, SenderMessage, SenderUserFK);

    public Int32 Respond(MessageProjection projection)
      => Respond(projection.ID, projection.ResponderUserFK, projection.ResponderMessage);
    public Int32 Respond(Int32 ID, Int32 ResponderUserFK, String ResponderMessage)
      => (Repository as IMessageRepository).Respond(ID, ResponderUserFK, ResponderMessage, ResponderUserFK);
  }
}
