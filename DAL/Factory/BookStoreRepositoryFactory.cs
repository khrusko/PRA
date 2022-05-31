using DAL.Abstract.Repository;
using DAL.Model;
using DAL.Repository.Database;

namespace DAL.Factory
{
  public static class BookStoreRepositoryFactory
  {
    public static IRepository<BookStoreModel> GetRepository() => new BookStoreDatabaseRepository();
  }
}
