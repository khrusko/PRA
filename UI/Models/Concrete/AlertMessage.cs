using System;

using UI.Models.Abstract;
using UI.Models.Enums;

namespace UI.Models.Concrete
{
  public class AlertMessage : MessageBase
  {
    private const MessageType TYPE = MessageType.ALERT;
    private const String ICON = "exclamation-triangle";
    private const String COLOR = "warning";

    public AlertMessage(String message) : base(message, TYPE, ICON, COLOR) { }
  }
}