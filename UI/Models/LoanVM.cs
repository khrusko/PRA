
using BLL.Projection;

namespace UI.Models
{
  public class LoanVM
  {
    public LoanProjection Loan { get; set; }
    public BookProjection Book { get; set; }
    public UserProjection User { get; set; }
  }
}