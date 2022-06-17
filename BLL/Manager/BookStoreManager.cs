using System;
using System.Collections.Generic;
using System.Linq;

using BLL.Abstract.Manager.Projection;
using BLL.Projection;

using DAL.Abstract.Repository;
using DAL.Abstract.Repository.Model;
using DAL.Factory;
using DAL.Model;

namespace BLL.Manager
{
  public class BookStoreManager : IBookStoreManager
  {
    public IRepository<BookStoreModel> Repository { get; } = BookStoreRepositoryFactory.GetRepository();

    public BookStoreProjection Project(BookStoreModel model)
      => new BookStoreProjection
      {
        ID = model.ID,
        IsAvailable = model.DeleteDate != DateTime.MinValue,
        Name = model.Name,
        OIB = model.OIB,
        DelayPricePerDay = model.DelayPricePerDay,
        Email = model.Email
      };

    public BookStoreModel Model(BookStoreProjection projection)
      => new BookStoreModel
      {
        ID = projection.ID,
        Name = projection.Name,
        OIB = projection.OIB,
        DelayPricePerDay = projection.DelayPricePerDay,
        Email = projection.Email
      };

    public BookStoreProjection GetByID(Int32 ID, Boolean availabilityCheck = true)
      => throw new NotImplementedException();

    public IEnumerable<BookStoreProjection> GetAll(Boolean availabilityCheck = true)
      => throw new NotImplementedException();

    public Int32 Remove(Int32 ID, Int32 deletedBy) => throw new NotImplementedException();

    public BookStoreProjection GetBookStore()
    {
      BookStoreModel model = (Repository as IBookStoreRepository).ReadAll().First();
      return model is null ? null : Project(model);
    }

    public Int32 Update(BookStoreProjection projection, Int32 updatedBy)
      => (Repository as IBookStoreRepository).Update(projection.ID, Model(projection), updatedBy);
  }
}
