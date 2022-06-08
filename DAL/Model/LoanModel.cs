using System;

using DAL.Abstract.Model;

namespace DAL.Model
{
  public class LoanModel : AbstractModel<Int32>
  {
    public Int32 BookFK { get; set; }
    public Int32 UserFK { get; set; }
    public Decimal LoanPrice { get; set; }
    public DateTime LoanDate { get; set; }
    public DateTime PlannedReturnDate { get; set; }
    public DateTime ReturnDate { get; set; }
    public Int32 DelayDays { get; set; }
    public Decimal DelayPricePerDay { get; set; }
  }
}
