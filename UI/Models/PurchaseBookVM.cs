using System;
using System.ComponentModel.DataAnnotations;

namespace UI.Models
{
  public class PurchaseBookVM
  {
    public FullBookInfoVM BookInfo { get; set; }

    public Int32 BookID { get; set; }

    [Display(Name = "Količina")]
    [Required(ErrorMessage = "Količina je obavezna")]
    public Int32 Quantity { get; set; }

    [Display(Name = "Prihvaćam pravila korištenja usluge i naplate zakasnine")]
    public Boolean AcceptRules { get; set; }
  }
}