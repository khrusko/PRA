using DAL.Abstract.Repository;
using DAL.Model;
using DAL.Repository.Database;

namespace DAL.Factory
{
  public static class AuthorRepositoryFactory
  {
    public static IRepository<AuthorModel> GetRepository() => new AuthorDatabaseRepository();
  }
}
