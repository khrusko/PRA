
using BLL.Projection;

namespace UI.Models
{
  public class PurchaseVM
  {
    public PurchaseProjection Purchase { get; set; }
    public BookProjection Book { get; set; }
    public UserProjection User { get; set; }
  }
}