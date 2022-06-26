using System.Collections.Generic;

using BLL.Projection;

namespace UI.Models
{
  public class AuthorVM
  {
    public AuthorProjection Author { get; set; }
    public IEnumerable<BookCardVM> Books { get; set; }
  }
}