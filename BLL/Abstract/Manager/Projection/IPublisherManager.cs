using BLL.Projection;
using DAL.Model;

namespace BLL.Abstract.Manager.Projection
{
  public interface IPublisherManager : IProjectionManager<PublisherModel, PublisherProjection, int>
  {
    PublisherProjection GetPublisherByID(int ID);
  }
}
