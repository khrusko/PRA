using DAL.Status;
using DAL.Model;
using System;

namespace DAL.Abstract.Repository.Model
{
  public interface IUserRepository : IModelRepository<UserModel, int>
  {
    UserModel Login(string Email, string Password);
    int Register(string FName, string LName, string Email, string Password, bool IsAdmin);
    RegistrationStatus CheckRegistrationStatus(Guid ConfirmationGUID);
    int ConfirmRegistration(Guid ConfirmationGUID);
  }
}
