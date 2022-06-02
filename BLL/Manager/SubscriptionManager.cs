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
        BookFK = model.BookFK,
        UserFK = model.UserFK,
        SubscriptionDate = model.SubscriptionDate,
        IsResolved = model.IsResolved,
        ResolvedDate = model.ResolvedDate
      };

    public int Subscribe(SubscriptionProjection projection)
      => Subscribe(projection.BookFK, projection.UserFK);
    public int Subscribe(int BookFK, int UserFK)
      => (Repository as ISubscriptionRepository).Subscribe(BookFK, UserFK, UserFK);
  }
}
