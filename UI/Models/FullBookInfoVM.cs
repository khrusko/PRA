using System;

using BLL.Projection;

namespace UI.Models
{
  public class FullBookInfoVM
  {
    public BookProjection Book { get; set; }
    public PublisherProjection Publisher { get; set; }
    public AuthorProjection Author { get; set; }
    public Int32 LoanCount { get; set; }
  }
}