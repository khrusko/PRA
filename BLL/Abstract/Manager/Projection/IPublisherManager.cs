using System;

using BLL.Projection;

using DAL.Model;

namespace BLL.Abstract.Manager.Projection
{
  public interface IPublisherManager : IProjectionManager<PublisherModel, PublisherProjection, Int32>
  {
  }
}
