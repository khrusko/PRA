using DAL.Model;

namespace DAL.Abstract.Repository.Model
{
  public interface ISubscriptionRepository : IModelRepository<SubscriptionModel, int>
  {
    int Subscribe(SubscriptionModel entity);
    int Subscribe(SubscriptionModel entity, int CreatedBy);
    int Subscribe(int BookFK, int UserFK, int CreatedBy);
  }
}
