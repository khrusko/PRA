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
    public MailAddress From { get; set; } = AppSettingsHelper.From;
    public MailAddress To { get; set; }
    public SmtpClient SmtpClient { get; set; } = AppSettingsHelper.SmtpClient;
    public MailMessage Message { get; set; }

    public EmailSender(MailAddress to)
    {
      To = to;
    }
    public EmailSender(MailAddress to, MailMessage message) : this(to)
    {
      Message = message;
    }
    public EmailSender(MailAddress from, MailAddress to, MailMessage message) : this(to, message)
    {
      From = from;
    }

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
