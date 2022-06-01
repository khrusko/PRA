using DAL.Abstract.Model;
using DAL.Abstract.Repository;

namespace BLL.Abstract.Manager
{
  public interface IManager<M, K>
    where M : class, IModel<K>
  {
    IRepository<M> Repository { get; }
  }
}
