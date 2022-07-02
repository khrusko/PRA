using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using UI.Models.Enums;

namespace UI.Models
{
  public class PurchaseSearchVM
  {
    public IEnumerable<PurchaseVM> Purchases { get; set; }
    public PagingInfo PagingInfo { get; set; }

    [Display(Name = "Sortiraj po")]
    public PurchaseSortType PurchaseSortType { get; set; }

    [Display(Name = "Smjer sortiranja")]
    public SortDirection SortDirection { get; set; }
  }
}