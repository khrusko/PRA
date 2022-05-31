using DAL.Abstract.Repository;
using DAL.Model;
using DAL.Repository.Database;

namespace DAL.Factory
{
  public static class UserRepositoryFactory
  {
    public static IRepository<UserModel> GetRepository() => new UserDatabaseRepository();
  }
}
