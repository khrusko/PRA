using System;

using DAL.Abstract.Model;

namespace DAL.Model
{
  public class MessageModel : AbstractModel<Int32>
  {
    public Int32 SenderUserFK { get; set; }
    public DateTime SenderDate { get; set; }
    public String SenderMessage { get; set; }
    public Int32 ResponderUserFK { get; set; }
    public DateTime ResponderDate { get; set; }
    public String ResponderMessage { get; set; }
  }
}
