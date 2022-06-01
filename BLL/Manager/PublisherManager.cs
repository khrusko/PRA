using BLL.Abstract.Manager.Projection;
using BLL.Projection;
using DAL.Abstract.Repository;
using DAL.Abstract.Repository.Model;
using DAL.Factory;
using DAL.Model;

namespace BLL.Manager
{
  public class PublisherManager : IPublisherManager
  {
    public IRepository<PublisherModel> Repository { get; } = PublisherRepositoryFactory.GetRepository();

    public PublisherProjection Project(PublisherModel model)
      => new PublisherProjection
      {
        ID = model.ID,
        Name = model.Name
      };

    public PublisherProjection GetPublisherByID(int ID)
    {
      PublisherModel model = (Repository as IPublisherRepository).Read(ID);
      return model is null ? null : Project(model);
    }
  }
}
