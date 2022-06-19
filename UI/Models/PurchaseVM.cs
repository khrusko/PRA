
using BLL.Projection;

namespace UI.Models
{
  public class PurchaseVM
  {
    public BookProjection Book { get; set; }
    public AuthorProjection Author { get; set; }
    public PurchaseProjection Purchase { get; set; }
  }
}