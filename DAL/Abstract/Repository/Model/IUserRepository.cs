using System;

using DAL.Model;
using DAL.Status;

namespace DAL.Abstract.Repository.Model
{
  public interface IUserRepository : IModelRepository<UserModel, Int32>
  {
    UserModel Login(String Email, String Password);
    Int32 Register(String FName, String LName, String Email, String Password, Boolean IsAdmin);
    RegistrationStatus CheckRegistrationStatus(Guid GUID);
    Int32 ConfirmRegistration(Guid GUID);
    UserModel ReadByEmail(String Email);
    Int32 RequestResetPassword(String Email);
    ResetPasswordStatus CheckResetPasswordStatus(Guid GUID);
    Int32 ResetPassword(Guid GUID, String Password);
    Int32 EditProfile(UserModel entity);
    Int32 EditProfile(UserModel entity, Int32 UpdatedBy);
    Int32 EditProfile(Int32 ID, String FName, String LName, String ImagePath, String Address, Int32 UpdatedBy);
  }
}
