using DAL.Abstract.Model;

namespace DAL.Abstract.Repository.Model
{
  public interface IModelRepository<T, K> : IIdentifiableRepository<T, K>
    where T : class, IModel<K>
  {
    int Create(T entity, K CreatedBy);
    int Update(K ID, T entity, K UpdatedBy);
    int Delete(K ID, K DeletedBy);
  }
}
