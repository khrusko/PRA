using BLL.Abstract.Projection;
using System;

namespace BLL.Projection
{
  public class AuthorProjection : AbstractProjection<int>
  {
    public string FName { get; set; }
    public string LName { get; set; }
    public DateTime BirthDate { get; set; }
    public string ImagePath { get; set; }
    public string Biography { get; set; }
  }
}
