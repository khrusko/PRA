using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Abstract.Helper
{
  public interface IEmailSender
  {
    MailAddress From { get; set; }
    MailAddress To { get; set; }
    SmtpClient SmtpClient { get; set; }
    void SendEmail();
    void SendEmail(string subject, string body);
  }
}
