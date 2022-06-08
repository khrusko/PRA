using System;

using DAL.Abstract.Model;

namespace DAL.Model
{
  public class BookStoreModel : AbstractModel<Int32>
  {
    public String Name { get; set; }
    public String OIB { get; set; }
    public Decimal DelayPricePerDay { get; set; }
    public String Email { get; set; }
  }
}
