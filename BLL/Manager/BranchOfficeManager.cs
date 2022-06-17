using System;
using System.Collections.Generic;
using System.Linq;

using BLL.Abstract.Manager.Projection;
using BLL.Projection;

using DAL.Abstract.Repository;
using DAL.Abstract.Repository.Model;
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
        IsAvailable = model.DeleteDate != DateTime.MinValue,
        Name = model.Name,
        Address = model.Address,
        Telephone = model.Telephone,
        Email = model.Email
      };

    public BranchOfficeModel Model(BranchOfficeProjection projection)
      => new BranchOfficeModel
      {
        ID = projection.ID,
        Name = projection.Name,
        Address = projection.Address,
        Telephone = projection.Telephone,
        Email = projection.Email
      };

    public BranchOfficeProjection GetByID(Int32 ID, Boolean availabilityCheck = true)
    {
      BranchOfficeModel model = availabilityCheck
        ? (Repository as IBranchOfficeRepository).ReadByIDAvailable(ID)
        : (Repository as IBranchOfficeRepository).ReadByID(ID);
      return model is null ? null : Project(model);
    }

    public IEnumerable<BranchOfficeProjection> GetAll(Boolean availabilityCheck = true)
      => availabilityCheck
      ? (Repository as IBranchOfficeRepository).ReadAllAvailable().Select(Project)
      : (Repository as IBranchOfficeRepository).ReadAll().Select(Project);

    public Int32 Remove(Int32 ID, Int32 deletedBy)
      => (Repository as IBranchOfficeRepository).Delete(ID, deletedBy);
  }
}
