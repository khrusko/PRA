using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Status
{
  public enum RequestResetPasswordStatus
  {
    SUCCESS       = 0,
    INVALID_EMAIL = 1,
    RESET_PENDING = 2
  }
}
