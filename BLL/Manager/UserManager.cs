using BLL.Abstract.Manager.Projection;
using BLL.Projection;
using DAL.Abstract.Repository;
using DAL.Abstract.Repository.Model;
using DAL.Factory;
using DAL.Model;
using System.Collections.Generic;
using System.Linq;

namespace BLL.Manager
{
  public class UserManager : IUserManager
  {
    public IRepository<UserModel> Repository { get; } = UserRepositoryFactory.GetRepository();

    public UserProjection Project(UserModel model)
      => new UserProjection
      {
        ID = model.ID,
        UserID = model.UserID,
        FName = model.FName,
        LName = model.LName,
        Email = model.Email,
        Password = model.PasswordHash,
        ImagePath = model.ImagePath,
        Address = model.Address,
        IsAdmin = model.IsAdmin
      };

    public UserProjection GetByID(int ID)
    {
      UserModel model = (Repository as IUserRepository).Read(ID);
      return model is null ? null : Project(model);
    }

    public IEnumerable<UserProjection> GetAll()
      => (Repository as IUserRepository).Read().Select(Project);

    public UserProjection Login(UserProjection projection) => Login(projection.Email, projection.Password);
    public UserProjection Login(string Email, string Password)
    {
      UserModel model = (Repository as IUserRepository).Login(Email, Password);
      return model is null ? null : Project(model);
    }

    public UserProjection Register(UserProjection projection)
      => Register(projection.FName, projection.LName, projection.Email, projection.Password, projection.IsAdmin);
    public UserProjection Register(string FName, string LName, string Email, string Password, bool IsAdmin)
    {
      return Project((Repository as IUserRepository).Register(FName, LName, Email, Password, IsAdmin));
    }
  }
}
