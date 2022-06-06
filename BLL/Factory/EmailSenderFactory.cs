using BLL.Abstract.Helper;
using BLL.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Factory
{
  public static class EmailSenderFactory
  {
    public static IEmailSender GetEmailSender() => new EmailSender();
  }
}
