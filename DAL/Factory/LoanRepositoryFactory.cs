﻿using DAL.Abstract.Repository;
using DAL.Model;
using DAL.Repository.Database;

namespace DAL.Factory
{
  public static class LoanRepositoryFactory
  {
    public static IRepository<LoanModel> GetRepository() => new LoanDatabaseRepository();
  }
}
