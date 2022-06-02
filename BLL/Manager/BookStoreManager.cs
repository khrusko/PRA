﻿using BLL.Abstract.Manager.Projection;
using BLL.Projection;
using DAL.Abstract.Repository;
using DAL.Abstract.Repository.Model;
using DAL.Factory;
using DAL.Model;
using System.Linq;

namespace BLL.Manager
{
  public class BookStoreManager : IBookStoreManager
  {
    public IRepository<BookStoreModel> Repository { get; } = BookStoreRepositoryFactory.GetRepository();

    public BookStoreProjection Project(BookStoreModel model)
      => new BookStoreProjection
      {
        ID = model.ID,
        Name = model.Name,
        OIB = model.OIB,
        DelayPricePerDay = model.DelayPricePerDay,
        Email = model.Email
      };

    public BookStoreProjection GetBookStore()
    {
      BookStoreModel model = (Repository as IBookStoreRepository).Read().First();
      return model is null ? null : Project(model);
    }
  }
}