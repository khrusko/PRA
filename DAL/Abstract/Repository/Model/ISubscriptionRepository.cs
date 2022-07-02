using System;
using System.Collections.Generic;

using DAL.Model;

namespace DAL.Abstract.Repository.Model
{
  public interface ISubscriptionRepository : IModelRepository<SubscriptionModel, Int32>
  {
    Int32 Subscribe(SubscriptionModel entity);
    Int32 Subscribe(SubscriptionModel entity, Int32 CreatedBy);
    Int32 Subscribe(Int32 BookFK, Int32 UserFK, Int32 CreatedBy);
    IEnumerable<SubscriptionModel> ReadAllUnresolved();
    Int32 Resolve(SubscriptionModel entity);
    Int32 Resolve(Int32 ID);
  }
}
