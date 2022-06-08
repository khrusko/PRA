using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UI.Models
{
    public class ResetPasswordVM
    {
        [Display(Name = "Zaporka")]
        [Required(ErrorMessage = "Zaporka je obavezna")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Ponovi Zaporku")]
        [Required(ErrorMessage = "Polje Ponovi Zaporku je obavezno")]
        [Compare("Password", ErrorMessage = "Zaporke se ne podudaraju")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
        public Guid GUID { get; set; }
    }
}