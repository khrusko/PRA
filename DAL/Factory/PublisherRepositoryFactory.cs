using DAL.Abstract.Repository;
using DAL.Model;
using DAL.Repository.Database;

namespace DAL.Factory
{
  public static class PublisherRepositoryFactory
  {
    public static IRepository<PublisherModel> GetRepository() => new PublisherDatabaseRepository();
  }
}
