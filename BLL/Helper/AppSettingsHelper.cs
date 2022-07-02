using System;
using System.Configuration;
using System.Net;
using System.Net.Mail;

namespace BLL.Helper
{
  internal static class AppSettingsHelper
  {
    public static MailAddress EMAIL_FROM { get; }
      = new MailAddress(ConfigurationManager.AppSettings["Email.From.Address"],
                        ConfigurationManager.AppSettings["Email.From.DisplayName"]);

    public static String EMAIL_SMTP_CLIENT_HOST = ConfigurationManager.AppSettings["Email.SmtpClient.Host"];

    public static Int32 EMAIL_SMTP_CLIENT_PORT = Int32.Parse(ConfigurationManager.AppSettings["Email.SmtpClient.Port"]);

    public static NetworkCredential EMAIL_SMTP_CLIENT_CREDENTIALS = new NetworkCredential
    {
      UserName = ConfigurationManager.AppSettings["Email.SmtpClient.Credentials.UserName"],
      Password = ConfigurationManager.AppSettings["Email.SmtpClient.Credentials.Password"]
    };

    public static String[] FILE_IMAGE_ALLOWED_EXTENSIONS { get; }
      = ConfigurationManager.AppSettings["File.Image.AllowedExtensions"].Split('|');

    public static String FILE_IMAGE_DIR_PATH { get; }
      = ConfigurationManager.AppSettings["File.Image.DirPath"];
  }
}
