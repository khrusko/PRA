using BLL.Abstract.Helper;
using BLL.Abstract.Manager.Projection;
using BLL.Factory;
using BLL.Projection;
using BLL.Status;
using DAL.Abstract.Repository;
using DAL.Abstract.Repository.Model;
using DAL.Factory;
using DAL.Model;
using DAL.Status;
using System;
using System.Collections.Generic;
using System.Linq;
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
        GUID = model.GUID
      };

    public UserProjection GetByID(int ID)
    {
      UserModel model = (Repository as IUserRepository).Read(ID);
      return model is null ? null : Project(model);
    }

    public UserProjection GetByEmail(string Email)
    {
      UserModel model = (Repository as IUserRepository).ReadByEmail(Email);
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

    public RegistrationStatus CheckRegistrationStatus(Guid GUID)
      => (Repository as IUserRepository).CheckRegistrationStatus(GUID);

    public int ConfirmRegistration(Guid GUID)
      => (Repository as IUserRepository).ConfirmRegistration(GUID);

    public RequestResetPasswordStatus RequestResetPassword(UserProjection projection)
      => RequestResetPassword(projection.Email);
    public RequestResetPasswordStatus RequestResetPassword(string Email)
    {
      UserProjection projection = GetByEmail(Email);
      if (projection == null) return RequestResetPasswordStatus.INVALID_EMAIL;

      int resetCount = (Repository as IUserRepository).RequestResetPassword(Email);
      if (resetCount == 0) return RequestResetPasswordStatus.RESET_PENDING;

      SendResetPasswordMail(projection);

      return RequestResetPasswordStatus.SUCCESS;
    }

    public ResetPasswordStatus CheckResetPasswordStatus(Guid GUID)
      => (Repository as IUserRepository).CheckResetPasswordStatus(GUID);

    public int ResetPassword(UserProjection projection)
      => ResetPassword(projection.GUID, projection.Password);
    public int ResetPassword(Guid GUID, string Password)
      => (Repository as IUserRepository).ResetPassword(GUID, Password);

    private void SendRegistrationConfirmationMail(UserProjection projection)
    {
      string verificationLink = $"/Registration/UserVerification/{projection.GUID}";
      string link = HttpContext.Current.Request.Url.AbsoluteUri.Replace(HttpContext.Current.Request.Url.PathAndQuery, verificationLink);

      string subject = "Potvrda registracije";
      string body = $"<br />Uspješno ste se registrirali.<br />Molimo Vas da potvrdite registraciju klikom na link:<br /><br /><a href='{link}'>{link}</a>";

      IEmailSender emailSender = EmailSenderFactory.GetEmailSender();
      emailSender.To = new MailAddress(projection.Email, $"{projection.FName} {projection.LName}");
      emailSender.SendEmail(subject, body);
    }

    private void SendResetPasswordMail(UserProjection projection)
    {
      string verificationLink = $"/User/ResetPassword/{projection.GUID}";
      string link = HttpContext.Current.Request.Url.AbsoluteUri.Replace(HttpContext.Current.Request.Url.PathAndQuery, verificationLink);

      string subject = "Obnova zaporke";
      string body = $"<br />Zahtjev za obnovu zaporke odobren.<br />Molimo Vas da obnovite zaporuku klikom na link:<br /><br /><a href='{link}'>{link}</a>";

      IEmailSender emailSender = EmailSenderFactory.GetEmailSender();
      emailSender.To = new MailAddress(projection.Email, $"{projection.FName} {projection.LName}");
      emailSender.SendEmail(subject, body);
    }
  }
}
