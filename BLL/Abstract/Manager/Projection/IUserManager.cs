using BLL.Projection;
using BLL.Status;
using DAL.Model;
using DAL.Status;
using System;

namespace BLL.Abstract.Manager.Projection
{
  public interface IUserManager : IProjectionManager<UserModel, UserProjection, int>
  {
    UserProjection GetByEmail(string Email);
    UserProjection Login(UserProjection projection);
    UserProjection Login(string Email, string Password);
    UserProjection Register(UserProjection projection);
    UserProjection Register(string FName, string LName, string Email, string Password, bool IsAdmin);
    RegistrationStatus CheckRegistrationStatus(UserProjection projection);
    RegistrationStatus CheckRegistrationStatus(Guid GUID);
    int ConfirmRegistration(UserProjection projection);
    int ConfirmRegistration(Guid GUID);
    RequestResetPasswordStatus RequestResetPassword(UserProjection projection);
    RequestResetPasswordStatus RequestResetPassword(string Email);
    ResetPasswordStatus CheckResetPasswordStatus(UserProjection projection);
    ResetPasswordStatus CheckResetPasswordStatus(Guid GUID);
    int ResetPassword(UserProjection projection);
    int ResetPassword(Guid GUID, string Password);
  }
}
