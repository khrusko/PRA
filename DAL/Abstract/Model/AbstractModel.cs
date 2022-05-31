using System;

namespace DAL.Abstract.Model
{
  public abstract class AbstractModel<K> : IModel<K>
  {
    public K ID { get; set; }
    public DateTime CreateDate { get; set; }
    public K CreatedBy { get; set; }
    public DateTime UpdateDate { get; set; }
    public K UpdatedBy { get; set; }
    public DateTime DeleteDate { get; set; }
    public K DeletedBy { get; set; }
  }
}
