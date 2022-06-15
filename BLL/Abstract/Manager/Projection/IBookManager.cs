using System;
using System.Collections.Generic;
using System.Web;

using BLL.Projection;

using DAL.Model;

namespace BLL.Abstract.Manager.Projection
{
  public interface IBookManager : IProjectionManager<BookModel, BookProjection, Int32>
  {
    IEnumerable<BookProjection> GetBooksByAuthorFK(Int32 AuthorFK);
    Int32 Create(BookProjection projection, HttpPostedFileBase Image, IEnumerable<Int32> Authors, Int32 CreatedBy);
    Int32 Update(BookProjection projection, HttpPostedFileBase Image, IEnumerable<Int32> Authors, Int32 UpdatedBy);
  }
}
