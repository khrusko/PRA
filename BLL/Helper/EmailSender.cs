using BLL.Abstract.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Helper
{
  public class EmailSender : IEmailSender
  {
    public MailAddress From { get; } = AppSettingsHelper.From;
    public MailAddress To { get; set; }
    public SmtpClient SmtpClient { get; } = AppSettingsHelper.SmtpClient;
    public MailMessage Message { get; set; }

    public void SendEmail()
    {
      SmtpClient.Send(Message);
    }
    public void SendEmail(string subject, string body)
    {
      Message = new MailMessage(From, To)
      {
        Subject = subject,
        Body = body,
        IsBodyHtml = true
      };
      SendEmail();
    }
  }
}
