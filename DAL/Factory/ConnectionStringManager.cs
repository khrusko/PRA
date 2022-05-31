using System.Configuration;

namespace DAL.Factory
{
  internal static class ConnectionStringFactory
  {
    public static string GetConnectionString() => ConfigurationManager.ConnectionStrings["cs"].ConnectionString;
  }
}
