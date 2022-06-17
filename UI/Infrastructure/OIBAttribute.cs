using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace UI.Infrastructure
{
  public class OIBAttribute : ValidationAttribute
  {
    public override Boolean IsValid(Object value)
      => value is String oib && oib.Length == 11 && oib.All(Char.IsDigit);
  }
}