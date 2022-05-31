using DAL.DAO;

namespace DAL.Abstract.Repository.DAO
{
  public interface IMessageRepository : IDAORepository<MessageDAO, int>
  {
    int Send(MessageDAO entity);
    int Send(MessageDAO entity, int CreatedBy);
    int Send(int SenderUserFK, string SenderMessage, int CreatedBy);
    int Respond(MessageDAO entity);
    int Respond(MessageDAO entity, int UpdatedBy);
    int Respond(int ID, int ResponderUserFK, string ResponderMessage, int UpdatedBy);
  }
}
