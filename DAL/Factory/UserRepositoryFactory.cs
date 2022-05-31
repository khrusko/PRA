using DAL.Abstract.Repository;
using DAL.Model;
using DAL.Repository.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Factory
{
  public static class UserRepositoryFactory
  {
    public static IRepository<UserModel> GetRepository() => new UserDatabaseRepository();
  }
}
