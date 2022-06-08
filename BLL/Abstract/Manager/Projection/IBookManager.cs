using System;
using System.Collections.Generic;

using BLL.Projection;

using DAL.Model;

namespace BLL.Abstract.Manager.Projection
{
  public interface IBookManager : IProjectionManager<BookModel, BookProjection, Int32>
  {
    IEnumerable<BookProjection> GetBooksByAuthorFK(Int32 AuthorFK);
  }
}
