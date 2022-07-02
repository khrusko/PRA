using System.Collections.Generic;

using BLL.Projection;

namespace UI.Models
{
  public class MessageListVM
  {
    public IEnumerable<MessageProjection> NotRespondedMessages { get; set; }
    public IEnumerable<MessageProjection> RespondedMessages { get; set; }
  }
}