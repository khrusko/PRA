using BLL.Projection;
using DAL.Model;

namespace BLL.Abstract.Manager.Projection
{
  public interface IUserManager : IProjectionManager<UserModel, UserProjection, int>
  {
    UserProjection Login(UserProjection projection);
    UserProjection Login(string Email, string Password);
    UserProjection Register(UserProjection projection);
    UserProjection Register(string FName, string LName, string Email, string Password, bool IsAdmin);
  }
}
