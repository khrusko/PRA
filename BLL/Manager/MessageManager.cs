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

    public int Send(MessageProjection projection)
      => Send(projection.SenderUserFK, projection.SenderMessage);
    public int Send(int SenderUserFK, string SenderMessage)
      => (Repository as IMessageRepository).Send(SenderUserFK, SenderMessage, SenderUserFK);

    public int Respond(MessageProjection projection)
      => Respond(projection.ID, projection.ResponderUserFK, projection.ResponderMessage);
    public int Respond(int ID, int ResponderUserFK, string ResponderMessage)
      => (Repository as IMessageRepository).Respond(ID, ResponderUserFK, ResponderMessage, ResponderUserFK);
  }
}
