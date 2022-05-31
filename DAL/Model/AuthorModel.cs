using DAL.Abstract.Model;
using System;

namespace DAL.Model
{
  public class AuthorModel : AbstractModel<int>
  {
    public string FName { get; set; }
    public string LName { get; set; }
    public DateTime BirthDate { get; set; }
    public string ImagePath { get; set; }
    public string Biography { get; set; }
  }
}
