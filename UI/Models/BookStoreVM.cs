﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

using UI.Infrastructure;
using UI.Models.Concrete;

namespace UI.Models
{
  public class BookStoreVM
  {
    public Int32 ID { get; set; }

    [Display(Name = "Naziv")]
    [Required(ErrorMessage = "Naziv je obavezan")]
    [MaxLength(length: 100)]
    public String Name { get; set; }

    [Required(ErrorMessage = "OIB je obavezan")]
    [OIB(ErrorMessage = "OIB nije u valjanom formatu")]
    public String OIB { get; set; }

    [Display(Name = "Cijena zakasnine")]
    [Required(ErrorMessage = "Cijena zakasnine je obavezna")]
    [DataType(DataType.Currency)]
    public Decimal DelayPricePerDay { get; set; }

    [Display(Name = "Adresa")]
    [Required(ErrorMessage = "Adresa je obavezna")]
    [MaxLength(length: 200)]
    public String Address { get; set; }

    [Display(Name = "Broj telefona")]
    [Required(ErrorMessage = "Broj telefona je obavezan")]
    [MaxLength(length: 50)]
    public String Telephone { get; set; }

    [Display(Name = "Broj mobitela")]
    [MaxLength(length: 50)]
    public String Mobile { get; set; }

    [Display(Name = "Email")]
    [Required(ErrorMessage = "Email je obavezan")]
    [EmailAddress(ErrorMessage = "Email nije u valjanom formatu")]
    public String Email { get; set; }
  }
}