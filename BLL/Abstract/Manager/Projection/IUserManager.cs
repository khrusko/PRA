using BLL.Projection;
using BLL.Status;
using DAL.Model;
using DAL.Status;
using System;
using System.Web;

namespace BLL.Abstract.Manager.Projection
{
  public interface IUserManager : IProjectionManager<UserModel, UserProjection, Int32>
  {
    UserProjection GetByEmail(String email);
    UserProjection Login(UserProjection projection);
    UserProjection Login(String email, String password);
    UserProjection Register(UserProjection projection);
    UserProjection Register(String fName, String lName, String email, String password, Boolean isAdmin);
    RegistrationStatus CheckRegistrationStatus(UserProjection projection);
    RegistrationStatus CheckRegistrationStatus(Guid GUID);
    Int32 ConfirmRegistration(UserProjection projection);
    Int32 ConfirmRegistration(Guid GUID);
    RequestResetPasswordStatus RequestResetPassword(UserProjection projection);
    RequestResetPasswordStatus RequestResetPassword(String email);
    ResetPasswordStatus CheckResetPasswordStatus(UserProjection projection);
    ResetPasswordStatus CheckResetPasswordStatus(Guid GUID);
    Int32 ResetPassword(UserProjection projection);
    Int32 ResetPassword(Guid GUID, String password);
    Int32 Update(UserProjection projection, HttpPostedFileBase file);
    Int32 Update(Int32 ID, String fName, String lName, HttpPostedFileBase file, String address);
    Int32 Remove(Int32 ID);
  }
}
