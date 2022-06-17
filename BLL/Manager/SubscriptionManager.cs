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
  public class SubscriptionManager : ISubscriptionManager
  {
    public IRepository<SubscriptionModel> Repository { get; } = SubscriptionRepositoryFactory.GetRepository();

    public SubscriptionProjection Project(SubscriptionModel model)
      => new SubscriptionProjection
      {
        ID = model.ID,
        IsAvailable = model.DeleteDate != DateTime.MinValue,
        BookFK = model.BookFK,
        UserFK = model.UserFK,
        SubscriptionDate = model.SubscriptionDate,
        IsResolved = model.IsResolved,
        ResolvedDate = model.ResolvedDate
      };

    public SubscriptionModel Model(SubscriptionProjection projection)
      => new SubscriptionModel
      {
        ID = projection.ID,
        BookFK = projection.BookFK,
        UserFK = projection.UserFK,
        SubscriptionDate = projection.SubscriptionDate,
        IsResolved = projection.IsResolved,
        ResolvedDate = projection.ResolvedDate
      };

    public SubscriptionProjection GetByID(Int32 ID, Boolean availabilityCheck = true)
    {
      SubscriptionModel model = availabilityCheck
        ? (Repository as ISubscriptionRepository).ReadByIDAvailable(ID)
        : (Repository as ISubscriptionRepository).ReadByID(ID);
      return model is null ? null : Project(model);
    }

    public IEnumerable<SubscriptionProjection> GetAll(Boolean availabilityCheck = true)
      => availabilityCheck
      ? (Repository as ISubscriptionRepository).ReadAllAvailable().Select(Project)
      : (Repository as ISubscriptionRepository).ReadAll().Select(Project);

    public Int32 Remove(Int32 ID, Int32 deletedBy) => throw new NotImplementedException();

    public Int32 Subscribe(SubscriptionProjection projection)
      => Subscribe(projection.BookFK, projection.UserFK);
    public Int32 Subscribe(Int32 bookFK, Int32 userFK)
      => (Repository as ISubscriptionRepository).Subscribe(bookFK, userFK, userFK);
  }
}
