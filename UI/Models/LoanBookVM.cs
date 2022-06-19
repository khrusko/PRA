using System;
using System.ComponentModel.DataAnnotations;

using BLL.Projection;

namespace UI.Models
{
  public class LoanBookVM
  {
    public FullBookInfoVM BookInfo { get; set; }
    public BookStoreProjection BookStore { get; set; }
    
    public Int32 BookID { get; set; }

    [Display(Name = "Trajanje posudbe")]
    [Required(ErrorMessage = "Trajanje posudbe je obavezno")]
    public Int32 LoanDays { get; set; }
  }
}