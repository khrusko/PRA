using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UI.Models
{
  public class LoginVM
  {

    [Display(Name = "E-mail")]
    [Required(ErrorMessage = "E-mail je obavezan")]
    [EmailAddress(ErrorMessage = "Nije valjani E-mail")]
    public string Email { get; set; }

    [Display(Name = "Zaporka")]
    [Required(ErrorMessage = "Zaporka je obavezna")]
    [DataType(DataType.Password)]
    public string Password { get; set; }

  }
}