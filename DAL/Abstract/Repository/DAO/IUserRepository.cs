using DAL.DAO;

namespace DAL.Abstract.Repository.DAO
{
  public interface IUserRepository : IDAORepository<UserDAO, int>
  {
    UserDAO Login(string Email, string Password);
    UserDAO Register(string UserID, string FName, string LName, string Email, string Password, bool IsAdmin, int CreatedBy);
  }
}
