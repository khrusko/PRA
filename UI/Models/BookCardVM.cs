using System;

using BLL.Projection;

namespace UI.Models
{
  public class BookCardVM
  {
    public BookProjection Book { get; set; }
    public String Author { get; set; }
  }
}