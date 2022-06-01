using BLL.Projection;
using DAL.Model;

namespace BLL.Abstract.Manager.Projection
{
  public interface ISubscriptionManager : IProjectionManager<SubscriptionModel, SubscriptionProjection, int>
  {
    int Subscribe(SubscriptionProjection projection);
    int Subscribe(int BookFK, int UserFK);
  }
}
