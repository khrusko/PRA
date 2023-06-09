﻿using System;
using System.Net.Mail;

namespace BLL.Abstract.Helper
{
  public interface IEmailSender
  {
    MailAddress From { get; }
    MailAddress To { get; set; }
    MailMessage Message { get; set; }
    void SendEmail();
    void SendEmail(String subject, String body);
  }
}
