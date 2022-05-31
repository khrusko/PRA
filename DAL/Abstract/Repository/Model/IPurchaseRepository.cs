using DAL.Model;

namespace DAL.Abstract.Repository.Model
{
  public interface IPurchaseRepository : IModelRepository<PurchaseModel, int>
  {
    int Purchase(PurchaseModel entity);
    int Purchase(PurchaseModel entity, int CreatedBy);
    int Purchase(int BookFK, int UserFK, int Quantity, int CreatedBy);
  }
}
