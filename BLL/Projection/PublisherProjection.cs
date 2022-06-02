using BLL.Abstract.Projection;

namespace BLL.Projection
{
  public class PublisherProjection : AbstractProjection<int>
  {
    public string Name { get; set; }
  }
}
