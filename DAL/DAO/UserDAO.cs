using DAL.Abstract.DAO;

namespace DAL.DAO
{
  public class UserDAO : AbstractDAO<int>
  {
    public string UserID { get; set; }
    public string FName { get; set; }
    public string LName { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public string ImagePath { get; set; }
    public string Address { get; set; }
    public bool IsAdmin { get; set; }
  }
}
