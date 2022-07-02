using System;

using BLL.Abstract.Projection;

namespace BLL.Projection
{
  public class BookStoreProjection : AbstractProjection<Int32>
  {
    public String Name { get; set; }
    public String OIB { get; set; }
    public Decimal DelayPricePerDay { get; set; }
    public String Address { get; set; }
    public String Telephone { get; set; }
    public String Mobile { get; set; }
    public String Email { get; set; }
  }
}
