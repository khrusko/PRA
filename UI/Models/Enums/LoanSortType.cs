using System.ComponentModel.DataAnnotations;

namespace UI.Models.Enums
{
  public enum LoanSortType
  {
    [Display(Name = "Datum posudbe")]
    LOAN_DATE = 0,

    [Display(Name = "Planirani datum vaćanja")]
    PLANNED_RETURN_DATE = 1,
  }
}