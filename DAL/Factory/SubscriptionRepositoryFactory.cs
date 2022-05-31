using DAL.Abstract.Repository;
using DAL.Model;
using DAL.Repository.Database;

namespace DAL.Factory
{
  public static class SubscriptionRepositoryFactory
  {
    public static IRepository<SubscriptionModel> GetRepository() => new SubscriptionDatabaseRepository();
  }
}
