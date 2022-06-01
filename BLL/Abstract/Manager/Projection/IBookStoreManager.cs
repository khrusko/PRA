using BLL.Projection;
using DAL.Model;

namespace BLL.Abstract.Manager.Projection
{
  public interface IBookStoreManager : IProjectionManager<BookStoreModel, BookStoreProjection, int>
  {
    BookStoreProjection GetBookStore();
  }
}
