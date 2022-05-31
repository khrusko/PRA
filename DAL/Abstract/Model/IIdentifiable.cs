namespace DAL.Abstract.Model
{
  public interface IIdentifiable<K>
  {
    K ID { get; set; }
  }
}
