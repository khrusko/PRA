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
  public class PublisherManager : IPublisherManager
  {
    public IRepository<PublisherModel> Repository { get; } = PublisherRepositoryFactory.GetRepository();

    public PublisherProjection Project(PublisherModel model)
      => new PublisherProjection
      {
        ID = model.ID,
        IsAvailable = model.DeleteDate != DateTime.MinValue,
        Name = model.Name
      };

    public PublisherModel Model(PublisherProjection projection)
      => new PublisherModel
      {
        ID = projection.ID,
        Name = projection.Name
      };

    public PublisherProjection GetByID(Int32 ID, Boolean availabilityCheck = true)
    {
      PublisherModel model = availabilityCheck
        ? (Repository as IPublisherRepository).ReadByIDAvailable(ID)
        : (Repository as IPublisherRepository).ReadByID(ID);
      return model is null ? null : Project(model);
    }

    public IEnumerable<PublisherProjection> GetAll(Boolean availabilityCheck = true)
      => availabilityCheck
      ? (Repository as IPublisherRepository).ReadAllAvailable().Select(Project)
      : (Repository as IPublisherRepository).ReadAll().Select(Project);

    public Int32 Remove(Int32 ID, Int32 deletedBy)
      => (Repository as IPublisherRepository).Delete(ID, deletedBy);
  }
}
