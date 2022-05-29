namespace DAL.Abstract.DAO
{
  public interface IIdentifiable<K>
  {
    K ID { get; set; }
  }
}
