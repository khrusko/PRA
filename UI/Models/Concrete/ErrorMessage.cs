using System;

using UI.Models.Abstract;
using UI.Models.Enums;

namespace UI.Models.Concrete
{
  public class ErrorMessage : MessageBase
  {
    private const MessageType TYPE = MessageType.ERROR;
    private const String ICON = "exclamation-triangle";
    private const String COLOR = "danger";

    public ErrorMessage(String message) : base(message, TYPE, ICON, COLOR) { }
  }
}