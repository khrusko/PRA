using System.Collections.Generic;

using BLL.Projection;

namespace UI.Models
{
  public class HomeVM
  {
    public IEnumerable<BookCardVM> Books { get; set; }

    public BookStoreProjection BookStore { get; set; }
  }
}