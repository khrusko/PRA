using System;
using System.Collections.Generic;
using System.Web;

using BLL.Projection;

using DAL.Model;

namespace BLL.Abstract.Manager.Projection
{
  public interface IBookManager : IProjectionManager<BookModel, BookProjection, Int32>
  {
    IEnumerable<BookProjection> GetBooksByAuthorFK(Int32 authorFK);
    Int32 Create(BookProjection projection, HttpPostedFileBase file, Int32 createdBy);
    Int32 Update(BookProjection projection, HttpPostedFileBase file, Int32 updatedBy);
  }
}
