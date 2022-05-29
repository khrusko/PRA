using DAL.Abstract.DAO;
using System;

namespace DAL.DAO
{
  public class PurchaseDAO : AbstractDAO<int>
  {
    public int BookFK { get; set; }
    public int UserFK { get; set; }
    public int Quantity { get; set; }
    public double UnitPrice { get; set; }
    public DateTime PurchaseDate { get; set; }
  }
}
