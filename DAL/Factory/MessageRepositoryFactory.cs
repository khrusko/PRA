using DAL.Abstract.Repository;
using DAL.Model;
using DAL.Repository.Database;

namespace DAL.Factory
{
  public static class MessageRepositoryFactory
  {
    public static IRepository<MessageModel> GetRepository() => new MessageDatabaseRepository();
  }
}
