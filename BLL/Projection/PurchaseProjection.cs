using BLL.Abstract.Projection;
using System;

namespace BLL.Projection
{
  public class PurchaseProjection : AbstractProjection<int>
  {
    public int BookFK { get; set; }
    public int UserFK { get; set; }
    public int Quantity { get; set; }
    public double UnitPrice { get; set; }
    public DateTime PurchaseDate { get; set; }
  }
}
