using System;

using DAL.Abstract.Model;

namespace DAL.Model
{
  public class AuthorModel : AbstractModel<Int32>
  {
    public String FName { get; set; }
    public String LName { get; set; }
    public DateTime BirthDate { get; set; }
    public String ImagePath { get; set; }
    public String Biography { get; set; }
  }
}
