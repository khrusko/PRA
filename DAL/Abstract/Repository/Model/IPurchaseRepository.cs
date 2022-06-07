using System;

using DAL.Model;

namespace DAL.Abstract.Repository.Model
{
  public interface IPurchaseRepository : IModelRepository<PurchaseModel, Int32>
  {
    Int32 Purchase(PurchaseModel entity);
    Int32 Purchase(PurchaseModel entity, Int32 CreatedBy);
    Int32 Purchase(Int32 BookFK, Int32 UserFK, Int32 Quantity, Int32 CreatedBy);
  }
}
