using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using BLL.Projection;

namespace UI.Models
{
  public class LoanVM
  {
    public BookProjection Book { get; set; }
    public BookStoreProjection Bookstore { get; set; }
    public LoanProjection Loan { get; set; }
    public AuthorProjection Author { get; set; }
  }
}