using System.ComponentModel.DataAnnotations;

namespace UI.Models.Enums
{
  public enum PurchaseSortType
  {
    [Display(Name = "Datum kupnje")]
    DATE = 0,

    [Display(Name = "Ukupna cijena")]
    TOTAL_PRICE = 1,

    [Display(Name = "Količina")]
    QUANTITY = 2,
  }
}