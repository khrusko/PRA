using System;

using BLL.Abstract.Projection;

namespace BLL.Projection
{
  public class AuthorProjection : AbstractProjection<Int32>
  {
    public String FName { get; set; }
    public String LName { get; set; }
    public DateTime BirthDate { get; set; }
    public String ImagePath { get; set; }
    public String Biography { get; set; }
  }
}
