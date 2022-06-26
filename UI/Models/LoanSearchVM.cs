using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using UI.Models.Enums;

namespace UI.Models
{
  public class LoanSearchVM
  {
    public IEnumerable<LoanVM> Loans { get; set; }
    public PagingInfo PagingInfo { get; set; }

    [Display(Name = "Filtriraj po")]
    public LoanFilterType LoanFilterType { get; set; }

    [Display(Name = "Sortiraj po")]
    public LoanSortType LoanSortType { get; set; }

    [Display(Name = "Smjer sortiranja")]
    public SortDirection SortDirection { get; set; }
  }
}