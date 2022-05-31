using DAL.Model;

namespace DAL.Abstract.Repository.Model
{
  public interface IMessageRepository : IModelRepository<MessageModel, int>
  {
    int Send(MessageModel entity);
    int Send(MessageModel entity, int CreatedBy);
    int Send(int SenderUserFK, string SenderMessage, int CreatedBy);
    int Respond(MessageModel entity);
    int Respond(MessageModel entity, int UpdatedBy);
    int Respond(int ID, int ResponderUserFK, string ResponderMessage, int UpdatedBy);
  }
}
