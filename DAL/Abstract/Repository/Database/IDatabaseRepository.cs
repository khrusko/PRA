using DAL.Abstract.DAO;
using DAL.Abstract.Repository.DAO;

namespace DAL.Abstract.Repository.Database
{
  internal interface IDatabaseRepository<T, K> : IDAORepository<T, K>
    where T : class, IDAO<K>
  {
    string ConnectionString { get; }
  }
}
