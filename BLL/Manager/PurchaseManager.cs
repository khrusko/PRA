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

using DAL.Abstract.Repository;
using DAL.Abstract.Repository.Model;
using DAL.Factory;
using DAL.Model;

namespace BLL.Manager
{
  public class PurchaseManager : IPurchaseManager
  {
    public IRepository<PurchaseModel> Repository { get; } = PurchaseRepositoryFactory.GetRepository();

    private readonly IUserManager _userManager = new UserManager();
    private readonly IBookManager _bookManager = new BookManager();

    public PurchaseProjection Project(PurchaseModel model)
      => new PurchaseProjection
      {
        ID = model.ID,
        IsAvailable = model.DeleteDate != DateTime.MinValue,
        BookFK = model.BookFK,
        UserFK = model.UserFK,
        Quantity = model.Quantity,
        UnitPrice = model.UnitPrice,
        PurchaseDate = model.PurchaseDate
      };

    public PurchaseModel Model(PurchaseProjection projection)
      => new PurchaseModel
      {
        ID = projection.ID,
        BookFK = projection.BookFK,
        UserFK = projection.UserFK,
        Quantity = projection.Quantity,
        UnitPrice = projection.UnitPrice,
        PurchaseDate = projection.PurchaseDate
      };

    public PurchaseProjection GetByID(Int32 ID, Boolean availabilityCheck = true)
    {
      PurchaseModel model = availabilityCheck
        ? (Repository as IPurchaseRepository).ReadByIDAvailable(ID)
        : (Repository as IPurchaseRepository).ReadByID(ID);
      return model is null ? null : Project(model);
    }

    public IEnumerable<PurchaseProjection> GetAll(Boolean availabilityCheck = true)
      => availabilityCheck
      ? (Repository as IPurchaseRepository).ReadAllAvailable().Select(Project)
      : (Repository as IPurchaseRepository).ReadAll().Select(Project);

    public Int32 Remove(Int32 ID, Int32 deletedBy) => throw new NotImplementedException();

    public Int32 Purchase(PurchaseProjection projection)
      => Purchase(projection.BookFK, projection.UserFK, projection.Quantity);
    public Int32 Purchase(Int32 bookFK, Int32 userFK, Int32 quantity)
    {
      Int32 ID = (Repository as IPurchaseRepository).Purchase(bookFK, userFK, quantity, userFK);
      if (ID == 0) return 0;

      UserProjection user = _userManager.GetByID(userFK);
      if (user is null) return 0;

      BookProjection book = _bookManager.GetByID(bookFK);
      if (book is null) return 0;

      SendPurchaseMail(user, book);

      return ID;
    }

    public IEnumerable<PurchaseProjection> GetByUserFK(Int32 userFK)
      => (Repository as IPurchaseRepository).ReadByUserFK(userFK).Select(Project);

    private void SendPurchaseMail(UserProjection user, BookProjection book)
    {
      String subject = "Kupnja knjige";
      StringBuilder body = new StringBuilder().Append($"Poštovani {user.FName} {user.LName},<br /><br />")
                                              .Append($"Kupili ste knjigu {book.Title}.<br />")
                                              .Append("Nadamo se da ćete uživati čitajući je.");

      IEmailSender emailSender = EmailSenderFactory.GetEmailSender();
      emailSender.To = new MailAddress(user.Email, $"{user.FName} {user.LName}");
      emailSender.SendEmail(subject, body.ToString());
    }
  }
}
