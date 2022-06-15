using System;

using DAL.Abstract.Model;

namespace DAL.Abstract.Repository
{
  public interface IIdentifiableRepository<T, K> : IRepository<T>
    where T : class, IIdentifiable<K>
  {
    K Create(T entity);
    T ReadByID(K ID);
    Int32 Update(K ID, T entity);
    Int32 Delete(K ID, T entity);
  }
}
