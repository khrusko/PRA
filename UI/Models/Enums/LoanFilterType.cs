using System.ComponentModel.DataAnnotations;

namespace UI.Models.Enums
{
  public enum LoanFilterType
  {
    [Display(Name = "Bez filtra")]
    NONE = 0,

    [Display(Name = "Valjane posudbe")]
    DELAY_DAYS_SUCCESS = 1,

    [Display(Name = "Posudbe s blizim istekom roka")]
    DELAY_DAYS_WARNING = 2,

    [Display(Name = "Istekle posudbe")]
    DELAY_DAYS_DANGER = 3,
  }
}