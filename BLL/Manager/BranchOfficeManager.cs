using BLL.Abstract.Manager.Projection;
using BLL.Projection;
using DAL.Abstract.Repository;
using DAL.Abstract.Repository.Model;
using DAL.Factory;
using DAL.Model;
using System.Collections.Generic;
using System.Linq;

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

    public BranchOfficeProjection GetByID(int ID)
    {
      BranchOfficeModel model = (Repository as IBranchOfficeRepository).Read(ID);
      return model is null ? null : Project(model);
    }

    public IEnumerable<BranchOfficeProjection> GetAll()
      => (Repository as IBranchOfficeRepository).Read().Select(Project);
  }
}
