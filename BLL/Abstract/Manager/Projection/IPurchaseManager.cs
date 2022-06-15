using System;
using System.Collections.Generic;

using BLL.Projection;

using DAL.Model;

namespace BLL.Abstract.Manager.Projection
{
  public interface IPurchaseManager : IProjectionManager<PurchaseModel, PurchaseProjection, Int32>
  {
    Int32 Purchase(PurchaseProjection projection);
    Int32 Purchase(Int32 bookFK, Int32 userFK, Int32 quantity);
    IEnumerable<PurchaseProjection> GetByUserFK(Int32 UserFK);
  }
}
