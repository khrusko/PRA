using DAL.Abstract.Model;

namespace DAL.Model
{
  public class BookModel : AbstractModel<int>
  {
    public string ISBN { get; set; }
    public string Title { get; set; }
    public string Summary { get; set; }
    public string Description { get; set; }
    public bool IsNew { get; set; }
    public int PublisherFK { get; set; }
    public int PageCount { get; set; }
    public decimal PurchasePrice { get; set; }
    public decimal LoanPrice { get; set; }
    public int Quantity { get; set; }
    public string ImagePath { get; set; }
  }
}
