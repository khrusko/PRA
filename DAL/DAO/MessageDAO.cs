using DAL.Abstract.DAO;
using System;

namespace DAL.DAO
{
  public class MessageDAO : AbstractDAO<int>
  {
    public int SenderUserFK { get; set; }
    public DateTime SenderDate { get; set; }
    public string SenderMessage { get; set; }
    public int ResponderUserFK { get; set; }
    public DateTime ResponderDate { get; set; }
    public string ResponderMessage { get; set; }
  }
}
