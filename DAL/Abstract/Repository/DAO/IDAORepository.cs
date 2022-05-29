using DAL.Abstract.DAO;

namespace DAL.Abstract.Repository.DAO
{
  public interface IDAORepository<T, K> : IIdentifiableRepository<T, K>
    where T : class, IDAO<K>
  {
  }
}
