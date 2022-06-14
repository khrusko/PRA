using System;

using UI.Models.Abstract;
using UI.Models.Enums;

namespace UI.Models.Concrete
{
  public class InfoMessage : MessageBase
  {
    private const MessageType TYPE = MessageType.INFO;
    private const String ICON = "info-circle";
    private const String COLOR = "primary";

    public InfoMessage(String message) : base(message, TYPE, ICON, COLOR) { }
  }
}