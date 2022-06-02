using BLL.Abstract.Projection;
using DAL.Abstract.Model;
using System.Collections.Generic;

namespace BLL.Abstract.Manager.Projection
{
  public interface IProjectionManager<M, P, K> : IManager<M, K>
    where M : class, IModel<K>
    where P : class, IProjection<K>
  {
    P Project(M model);
    P GetByID(int ID);
    IEnumerable<P> GetAll();
  }
}
