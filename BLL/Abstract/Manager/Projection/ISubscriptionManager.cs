using System;

using BLL.Projection;

using DAL.Model;

namespace BLL.Abstract.Manager.Projection
{
  public interface ISubscriptionManager : IProjectionManager<SubscriptionModel, SubscriptionProjection, Int32>
  {
    Int32 Subscribe(SubscriptionProjection projection);
    Int32 Subscribe(Int32 bookFK, Int32 userFK);
  }
}
