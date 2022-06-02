namespace BLL.Abstract.Projection
{
  public abstract class AbstractProjection<K> : IProjection<K>
  {
    public K ID { get; set; }
  }
}
