using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Factory
{
  internal static class ConnectionStringFactory
  {
    public static string GetConnectionString() => ConfigurationManager.ConnectionStrings["cs"].ConnectionString;
  }
}
