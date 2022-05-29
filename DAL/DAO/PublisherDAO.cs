using DAL.Abstract.DAO;

namespace DAL.DAO
{
  public class PublisherDAO : AbstractDAO<int>
  {
    public string Name { get; set; }
  }
}
