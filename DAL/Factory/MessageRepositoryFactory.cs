using DAL.Abstract.Repository;
using DAL.DAO;
using DAL.Repository.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Factory
{
  public static class MessageRepositoryFactory
  {
    public static IRepository<MessageDAO> GetRepository() => new MessageDatabaseRepository();
  }
}
