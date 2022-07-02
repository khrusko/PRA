using System;

using BLL.Abstract.Projection;

namespace BLL.Projection
{
  public class PublisherProjection : AbstractProjection<Int32>
  {
    public String Name { get; set; }
  }
}
