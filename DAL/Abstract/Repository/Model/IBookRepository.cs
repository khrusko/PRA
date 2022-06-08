using System;
using System.Collections.Generic;

using DAL.Model;

namespace DAL.Abstract.Repository.Model
{
  public interface IBookRepository : IModelRepository<BookModel, Int32>
  {
    Int32 Create(BookModel entity, IEnumerable<Int32> Authors, Int32 CreatedBy);
    Int32 Update(BookModel entity, IEnumerable<Int32> Authors, Int32 UpdatedBy);
    Int32 Update(Int32 ID, BookModel entity, IEnumerable<Int32> Authors, Int32 UpdatedBy);
    IEnumerable<BookModel> ReadByAuthorFK(Int32 AuthorFK);
  }
}
