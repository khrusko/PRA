using System;
using System.Net.Mail;

using BLL.Abstract.Helper;

namespace BLL.Helper
{
  public class EmailSender : IEmailSender
  {
    public MailAddress From { get; } = AppSettingsHelper.EMAIL_FROM;
    public MailAddress To { get; set; }
    public MailMessage Message { get; set; }

    public void SendEmail()
    {
      using (var smtpClient = new SmtpClient())
      {
        smtpClient.Host = AppSettingsHelper.EMAIL_SMTP_CLIENT_HOST;
        smtpClient.Port = AppSettingsHelper.EMAIL_SMTP_CLIENT_PORT;
        smtpClient.EnableSsl = true;
        smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
        smtpClient.Credentials = AppSettingsHelper.EMAIL_SMTP_CLIENT_CREDENTIALS;

        smtpClient.Send(Message);
      }
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
