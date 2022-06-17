using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace UI.Infrastructure
{
  public class ISBNAttribute : ValidationAttribute
  {
    public override Boolean IsValid(Object value)
    {
      String isbn = value as String;
      if (!String.IsNullOrEmpty(isbn))
      {
        isbn = isbn.Trim();

        if (isbn.Contains('-'))
          isbn = isbn.Replace("-", "");

        if (!Int64.TryParse(isbn, out _))
          return false;

        if (isbn.Length < 13)
          return false;

        Int32 sum = 0;
        for (Int32 i = 0; i < 12; i++)
        {
          sum += Int32.Parse(isbn[i].ToString()) * (i % 2 == 1 ? 3 : 1);
        }

        Int32 remainder = sum % 10;
        Int32 checkDigit = 10 - remainder;
        if (checkDigit == 10)
          checkDigit = 0;

        return checkDigit == Int32.Parse(isbn[12].ToString());
      }

      return false;
    }
  }
}