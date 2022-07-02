using System;

using DAL.Model;

namespace DAL.Abstract.Repository.Model
{
  public interface IBookStoreRepository : IModelRepository<BookStoreModel, Int32>
  {
  }
}
