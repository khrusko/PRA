using System;
using System.Configuration;
using System.Net;
using System.Net.Mail;

namespace BLL.Helper
{
  internal static class AppSettingsHelper
  {
    public static MailAddress From { get; }
      = new MailAddress(ConfigurationManager.AppSettings["Email.From.Address"],
                        ConfigurationManager.AppSettings["Email.From.DisplayName"]);

    public static SmtpClient SmtpClient { get; }
      = new SmtpClient
      {
        Host = ConfigurationManager.AppSettings["Email.SmtpClient.Host"],
        Port = Int32.Parse(ConfigurationManager.AppSettings["Email.SmtpClient.Port"]),
        EnableSsl = true,
        DeliveryMethod = SmtpDeliveryMethod.Network,
        Credentials = new NetworkCredential
        {
          UserName = ConfigurationManager.AppSettings["Email.SmtpClient.Credentials.UserName"],
          Password = ConfigurationManager.AppSettings["Email.SmtpClient.Credentials.Password"]
        }
      };
  }
}
