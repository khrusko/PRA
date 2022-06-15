using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using BLL.Projection;

namespace UI.Models
{
  public class FullBookInfoVM
  {
    public BookProjection Book { get; set; }
    public PublisherProjection Publisher { get; set; }
    public AuthorProjection Author { get; set; }
  }
}