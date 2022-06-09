using System;
using System.Collections.Generic;

using BLL.Abstract.Projection;

using DAL.Abstract.Model;

namespace BLL.Abstract.Manager.Projection
{
  public interface IProjectionManager<M, P, K> : IManager<M, K>
    where M : class, IModel<K>
    where P : class, IProjection<K>
  {
    P Project(M model);
    M Model(P projection);
    P GetByID(Int32 ID);
    IEnumerable<P> GetAll();
    Int32 Remove(K ID, K DeletedBy);
  }
}
