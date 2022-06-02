using BLL.Abstract.Projection;
using System;

namespace BLL.Projection
{
  public class SubscriptionProjection : AbstractProjection<int>
  {
    public int BookFK { get; set; }
    public int UserFK { get; set; }
    public DateTime SubscriptionDate { get; set; }
    public bool IsResolved { get; set; }
    public DateTime ResolvedDate { get; set; }
  }
}
