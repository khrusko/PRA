using DAL.Model;
using System.Collections.Generic;

namespace DAL.Abstract.Repository.Model
{
  public interface IBookRepository : IModelRepository<BookModel, int>
  {
    int Create(BookModel entity, IEnumerable<int> Authors, int CreatedBy);
    int Update(BookModel entity, IEnumerable<int> Authors, int UpdatedBy);
    int Update(int ID, BookModel entity, IEnumerable<int> Authors, int UpdatedBy);
    IEnumerable<BookModel> ReadByAuthorFK(int AuthorFK);
  }
}
