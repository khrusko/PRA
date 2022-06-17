using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;

using BLL.Projection;

using UI.Infrastructure;

namespace UI.Models
{
  public class BookVM
  {
    public Int32 ID { get; set; }

    [Required(ErrorMessage = "ISBN je obavezan")]
    [ISBN(ErrorMessage = "Format ISBN-13 nije važeć")]
    public String ISBN { get; set; }

    [Display(Name = "Naslov")]
    [Required(ErrorMessage = "Naslov je obavezno")]
    [MaxLength(length: 100)]
    public String Title { get; set; }

    [Display(Name = "Kratki sadržaj")]
    [DataType(DataType.MultilineText)]
    public String Summary { get; set; }

    [Display(Name = "Opis knjige")]
    [Required(ErrorMessage = "Opis knjige je obavzan")]
    [DataType(DataType.MultilineText)]
    public String Description { get; set; }

    [Display(Name = "Nova knjiga")]
    public Boolean IsNew { get; set; }

    [Display(Name = "Izdavač")]
    [Required(ErrorMessage = "Izdavač je obavezan")]
    public Int32 PublisherFK { get; set; }

    [Display(Name = "Autor")]
    [Required(ErrorMessage = "Autor je obavezan")]
    public Int32 AuthorFK { get; set; }

    [Display(Name = "Broj stranica")]
    [Required(ErrorMessage = "Broj stranica je obavezan")]
    public Int32 PageCount { get; set; }

    [Display(Name = "Cijena kupnje")]
    [Required(ErrorMessage = "Cijena kupnje je obavezan")]
    [DataType(DataType.Currency, ErrorMessage = "Cijena kupnje je valuta")]
    public Decimal PurchasePrice { get; set; }

    [Display(Name = "Cijena posudbe")]
    [Required(ErrorMessage = "Cijena posudbe je obavezan")]
    [DataType(DataType.Currency, ErrorMessage = "Cijena posudbe je valuta")]
    public Decimal LoanPrice { get; set; }

    [Display(Name = "Dostupna količina")]
    [Required(ErrorMessage = "Dostupna količina je obavezan")]
    public Int32 Quantity { get; set; }

    [Display(Name = "Slika")]
    public HttpPostedFileBase Image { get; set; }

    public String ImagePath { get; set; }

    public IEnumerable<AuthorProjection> Authors { get; set; }
    public IEnumerable<PublisherProjection> Publishers { get; set; }
  }
}