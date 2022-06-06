using DAL.Status;
using DAL.Model;
using System;

namespace DAL.Abstract.Repository.Model
{
  public interface IUserRepository : IModelRepository<UserModel, int>
  {
    UserModel Login(string Email, string Password);
    int Register(string FName, string LName, string Email, string Password, bool IsAdmin);
    RegistrationStatus CheckRegistrationStatus(Guid GUID);
    int ConfirmRegistration(Guid GUID);
    UserModel ReadByEmail(string Email);
    int RequestResetPassword(string Email);
    ResetPasswordStatus CheckResetPasswordStatus(Guid GUID);
    int ResetPassword(Guid GUID, string Password);
  }
}
