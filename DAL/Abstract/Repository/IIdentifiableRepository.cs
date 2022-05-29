using DAL.Abstract.DAO;

namespace DAL.Abstract.Repository
{
  public interface IIdentifiableRepository<T, K> : IRepository<T>
    where T : class, IIdentifiable<K>
  {
    T Read(K ID);
    int Update(K ID, T entity);
    int Delete(K ID);
  }
}
