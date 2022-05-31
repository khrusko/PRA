using DAL.Abstract.DAO;

namespace DAL.Abstract.Repository.DAO
{
  public interface IDAORepository<T, K> : IIdentifiableRepository<T, K>
    where T : class, IDAO<K>
  {
    int Create(T entity, K CreatedBy);
    int Update(K ID, T entity, K UpdatedBy);
    int Delete(K ID, K DeletedBy);
  }
}
