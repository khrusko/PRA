using System;
using System.Collections.Generic;

using BLL.Projection;

using DAL.Model;

namespace BLL.Abstract.Manager.Projection
{
  public interface IPurchaseManager : IProjectionManager<PurchaseModel, PurchaseProjection, Int32>
  {
    Int32 Purchase(PurchaseProjection projection);
    Int32 Purchase(Int32 BookFK, Int32 UserFK, Int32 Quantity);
    IEnumerable<PurchaseProjection> GetByUserFK(Int32 UserFK);
  }
}
