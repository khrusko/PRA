using BLL.Abstract.Helper;
using System.Net.Mail;

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
