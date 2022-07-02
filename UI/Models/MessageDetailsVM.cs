using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using BLL.Projection;

namespace UI.Models
{
  public class MessageDetailsVM
  {
    public MessageProjection Message { get; set; } 
    public UserProjection Responder { get; set; }
  }
}