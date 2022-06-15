using System.Collections.Generic;

using BLL.Projection;

namespace UI.Models
{
  public class HomeVM
  {
    public IEnumerable<FullBookInfoVM> Books { get; set; }

    public BookStoreProjection BookStore { get; set; }
  }
}