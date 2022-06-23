using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
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
        IsAvailable = model.DeleteDate != DateTime.MinValue,
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

    public Int32 Remove(Int32 ID)
      => Remove(ID, ID);
    public Int32 Remove(Int32 ID, Int32 DeletedBy)
      => (Repository as IUserRepository).Delete(ID, DeletedBy);

    public UserProjection GetByEmail(String Email)
    {
      UserModel model = (Repository as IUserRepository).ReadByEmail(Email);
      return model is null ? null : Project(model);
    }

    public UserProjection Login(UserProjection projection) => Login(projection.Email, projection.Password);
    public UserProjection Login(String email, String password)
    {
      UserModel model = (Repository as IUserRepository).Login(email, password);
      return model is null ? null : Project(model);
    }

    public UserProjection Register(UserProjection projection)
      => Register(projection.FName, projection.LName, projection.Email, projection.Password);
    public UserProjection Register(String fName, String lName, String email, String password)
    {
      Int32 ID = (Repository as IUserRepository).Register(fName, lName, email, password, false);
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
    public RequestResetPasswordStatus RequestResetPassword(String email)
    {
      UserProjection projection = GetByEmail(email);
      if (projection == null) return RequestResetPasswordStatus.INVALID_EMAIL;

      Int32 resetCount = (Repository as IUserRepository).RequestResetPassword(email);
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
    public Int32 ResetPassword(Guid GUID, String password)
      => (Repository as IUserRepository).ResetPassword(GUID, password);

    public Int32 Update(UserProjection projection, HttpPostedFileBase file)
      => Update(projection.ID, projection.FName, projection.LName, file, projection.Address);
    public Int32 Update(Int32 ID, String fName, String lName, HttpPostedFileBase file, String address)
    {
      String imagePath;
      if (!(file is null))
      {
        IImageSaver imageSaver = ImageSaverFactory.GetImageSaver();
        imageSaver.File = file;
        imageSaver.SaveImageAs();

        imagePath = imageSaver.RelativePath;
      }
      else
      {
        imagePath = GetByID(ID).ImagePath;
      }

      return (Repository as IUserRepository).EditProfile(ID, fName, lName, imagePath, address, ID);
    }

    public UserProjection CreateAdmin(String fName, String lName, String email, String password)
    {
      Int32 ID = (Repository as IUserRepository).Register(fName, lName, email, password, true);
      if (ID == 0)
        return null;

      UserProjection projection = GetByID(ID);
      RegistrationStatus registrationStatus = CheckRegistrationStatus(projection);

      if (registrationStatus == RegistrationStatus.VALID)
      {
        Int32 updatedCount = ConfirmRegistration(projection);
        return updatedCount == 0 ? null : projection;
      }

      return null;
    }

    private void SendRegistrationConfirmationMail(UserProjection projection)
    {
      String verificationLink = $"/Account/UserVerification/{projection.GUID}";
      String link = HttpContext.Current.Request.Url.AbsoluteUri.Replace(HttpContext.Current.Request.Url.PathAndQuery, verificationLink);

      String subject = "Potvrda registracije";
      StringBuilder body = new StringBuilder().Append($"Poštovani {projection.FName} {projection.LName},<br /><br />")
                                              .Append("Uspješno ste se registrirali.<br />")
                                              .Append("Molimo Vas da potvrdite registraciju klikom na link:<br />")
                                              .Append($"<a href='{link}'>{link}</a>");

      IEmailSender emailSender = EmailSenderFactory.GetEmailSender();
      emailSender.To = new MailAddress(projection.Email, $"{projection.FName} {projection.LName}");
      emailSender.SendEmail(subject, body.ToString());
    }

    private void SendResetPasswordMail(UserProjection projection)
    {
      String verificationLink = $"/Account/ResetPassword/{projection.GUID}";
      String link = HttpContext.Current.Request.Url.AbsoluteUri.Replace(HttpContext.Current.Request.Url.PathAndQuery, verificationLink);

      String subject = "Obnova zaporke";
      StringBuilder body = new StringBuilder().Append($"Poštovani {projection.FName} {projection.LName},<br /><br />")
                                              .Append("Zahtjev za obnovu zaporke je odobren.<br />")
                                              .Append("Molimo Vas da obnovite zaporku klikom na link:<br />")
                                              .Append($"<a href='{link}'>{link}</a>");

      IEmailSender emailSender = EmailSenderFactory.GetEmailSender();
      emailSender.To = new MailAddress(projection.Email, $"{projection.FName} {projection.LName}");
      emailSender.SendEmail(subject, body.ToString());
    }
  }
}
