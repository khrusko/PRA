using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

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

    public UserModel Model(UserProjection projection)
      => new UserModel
      {
        ID = projection.ID,
        UserID = projection.UserID,
        FName = projection.FName,
        LName = projection.LName,
        Email = projection.Email,
        PasswordHash = projection.Password,
        ImagePath = projection.ImagePath,
        Address = projection.Address,
        IsAdmin = projection.IsAdmin,
        GUID = projection.GUID
      };

    public UserProjection GetByID(Int32 ID, Boolean availabilityCheck = true)
    {
      UserModel model = availabilityCheck
        ? (Repository as IUserRepository).ReadByIDAvailable(ID)
        : (Repository as IUserRepository).ReadByID(ID);
      return model is null ? null : Project(model);
    }

    public IEnumerable<UserProjection> GetAll(Boolean availabilityCheck = true)
      => availabilityCheck
      ? (Repository as IUserRepository).ReadAllAvailable().Select(Project)
      : (Repository as IUserRepository).ReadAll().Select(Project);

    public Int32 Remove(Int32 ID, Int32 DeletedBy)
      => (Repository as IUserRepository).Delete(ID, DeletedBy);

    public UserProjection GetByEmail(String Email)
    {
      UserModel model = (Repository as IUserRepository).ReadByEmail(Email);
      return model is null ? null : Project(model);
    }

    public UserProjection Login(UserProjection projection) => Login(projection.Email, projection.Password);
    public UserProjection Login(String Email, String Password)
    {
      UserModel model = (Repository as IUserRepository).Login(Email, Password);
      return model is null ? null : Project(model);
    }

    public UserProjection Register(UserProjection projection)
      => Register(projection.FName, projection.LName, projection.Email, projection.Password, projection.IsAdmin);
    public UserProjection Register(String FName, String LName, String Email, String Password, Boolean IsAdmin)
    {
      Int32 ID = (Repository as IUserRepository).Register(FName, LName, Email, Password, IsAdmin);
      if (ID == 0) return null;

      UserProjection projection = GetByID(ID);
      SendRegistrationConfirmationMail(projection);

      return projection;
    }

    public RegistrationStatus CheckRegistrationStatus(UserProjection projection)
      => CheckRegistrationStatus(projection.GUID);
    public RegistrationStatus CheckRegistrationStatus(Guid GUID)
      => (Repository as IUserRepository).CheckRegistrationStatus(GUID);

    public Int32 ConfirmRegistration(UserProjection projection)
      => ConfirmRegistration(projection.GUID);
    public Int32 ConfirmRegistration(Guid GUID)
      => (Repository as IUserRepository).ConfirmRegistration(GUID);

    public RequestResetPasswordStatus RequestResetPassword(UserProjection projection)
      => RequestResetPassword(projection.Email);
    public RequestResetPasswordStatus RequestResetPassword(String Email)
    {
      UserProjection projection = GetByEmail(Email);
      if (projection == null) return RequestResetPasswordStatus.INVALID_EMAIL;

      Int32 resetCount = (Repository as IUserRepository).RequestResetPassword(Email);
      if (resetCount == 0) return RequestResetPasswordStatus.RESET_PENDING;

      SendResetPasswordMail(projection);

      return RequestResetPasswordStatus.SUCCESS;
    }

    public ResetPasswordStatus CheckResetPasswordStatus(UserProjection projection)
      => CheckResetPasswordStatus(projection.GUID);
    public ResetPasswordStatus CheckResetPasswordStatus(Guid GUID)
      => (Repository as IUserRepository).CheckResetPasswordStatus(GUID);

    public Int32 ResetPassword(UserProjection projection)
      => ResetPassword(projection.GUID, projection.Password);
    public Int32 ResetPassword(Guid GUID, String Password)
      => (Repository as IUserRepository).ResetPassword(GUID, Password);

    private void SendRegistrationConfirmationMail(UserProjection projection)
    {
      String verificationLink = $"/Registration/UserVerification/{projection.GUID}";
      String link = HttpContext.Current.Request.Url.AbsoluteUri.Replace(HttpContext.Current.Request.Url.PathAndQuery, verificationLink);

      String subject = "Potvrda registracije";
      String body = $"<br />Uspješno ste se registrirali.<br />Molimo Vas da potvrdite registraciju klikom na link:<br /><br /><a href='{link}'>{link}</a>";

      IEmailSender emailSender = EmailSenderFactory.GetEmailSender();
      emailSender.To = new MailAddress(projection.Email, $"{projection.FName} {projection.LName}");
      emailSender.SendEmail(subject, body);
    }

    private void SendResetPasswordMail(UserProjection projection)
    {
      String verificationLink = $"/Login/ResetPassword/{projection.GUID}";
      String link = HttpContext.Current.Request.Url.AbsoluteUri.Replace(HttpContext.Current.Request.Url.PathAndQuery, verificationLink);

      String subject = "Obnova zaporke";
      String body = $"<br />Zahtjev za obnovu zaporke odobren.<br />Molimo Vas da obnovite zaporuku klikom na link:<br /><br /><a href='{link}'>{link}</a>";

      IEmailSender emailSender = EmailSenderFactory.GetEmailSender();
      emailSender.To = new MailAddress(projection.Email, $"{projection.FName} {projection.LName}");
      emailSender.SendEmail(subject, body);
    }
  }
}
