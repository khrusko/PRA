using DAL.Abstract.DAO;
using System;

namespace DAL.DAO
{
  public class UserDAO : AbstractDAO<int>
  {
    public string FName { get; set; }
    public string LName { get; set; }
    public DateTime BirthDate { get; set; }
    public string ImagePath { get; set; }
    public string Biography { get; set; }
  }
}
