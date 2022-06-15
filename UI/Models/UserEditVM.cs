using System;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace UI.Models
{
  public class UserEditVM
  {
    public Int32 ID { get; set; }

    [Display(Name = "Ime")]
    [Required(ErrorMessage = "Ime je obavezno")]
    [MaxLength(length: 50)]
    public String FName { get; set; }

    [Display(Name = "Prezime")]
    [Required(ErrorMessage = "Prezime je obavezno")]
    [MaxLength(length: 50)]
    public String LName { get; set; }

    [Display(Name = "Adresa")]
    [MaxLength(length: 200)]
    public String Address { get; set; }

    [Display(Name = "Slika")]
    public HttpPostedFileBase Image { get; set; }

    public String ImagePath { get; set; }
  }
}