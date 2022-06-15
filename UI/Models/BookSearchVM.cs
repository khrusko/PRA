using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

using UI.Models.Enums;

namespace UI.Models
{
  public class BookSearchVM
  {
    public IEnumerable<FullBookInfoVM> BookPublishers { get; set; }
    public PagingInfo PagingInfo { get; set; }

    [Display(Name = "Prikaži samo dostupne knjige")]
    public Boolean AvailableOnly { get; set; }

    [Display(Name = "Naziv autora")]
    public String AuthorQuery { get; set; }

    [Display(Name = "Naziv knjige")]
    public String BookQuery { get; set; }

    [Display(Name = "Sortiraj po")]
    public BookSortType BookSortType { get; set; }

    [Display(Name = "Smjer sortiranja")]
    public SortDirection SortDirection { get; set; }
  }
}