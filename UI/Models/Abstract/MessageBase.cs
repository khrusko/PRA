using System;

using UI.Models.Enums;

namespace UI.Models.Abstract
{
  public abstract class MessageBase : IMessage
  {
    private readonly String _message;
    private readonly MessageType _type;
    private readonly String _icon;
    private readonly String _color;

    public MessageBase(String message, MessageType type, String icon, String color)
    {
      _message = message;
      _type = type;
      _icon = icon;
      _color = color;
    }

    public String Message => _message;
    public MessageType Type => _type;
    public String Icon => _icon;
    public String Color => _color;
  }
}