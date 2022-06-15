using System.ComponentModel.DataAnnotations;

namespace UI.Models.Enums
{
  public enum SortDirection
  {
    [Display(Name = "Rastuće")]
    ASCENDING = 0,

    [Display(Name = "Padajuće")]
    DESCENDING
  }
}