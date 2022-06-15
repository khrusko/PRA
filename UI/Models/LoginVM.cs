using System;
using System.ComponentModel.DataAnnotations;

namespace UI.Models
{
  public class LoginVM
  {
    [Display(Name = "E-mail")]
    [Required(ErrorMessage = "E-mail je obavezan")]
    [EmailAddress(ErrorMessage = "Nije valjani E-mail")]
    public String Email { get; set; }

    [Display(Name = "Zaporka")]
    [Required(ErrorMessage = "Zaporka je obavezna")]
    [DataType(DataType.Password)]
    public String Password { get; set; }
  }
}