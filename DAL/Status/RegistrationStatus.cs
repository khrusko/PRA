using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Status
{
  public enum RegistrationStatus
  {
    INVALID = 0,
    VALID = 1,
    APPROVED = 2,
    TIMEOUT = 3
  }
}
