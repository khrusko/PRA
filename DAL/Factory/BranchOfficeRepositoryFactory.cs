using DAL.Abstract.Repository;
using DAL.Model;
using DAL.Repository.Database;

namespace DAL.Factory
{
  public static class BranchOfficeRepositoryFactory
  {
    public static IRepository<BranchOfficeModel> GetRepository() => new BranchOfficeDatabaseRepository();
  }
}
