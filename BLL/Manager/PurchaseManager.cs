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

    public PurchaseProjection GetByID(Int32 ID)
    {
      PurchaseModel model = (Repository as IPurchaseRepository).Read(ID);
      return model is null ? null : Project(model);
    }

    public IEnumerable<PurchaseProjection> GetAll()
      => (Repository as IPurchaseRepository).Read().Select(Project);

    public Int32 Purchase(PurchaseProjection projection)
      => Purchase(projection.BookFK, projection.UserFK, projection.Quantity);
    public Int32 Purchase(Int32 BookFK, Int32 UserFK, Int32 Quantity)
      => (Repository as IPurchaseRepository).Purchase(BookFK, UserFK, Quantity, UserFK);
  }
}
