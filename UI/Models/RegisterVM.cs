using System;
using System.ComponentModel.DataAnnotations;

namespace UI.Models
{
  public class RegisterVM
  {
    [Display(Name = "Ime")]
    [Required(ErrorMessage = "Ime je obavezno")]
    [MaxLength(length: 50)]
    public String FName { get; set; }

    [Display(Name = "Prezime")]
    [Required(ErrorMessage = "Prezime je obavezno")]
    [MaxLength(length: 50)]
    public String LName { get; set; }

    [Display(Name = "E-mail")]
    [Required(ErrorMessage = "E-mail je obavezan")]
    [EmailAddress(ErrorMessage = "Nije valjani E-mail")]
    public String Email { get; set; }

    [Display(Name = "Zaporka")]
    [Required(ErrorMessage = "Zaporka je obavezna")]
    [DataType(DataType.Password)]
    public String Password { get; set; }

    [Display(Name = "Ponovi zaporku")]
    [Required(ErrorMessage = "Ponovite zaporku")]
    [Compare("Password", ErrorMessage = "Zaporke se ne podudaraju")]
    [DataType(DataType.Password)]
    public String ConfirmPassword { get; set; }

    [Display(Name = "Prihvaćam pravila privatnosti")]
    public Boolean AcceptRules { get; set; }
  }
}