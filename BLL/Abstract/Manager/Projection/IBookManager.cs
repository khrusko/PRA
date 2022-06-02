using BLL.Projection;
using DAL.Model;
using System.Collections.Generic;

namespace BLL.Abstract.Manager.Projection
{
  public interface IBookManager : IProjectionManager<BookModel, BookProjection, int>
  {
    IEnumerable<BookProjection> GetBooksByAuthorFK(int AuthorFK);
  }
}
