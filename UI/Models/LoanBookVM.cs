using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using BLL.Projection;

namespace UI.Models
{
  public class LoanBookVM
  {
    public BookProjection Book { get; set; }
    public LoanProjection Loan { get; set; }
    public AuthorProjection Author { get; set; }
  }
}