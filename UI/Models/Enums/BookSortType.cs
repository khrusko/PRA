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

    [Display(Name = "Datum objave")]
    CREATE_DATE,

    [Display(Name = "Broj stranica")]
    PAGE_COUNT,

    [Display(Name = "Broj posudbi")]
    LOAN_COUNT
  }
}