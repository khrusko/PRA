using DAL.DAO;
using System.Collections.Generic;

namespace DAL.Abstract.Repository.DAO
{
  public interface IBookRepository : IDAORepository<BookDAO, int>
  {
    int Create(BookDAO entity, IEnumerable<int> Authors, int CreatedBy);
    int Update(BookDAO entity, IEnumerable<int> Authors, int UpdatedBy);
    int Update(int ID, BookDAO entity, IEnumerable<int> Authors, int UpdatedBy);
  }
}
