using System;
using System.ComponentModel.DataAnnotations;

namespace UI.Models
{
  public class ResetPasswordVM
  {
    public Guid GUID { get; set; }

    [Display(Name = "Zaporka")]
    [Required(ErrorMessage = "Zaporka je obavezna")]
    [DataType(DataType.Password)]
    public String Password { get; set; }

    [Display(Name = "Ponovi zaporku")]
    [Required(ErrorMessage = "Polje Ponovi Zaporku je obavezno")]
    [Compare("Password", ErrorMessage = "Zaporke se ne podudaraju")]
    [DataType(DataType.Password)]
    public String ConfirmPassword { get; set; }
  }
}