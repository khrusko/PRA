using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace UI.Models
{
  public class BookSearchVM
  {
    public IEnumerable<FullBookInfoVM> BookPublishers { get; set; }
    public PagingInfo PagingInfo { get; set; }

    [Display(Name = "Prikaži samo dostupne knjige")]
    public Boolean AvailableOnly { get; set; }

    public String AuthorQuery { get; set; }
    public String BookQuery { get; set; }
  }
}