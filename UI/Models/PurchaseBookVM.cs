using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using BLL.Projection;

namespace UI.Models
{
  public class PurchaseBookVM
  {
    public BookProjection Book { get; set; }
    public PurchaseProjection Purchase { get; set; }
    public AuthorProjection Author { get; set; }
  }
}