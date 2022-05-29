using System.Collections.Generic;

namespace DAL.Abstract.Repository
{
  public interface IRepository<T> where T : class
  {
    int Create(T entity);
    IEnumerable<T> ReadAll();
    int Update(T entity);
    int Delete(T entity);
  }
}
