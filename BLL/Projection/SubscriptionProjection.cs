using System;

using BLL.Abstract.Projection;

namespace BLL.Projection
{
  public class SubscriptionProjection : AbstractProjection<Int32>
  {
    public Int32 BookFK { get; set; }
    public Int32 UserFK { get; set; }
    public DateTime SubscriptionDate { get; set; }
    public Boolean IsResolved { get; set; }
    public DateTime ResolvedDate { get; set; }
  }
}
