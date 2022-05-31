using DAL.Abstract.Repository;
using DAL.Model;
using DAL.Repository.Database;

namespace DAL.Factory
{
  public static class BookRepositoryFactory
  {
    public static IRepository<BookModel> GetRepository() => new BookDatabaseRepository();
  }
}
