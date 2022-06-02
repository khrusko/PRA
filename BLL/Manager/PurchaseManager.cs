﻿using BLL.Abstract.Manager.Projection;
using BLL.Projection;
using DAL.Abstract.Repository;
using DAL.Abstract.Repository.Model;
using DAL.Factory;
using DAL.Model;

namespace BLL.Manager
{
  public class PurchaseManager : IPurchaseManager
  {
    public IRepository<PurchaseModel> Repository { get; } = PurchaseRepositoryFactory.GetRepository();

    public PurchaseProjection Project(PurchaseModel model)
      => new PurchaseProjection
      {
        ID = model.ID,
        BookFK = model.BookFK,
        UserFK = model.UserFK,
        Quantity = model.Quantity,
        UnitPrice = model.UnitPrice,
        PurchaseDate = model.PurchaseDate
      };

    public int Purchase(PurchaseProjection projection)
      => Purchase(projection.BookFK, projection.UserFK, projection.Quantity);
    public int Purchase(int BookFK, int UserFK, int Quantity)
      => (Repository as IPurchaseRepository).Purchase(BookFK, UserFK, Quantity, UserFK);
  }
}
