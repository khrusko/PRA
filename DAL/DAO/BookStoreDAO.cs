using DAL.Abstract.DAO;

namespace DAL.DAO
{
  public class BookStoreDAO : AbstractDAO<int>
  {
    public string Name { get; set; }
    public string OIB { get; set; }
    public double DelayPricePerDay { get; set; }
    public string Email { get; set; }
  }
}
