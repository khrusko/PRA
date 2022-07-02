using System;
using System.Collections.Generic;
using System.Data;

using DAL.Abstract.Model;

namespace DAL.Abstract.Repository.Database
{
  internal interface IDatabaseRepository<T, K> : IIdentifiableRepository<T, K>
    where T : class, IIdentifiable<K>
  {
    String ConnectionString { get; }
    String EntityName { get; }
    IDictionary<String, SqlDbType> DbKeyTypePairs { get; }
  }
}
