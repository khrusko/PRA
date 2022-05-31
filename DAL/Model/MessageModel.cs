using DAL.Abstract.Model;
using System;

namespace DAL.Model
{
  public class MessageModel : AbstractModel<int>
  {
    public int SenderUserFK { get; set; }
    public DateTime SenderDate { get; set; }
    public string SenderMessage { get; set; }
    public int ResponderUserFK { get; set; }
    public DateTime ResponderDate { get; set; }
    public string ResponderMessage { get; set; }
  }
}
