using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
namespace UI.Models
{
  public class EditBranchOfficeDetailsVM
  {
    [Display(Name="Adresa")]
    [Required(ErrorMessage ="Adresa je obvezna")]
    public String Address { get; set; }

    [Display(Name = "Telefon")]
    [Required(ErrorMessage = "Telefon je obvezan")]
    [Phone(ErrorMessage ="Telefon može sadržavati samo brojeve")]
    public String Telephone { get; set; }

    [Display(Name = "Adresa")]
    [Required(ErrorMessage = "Adresa je obvzena")]
    [EmailAddress(ErrorMessage = "Nije valjani E-mail")]
    public String Email { get; set; }
  }
} 