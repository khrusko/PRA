using System;
using System.Net.Mail;

using BLL.Abstract.Helper;

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
    public void SendEmail(String subject, String body)
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
