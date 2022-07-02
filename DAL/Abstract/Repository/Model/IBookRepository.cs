using System;
using System.Collections.Generic;

using DAL.Model;

namespace DAL.Abstract.Repository.Model
{
  public interface IBookRepository : IModelRepository<BookModel, Int32>
  {
    IEnumerable<BookModel> ReadByAuthorFK(Int32 AuthorFK);
  }
}
