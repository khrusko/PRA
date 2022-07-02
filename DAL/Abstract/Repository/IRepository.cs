using System;
using System.Collections.Generic;

namespace DAL.Abstract.Repository
{
  public interface IRepository<T> where T : class
  {
    IEnumerable<T> ReadAll();
    Int32 Update(T entity);
    Int32 Delete(T entity);
  }
}
