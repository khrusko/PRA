using System;

using BLL.Abstract.Projection;

namespace BLL.Projection
{
  public class PurchaseProjection : AbstractProjection<Int32>
  {
    public Int32 BookFK { get; set; }
    public Int32 UserFK { get; set; }
    public Int32 Quantity { get; set; }
    public Decimal UnitPrice { get; set; }
    public DateTime PurchaseDate { get; set; }
  }
}
