using DAL.Abstract.Model;
using DAL.Abstract.Repository.Model;
using System.Collections.Generic;
using System.Data;

namespace DAL.Abstract.Repository.Database
{
  internal interface IDatabaseRepository<T, K> : IIdentifiableRepository<T, K>
    where T : class, IIdentifiable<K>
  {
    string ConnectionString { get; }
    string EntityName { get; }
    IDictionary<string, SqlDbType> DbKeyTypePairs { get; }
  }
}
