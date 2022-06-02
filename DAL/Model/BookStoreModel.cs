using DAL.Abstract.Model;

namespace DAL.Model
{
  public class BookStoreModel : AbstractModel<int>
  {
    public string Name { get; set; }
    public string OIB { get; set; }
    public decimal DelayPricePerDay { get; set; }
    public string Email { get; set; }
  }
}
