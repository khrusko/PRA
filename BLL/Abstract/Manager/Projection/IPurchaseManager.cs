using BLL.Projection;
using DAL.Model;

namespace BLL.Abstract.Manager.Projection
{
  public interface IPurchaseManager : IProjectionManager<PurchaseModel, PurchaseProjection, int>
  {
    int Purchase(PurchaseProjection projection);
    int Purchase(int BookFK, int UserFK, int Quantity);
  }
}
