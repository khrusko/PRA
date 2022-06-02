using BLL.Projection;
using DAL.Model;

namespace BLL.Abstract.Manager.Projection
{
  public interface IMessageManager : IProjectionManager<MessageModel, MessageProjection, int>
  {
    int Send(MessageProjection projection);
    int Send(int SenderUserFK, string SenderMessage);
    int Respond(MessageProjection projection);
    int Respond(int ID, int ResponderUserFK, string ResponderMessage);
  }
}
