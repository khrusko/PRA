using System;

using DAL.Abstract.Model;

namespace DAL.Model
{
  public class BookModel : AbstractModel<Int32>
  {
    public String ISBN { get; set; }
    public String Title { get; set; }
    public String Summary { get; set; }
    public String Description { get; set; }
    public Boolean IsNew { get; set; }
    public Int32 PublisherFK { get; set; }
    public Int32 PageCount { get; set; }
    public Decimal PurchasePrice { get; set; }
    public Decimal LoanPrice { get; set; }
    public Int32 Quantity { get; set; }
    public String ImagePath { get; set; }
  }
}
