using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UI.Models.Enums;

namespace UI.Models.Abstract
{
  public interface IMessage
  {
    String Message { get; }
    MessageType Type { get; }
    String Icon { get; }
    String Color { get; }
  }
}
