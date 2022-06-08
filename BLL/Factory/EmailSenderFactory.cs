using BLL.Abstract.Helper;
using BLL.Helper;

namespace BLL.Factory
{
  public static class EmailSenderFactory
  {
    public static IEmailSender GetEmailSender() => new EmailSender();
  }
}
