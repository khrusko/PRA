using DAL.Abstract.Model;
using System;

namespace DAL.Model
{
  public class SubscriptionModel : AbstractModel<int>
  {
    public int BookFK { get; set; }
    public int UserFK { get; set; }
    public DateTime SubscriptionDate { get; set; }
    public bool IsResolved { get; set; }
    public DateTime ResolvedDate { get; set; }
  }
}
