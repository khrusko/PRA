using System;
using System.Collections.Generic;
using System.Linq;

using BLL.Abstract.Manager.Projection;
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

    public LoanProjection Project(LoanModel model)
      => new LoanProjection
      {
        ID = model.ID,
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
      => (Repository as ILoanRepository).Loan(bookFK, userFK, plannedReturnDate, userFK);

    public Int32 Return(Int32 ID, Int32 updatedBy)
      => (Repository as ILoanRepository).Return(ID, updatedBy);

    public IEnumerable<LoanProjection> GetByUserFK(Int32 userFK)
      => (Repository as ILoanRepository).ReadByUserFK(userFK).Select(Project);
    public IEnumerable<LoanProjection> GetActiveByUserFK(Int32 userFK)
      => (Repository as ILoanRepository).ReadByUserFKActive(userFK).Select(Project);
  }
}
