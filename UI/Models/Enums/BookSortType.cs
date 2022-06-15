using System.ComponentModel.DataAnnotations;

namespace UI.Models.Enums
{
  public enum BookSortType
  {
    [Display(Name = "Naslov knjige")]
    TITLE = 0,

    [Display(Name = "Naziv autora")]
    AUTHOR,

    [Display(Name = "Cijena kupnje")]
    PURCHASE_PRICE,
  }
}