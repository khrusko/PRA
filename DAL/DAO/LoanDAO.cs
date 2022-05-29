using DAL.Abstract.DAO;
using System;

namespace DAL.DAO
{
  public class LoanDAO : AbstractDAO<int>
  {
    public int BookFK { get; set; }
    public int UserFK { get; set; }
    public double LoanPrice { get; set; }
    public DateTime LoanDate { get; set; }
    public DateTime PlannedReturnDate { get; set; }
    public DateTime ReturnDate { get; set; }
    public int DelayDays { get; set; }
    public double DelayPricePerDay { get; set; }
  }
}
