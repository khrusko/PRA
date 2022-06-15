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
  public class PurchaseManager : IPurchaseManager
  {
    public IRepository<PurchaseModel> Repository { get; } = PurchaseRepositoryFactory.GetRepository();

    public PurchaseProjection Project(PurchaseModel model)
      => new PurchaseProjection
      {
        ID = model.ID,
        BookFK = model.BookFK,
        UserFK = model.UserFK,
        Quantity = model.Quantity,
        UnitPrice = model.UnitPrice,
        PurchaseDate = model.PurchaseDate
      };

    public PurchaseModel Model(PurchaseProjection projection)
      => new PurchaseModel
      {
        ID = projection.ID,
        BookFK = projection.BookFK,
        UserFK = projection.UserFK,
        Quantity = projection.Quantity,
        UnitPrice = projection.UnitPrice,
        PurchaseDate = projection.PurchaseDate
      };

    public PurchaseProjection GetByID(Int32 ID, Boolean availabilityCheck = true)
    {
      PurchaseModel model = availabilityCheck
        ? (Repository as IPurchaseRepository).ReadByIDAvailable(ID)
        : (Repository as IPurchaseRepository).ReadByID(ID);
      return model is null ? null : Project(model);
    }

    public IEnumerable<PurchaseProjection> GetAll(Boolean availabilityCheck = true)
      => availabilityCheck
      ? (Repository as IPurchaseRepository).ReadAllAvailable().Select(Project)
      : (Repository as IPurchaseRepository).ReadAll().Select(Project);

    public Int32 Remove(Int32 ID, Int32 deletedBy) => throw new NotImplementedException();

    public Int32 Purchase(PurchaseProjection projection)
      => Purchase(projection.BookFK, projection.UserFK, projection.Quantity);
    public Int32 Purchase(Int32 bookFK, Int32 userFK, Int32 quantity)
      => (Repository as IPurchaseRepository).Purchase(bookFK, userFK, quantity, userFK);

    public IEnumerable<PurchaseProjection> GetByUserFK(Int32 UserFK)
      => (Repository as IPurchaseRepository).ReadByUserFK(UserFK).Select(Project);
  }
}
