using System;

using DAL.Abstract.Model;

namespace DAL.Model
{
  public class PurchaseModel : AbstractModel<Int32>
  {
    public Int32 BookFK { get; set; }
    public Int32 UserFK { get; set; }
    public Int32 Quantity { get; set; }
    public Decimal UnitPrice { get; set; }
    public DateTime PurchaseDate { get; set; }
  }
}
