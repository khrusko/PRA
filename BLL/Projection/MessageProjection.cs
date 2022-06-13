﻿using System;

using BLL.Abstract.Projection;

namespace BLL.Projection
{
  public class MessageProjection : AbstractProjection<Int32>
  {
    public String SenderFName { get; set; }
    public String SenderLName { get; set; }
    public String SenderEmail { get; set; }
    public DateTime SenderDate { get; set; }
    public String SenderMessage { get; set; }
    public Int32 ResponderUserFK { get; set; }
    public DateTime ResponderDate { get; set; }
    public String ResponderMessage { get; set; }
  }
}
