using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;

using BLL.Abstract.Helper;
using BLL.Abstract.Manager.Projection;
using BLL.Abstract.Projection;
using BLL.Factory;
using BLL.Projection;

using DAL.Abstract.Repository;
using DAL.Abstract.Repository.Model;
using DAL.Factory;
using DAL.Model;

namespace BLL.Manager
{
  public class SubscriptionManager : ISubscriptionManager
  {
    public IRepository<SubscriptionModel> Repository { get; } = SubscriptionRepositoryFactory.GetRepository();

    public SubscriptionProjection Project(SubscriptionModel model)
      => new SubscriptionProjection
      {
        ID = model.ID,
        IsAvailable = model.DeleteDate != DateTime.MinValue,
        BookFK = model.BookFK,
        UserFK = model.UserFK,
        SubscriptionDate = model.SubscriptionDate,
        IsResolved = model.IsResolved,
        ResolvedDate = model.ResolvedDate
      };

    public SubscriptionModel Model(SubscriptionProjection projection)
      => new SubscriptionModel
      {
        ID = projection.ID,
        BookFK = projection.BookFK,
        UserFK = projection.UserFK,
        SubscriptionDate = projection.SubscriptionDate,
        IsResolved = projection.IsResolved,
        ResolvedDate = projection.ResolvedDate
      };

    public SubscriptionProjection GetByID(Int32 ID, Boolean availabilityCheck = true)
    {
      SubscriptionModel model = availabilityCheck
        ? (Repository as ISubscriptionRepository).ReadByIDAvailable(ID)
        : (Repository as ISubscriptionRepository).ReadByID(ID);
      return model is null ? null : Project(model);
    }

    public IEnumerable<SubscriptionProjection> GetAll(Boolean availabilityCheck = true)
      => availabilityCheck
      ? (Repository as ISubscriptionRepository).ReadAllAvailable().Select(Project)
      : (Repository as ISubscriptionRepository).ReadAll().Select(Project);

    public Int32 Remove(Int32 ID, Int32 deletedBy) => throw new NotImplementedException();

    public Int32 Subscribe(SubscriptionProjection projection)
      => Subscribe(projection.BookFK, projection.UserFK);
    public Int32 Subscribe(Int32 bookFK, Int32 userFK)
      => (Repository as ISubscriptionRepository).Subscribe(bookFK, userFK, userFK);

    public IEnumerable<SubscriptionProjection> GetAllUnresolved()
      => (Repository as ISubscriptionRepository).ReadAllUnresolved().Select(Project);
    public Int32 Resolve(Int32 ID, BookProjection book, UserProjection user)
    {
      Int32 updatedCount = (Repository as ISubscriptionRepository).Resolve(ID);
      if (updatedCount == 0) return 0;

      SendResolvedSubscriptionMail(book, user);

      return updatedCount;
    }

    private void SendResolvedSubscriptionMail(BookProjection book, UserProjection user)
    {
      String bookLink = $"/Book/Details/{book.ID}";
      String link = HttpContext.Current.Request.Url.AbsoluteUri.Replace(HttpContext.Current.Request.Url.PathAndQuery, bookLink);

      String subject = $"Dostupna je knjiga {book.Title}";
      StringBuilder body = new StringBuilder().Append($"Poštovani {user.FName} {user.LName},<br /><br />")
                                              .Append($"Knjiga <a href='{link}'>{book.Title}</a> je dostupna za kupnju i posudbu.<br />")
                                              .Append("Obavite željenu radnju klikom na link:<br />")
                                              .Append($"<a href='{link}'>{link}</a>");

      IEmailSender emailSender = EmailSenderFactory.GetEmailSender();
      emailSender.To = new MailAddress(user.Email, $"{user.FName} {user.LName}");
      emailSender.SendEmail(subject, body.ToString());
    }
  }
}
