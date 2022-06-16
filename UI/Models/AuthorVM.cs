using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

using UI.Models.Concrete;
namespace UI.Models
{
  public class AuthorVM
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

    [Display(Name = "Datum rođenja")]
    [Required(ErrorMessage = "Datum rođenja je obavezan")]
    [DataType(DataType.Date)]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
    public DateTime BirthDate { get; set; }

    [Display(Name = "Biografija")]
    public String Biography { get; set; }

    [Display(Name = "Slika")]
    public HttpPostedFileBase Image { get; set; }

    public String ImagePath { get; set; }
  }
}