using System;

using BLL.Projection;

using DAL.Model;

namespace BLL.Abstract.Manager.Projection
{
  public interface IBookStoreManager : IProjectionManager<BookStoreModel, BookStoreProjection, Int32>
  {
    BookStoreProjection GetBookStore();
    Int32 Update(BookStoreProjection projection, Int32 updatedBy);
  }
}
