using BLL.Projection;
using BLL.Status;
using DAL.Model;
using DAL.Status;
using System;

namespace BLL.Abstract.Manager.Projection
{
  public interface IUserManager : IProjectionManager<UserModel, UserProjection, Int32>
  {
    UserProjection GetByEmail(String Email);
    UserProjection Login(UserProjection projection);
    UserProjection Login(String Email, String Password);
    UserProjection Register(UserProjection projection);
    UserProjection Register(String FName, String LName, String Email, String Password, Boolean IsAdmin);
    RegistrationStatus CheckRegistrationStatus(UserProjection projection);
    RegistrationStatus CheckRegistrationStatus(Guid GUID);
    Int32 ConfirmRegistration(UserProjection projection);
    Int32 ConfirmRegistration(Guid GUID);
    RequestResetPasswordStatus RequestResetPassword(UserProjection projection);
    RequestResetPasswordStatus RequestResetPassword(String Email);
    ResetPasswordStatus CheckResetPasswordStatus(UserProjection projection);
    ResetPasswordStatus CheckResetPasswordStatus(Guid GUID);
    Int32 ResetPassword(UserProjection projection);
    Int32 ResetPassword(Guid GUID, String Password);
  }
}
