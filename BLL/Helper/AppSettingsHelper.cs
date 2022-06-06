﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

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
        Port = int.Parse(ConfigurationManager.AppSettings["Email.SmtpClient.Port"]),
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