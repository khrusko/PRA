using System;
using System.ComponentModel.DataAnnotations;

namespace UI.Models
{
  public class PagingInfo
  {
    public Int32 TotalItems { get; set; }
    public Int32 ItemsPerPage { get; set; }
    public Int32 CurrentPage { get; set; }
    public Int32 TotalPages => (Int32)Math.Ceiling((Decimal)TotalItems / ItemsPerPage);
  }
}