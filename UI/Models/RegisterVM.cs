using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UI.Models
{
    public class RegisterVM
    {
        [Display(Name = "Ime")]
        [Required(ErrorMessage = "Ime je obavezno")]
        [StringLength(maximumLength: 50, ErrorMessage = "Mora sadržavati barem 1 slovo i najviše 50 slova", MinimumLength = 1)]
        public string FName { get; set; }

        [Display(Name = "Prezime")]
        [Required(ErrorMessage = "Prezime je obavezno")]
        [StringLength(maximumLength: 50, ErrorMessage = "Mora sadržavati barem 1 slovo i najviše 50 slova", MinimumLength = 1)]
        public string LName { get; set; }

        [Display(Name = "E-mail")]
        [Required(ErrorMessage = "E-mail je obavezan")]
        [StringLength(maximumLength: 100, ErrorMessage = "Mora sadržavati barem 1 slovo i najviše 100 slova", MinimumLength = 1)]
        [EmailAddress (ErrorMessage ="Nije valjani E-mail")]
        public string Email { get; set; }

        [Display(Name = "Zaporka")]
        [Required(ErrorMessage = "Zaporka je obavezna")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Zaporka")]
        [Required(ErrorMessage = "Zaporka je obavezna")]
        [Compare("Password", ErrorMessage = "Zaporke se ne podudaraju")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

    }
}