using BLL.Projection;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UI.Models
{
  public class BookPublisherVM
  {
    public BookProjection Book { get; set; }
    public PublisherProjection Publisher { get; set; }
  }
}