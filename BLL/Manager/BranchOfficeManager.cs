using BLL.Abstract.Manager.Projection;
using BLL.Projection;
using DAL.Abstract.Repository;
using DAL.Factory;
using DAL.Model;

namespace BLL.Manager
{
  public class BranchOfficeManager : IBranchOfficeManager
  {
    public IRepository<BranchOfficeModel> Repository { get; } = BranchOfficeRepositoryFactory.GetRepository();

    public BranchOfficeProjection Project(BranchOfficeModel model)
      => new BranchOfficeProjection
      {
        ID = model.ID,
        Name = model.Name,
        Address = model.Address,
        Telephone = model.Telephone,
        Email = model.Email
      };
  }
}
