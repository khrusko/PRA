using DAL.Abstract.Model;
using System;

namespace DAL.Model
{
  public class PurchaseModel : AbstractModel<int>
  {
    public int BookFK { get; set; }
    public int UserFK { get; set; }
    public int Quantity { get; set; }
    public double UnitPrice { get; set; }
    public DateTime PurchaseDate { get; set; }
  }
}
