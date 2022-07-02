using System;
using System.Collections.Generic;

using DAL.Abstract.Model;

namespace DAL.Abstract.Repository.Model
{
  public interface IModelRepository<T, K> : IIdentifiableRepository<T, K>
    where T : class, IModel<K>
  {
    K Create(T entity, K CreatedBy);
    IEnumerable<T> ReadAllAvailable();
    T ReadByIDAvailable(K ID);
    Int32 Update(K ID, T entity, K UpdatedBy);
    Int32 Delete(K ID, K DeletedBy);
  }
}
