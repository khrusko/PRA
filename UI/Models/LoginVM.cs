using System;
using System.ComponentModel.DataAnnotations;

namespace UI.Models
{
  public class LoginVM
  {
    [Display(Name = "Email")]
    [Required(ErrorMessage = "Email je obavezan")]
    [EmailAddress(ErrorMessage = "Nije valjani Email")]
    public String Email { get; set; }

    [Display(Name = "Zaporka")]
    [Required(ErrorMessage = "Zaporka je obavezna")]
    [DataType(DataType.Password)]
    public String Password { get; set; }
  }
}