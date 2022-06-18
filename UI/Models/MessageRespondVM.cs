using System;
using System.ComponentModel.DataAnnotations;

namespace UI.Models
{
  public class MessageRespondVM
  {
    public Int32 ID { get; set; }
    public String SenderFName { get; set; }
    public String SenderLName { get; set; }
    public String SenderEmail { get; set; }
    public DateTime SenderDate { get; set; }
    public String SenderMessage { get; set; }

    [Display(Name = "Odgovor")]
    [Required(ErrorMessage = "Odgovor na upit je obavezan")]
    [DataType(DataType.MultilineText)]
    public String ResponderMessage { get; set; }
  }
}