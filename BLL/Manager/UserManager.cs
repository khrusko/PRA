using BLL.Abstract.Helper;
using BLL.Abstract.Manager.Projection;
using BLL.Factory;
using BLL.Helper;
using BLL.Projection;
using DAL.Abstract.Repository;
using DAL.Abstract.Repository.Model;
using DAL.Factory;
using DAL.Model;
using DAL.Status;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

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
        IsAdmin = model.IsAdmin,
        ConfirmationGUID = model.ConfirmationGUID
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
      int ID = (Repository as IUserRepository).Register(FName, LName, Email, Password, IsAdmin);
      if (ID == 0) return null;

      UserProjection projection = GetByID(ID);
      SendRegistrationConfirmationMail(projection);

      return projection;
    }

    public RegistrationStatus CheckRegistrationStatus(Guid ConfirmationGUID)
      => (Repository as IUserRepository).CheckRegistrationStatus(ConfirmationGUID);

    public void ConfirmRegistration(Guid ConfirmationGUID)
      => (Repository as IUserRepository).ConfirmRegistration(ConfirmationGUID);

    private void SendRegistrationConfirmationMail(UserProjection projection)
    {
      string verificationLink = $"/Registration/UserVerification/{projection.ConfirmationGUID}";
      string link = HttpContext.Current.Request.Url.AbsoluteUri.Replace(HttpContext.Current.Request.Url.PathAndQuery, verificationLink);

      string subject = "Potvrda registracije";
      string body = $"<br />Uspješno ste se registrirali.<br />Molimo Vas da potvrdite registraciju klikom na link:<br /><br /><a href='{link}'>{link}</a>";

      IEmailSender emailSender = EmailSenderFactory.GetEmailSender();
      emailSender.To = new MailAddress(projection.Email, $"{projection.FName} {projection.LName}");
      emailSender.SendEmail(subject, body);
    }
  }
}
