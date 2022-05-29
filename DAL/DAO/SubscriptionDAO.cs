using DAL.Abstract.DAO;
using System;

namespace DAL.DAO
{
  public class SubscriptionDAO : AbstractDAO<int>
  {
    public int BookFK { get; set; }
    public int UserFK { get; set; }
    public DateTime SubscriptionDate { get; set; }
    public bool IsResolved { get; set; }
    public DateTime ResolvedDate { get; set; }
  }
}
