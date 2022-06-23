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
  public class LoanManager : ILoanManager
  {
    public IRepository<LoanModel> Repository { get; } = LoanRepositoryFactory.GetRepository();

    private readonly IUserManager _userManager = new UserManager();
    private readonly IBookManager _bookManager = new BookManager();

    public LoanProjection Project(LoanModel model)
      => new LoanProjection
      {
        ID = model.ID,
        IsAvailable = model.DeleteDate != DateTime.MinValue,
        BookFK = model.BookFK,
        UserFK = model.UserFK,
        LoanPrice = model.LoanPrice,
        LoanDate = model.LoanDate,
        PlannedReturnDate = model.PlannedReturnDate,
        ReturnDate = model.ReturnDate,
        DelayDays = model.DelayDays,
        DelayPricePerDay = model.DelayPricePerDay
      };

    public LoanModel Model(LoanProjection projection)
      => new LoanModel
      {
        ID = projection.ID,
        BookFK = projection.BookFK,
        UserFK = projection.UserFK,
        LoanPrice = projection.LoanPrice,
        LoanDate = projection.LoanDate,
        PlannedReturnDate = projection.PlannedReturnDate,
        ReturnDate = projection.ReturnDate,
        DelayDays = projection.DelayDays,
        DelayPricePerDay = projection.DelayPricePerDay
      };

    public LoanProjection GetByID(Int32 ID, Boolean availabilityCheck = true)
    {
      LoanModel model = availabilityCheck
        ? (Repository as ILoanRepository).ReadByIDAvailable(ID)
        : (Repository as ILoanRepository).ReadByID(ID);
      return model is null ? null : Project(model);
    }

    public IEnumerable<LoanProjection> GetAll(Boolean availabilityCheck = true)
      => availabilityCheck
      ? (Repository as ILoanRepository).ReadAllAvailable().Select(Project)
      : (Repository as ILoanRepository).ReadAll().Select(Project);

    public Int32 Remove(Int32 ID, Int32 deletedBy) => throw new NotImplementedException();

    public Int32 Loan(LoanProjection projection)
      => Loan(projection.BookFK, projection.UserFK, projection.PlannedReturnDate);
    public Int32 Loan(Int32 bookFK, Int32 userFK, DateTime plannedReturnDate)
    {
      Int32 ID = (Repository as ILoanRepository).Loan(bookFK, userFK, plannedReturnDate, userFK);
      if (ID == 0) return 0;

      LoanProjection loan = GetByID(ID);
      if (loan is null) return 0;

      UserProjection user = _userManager.GetByID(userFK);
      if (user is null) return 0;

      BookProjection book = _bookManager.GetByID(bookFK);
      if (book is null) return 0;

      SendLoanMail(loan, user, book);

      return ID;
    }

    public Int32 Return(Int32 ID, Int32 updatedBy)
      => (Repository as ILoanRepository).Return(ID, updatedBy);

    public IEnumerable<LoanProjection> GetByUserFK(Int32 userFK)
      => (Repository as ILoanRepository).ReadByUserFK(userFK).Select(Project);
    public IEnumerable<LoanProjection> GetActiveByUserFK(Int32 userFK)
      => (Repository as ILoanRepository).ReadByUserFKActive(userFK).Select(Project);

    public IEnumerable<LoanProjection> GetLoansInTimeout()
      => (Repository as ILoanRepository).ReadLoansInTimeout().Select(Project);

    public void NotifyTimeout(Int32 ID, BookProjection book, UserProjection user) => SendResolvedSubscriptionMail(book, user);

    private void SendLoanMail(LoanProjection loan, UserProjection user, BookProjection book)
    {
      String subject = "Posudba knjige";
      StringBuilder body = new StringBuilder().Append($"Poštovani {user.FName} {user.LName},<br /><br />")
                                              .Append($"Posudili ste knjigu {book.Title}.<br />")
                                              .Append("Nadamo se da ćete uživati čitajući je.")
                                              .Append($"Rok vraćanja knjige je: {loan.PlannedReturnDate.ToLongDateString()}.");

      IEmailSender emailSender = EmailSenderFactory.GetEmailSender();
      emailSender.To = new MailAddress(user.Email, $"{user.FName} {user.LName}");
      emailSender.SendEmail(subject, body.ToString());
    }

    private void SendResolvedSubscriptionMail(BookProjection book, UserProjection user)
    {
      String subject = $"Istekla posudba";
      StringBuilder body = new StringBuilder().Append($"Poštovani {user.FName} {user.LName},<br /><br />")
                                              .Append("Istekla Vam je posudba.<br />")
                                              .Append($"Molimo Vas da knjigu {book.Title} vratite u knjižaru,<br />")
                                              .Append("kako Vam se ne bi naplatila zakasnina.");

      IEmailSender emailSender = EmailSenderFactory.GetEmailSender();
      emailSender.To = new MailAddress(user.Email, $"{user.FName} {user.LName}");
      emailSender.SendEmail(subject, body.ToString());
    }
  }
}
