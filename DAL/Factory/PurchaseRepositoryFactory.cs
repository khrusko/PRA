using DAL.Abstract.Repository;
using DAL.Model;
using DAL.Repository.Database;

namespace DAL.Factory
{
  public static class PurchaseRepositoryFactory
  {
    public static IRepository<PurchaseModel> GetRepository() => new PurchaseDatabaseRepository();
  }
}
