using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UI.Models
{
    public class ContactFormVM
    {
        [Display(Name = "Ime i prezime*")]
        [Required(ErrorMessage = "Ime i prezime je obavezno")]
        public string FullName { get; set; }

        [Display(Name = "E-mail*")]
        [Required(ErrorMessage = "E-mail je obavezan")]
        [EmailAddress(ErrorMessage = "Nije valjani E-mail")]
        public string Email { get; set; }


        [Display(Name = "Upit*")]
        [Required(ErrorMessage = "Upit je obavezan*")]
        public string Question { get; set; }
    }
}