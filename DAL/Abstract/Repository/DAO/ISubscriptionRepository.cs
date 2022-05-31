using DAL.DAO;

namespace DAL.Abstract.Repository.DAO
{
  public interface ISubscriptionRepository : IDAORepository<SubscriptionDAO, int>
  {
    int Subscribe(SubscriptionDAO entity);
    int Subscribe(SubscriptionDAO entity, int CreatedBy);
    int Subscribe(int BookFK, int UserFK, int CreatedBy);
  }
}
