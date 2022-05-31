using DAL.DAO;

namespace DAL.Abstract.Repository.DAO
{
  public interface IPurchaseRepository : IDAORepository<PurchaseDAO, int>
  {
    int Purchase(PurchaseDAO entity);
    int Purchase(PurchaseDAO entity, int CreatedBy);
    int Purchase(int BookFK, int UserFK, int Quantity, int CreatedBy);
  }
}
