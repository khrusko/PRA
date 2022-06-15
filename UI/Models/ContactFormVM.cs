using System;
using System.ComponentModel.DataAnnotations;

namespace UI.Models
{
  public class ContactFormVM
  {
    [Display(Name = "Ime")]
    [Required(ErrorMessage = "Ime je obavezno")]
    public String FName { get; set; }

    [Display(Name = "Prezime")]
    [Required(ErrorMessage = "Prezime je obavezno")]
    public String LName { get; set; }

    [Display(Name = "Email")]
    [Required(ErrorMessage = "Email je obavezan")]
    [EmailAddress(ErrorMessage = "Email nije u ispravnom formatu")]
    public String Email { get; set; }

    [Display(Name = "Upit")]
    [Required(ErrorMessage = "Upit je obavezan")]
    public String Message { get; set; }
  }
}