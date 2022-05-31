using DAL.Model;

namespace DAL.Abstract.Repository.Model
{
  public interface IUserRepository : IModelRepository<UserModel, int>
  {
    UserModel Login(string Email, string Password);
    UserModel Register(string UserID, string FName, string LName, string Email, string Password, bool IsAdmin, int CreatedBy);
  }
}
