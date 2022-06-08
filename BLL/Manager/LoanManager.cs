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

    public LoanProjection GetByID(Int32 ID)
    {
      LoanModel model = (Repository as ILoanRepository).Read(ID);
      return model is null ? null : Project(model);
    }

    public IEnumerable<LoanProjection> GetAll()
      => (Repository as ILoanRepository).Read().Select(Project);

    public Int32 Loan(LoanProjection projection)
      => Loan(projection.BookFK, projection.UserFK, projection.PlannedReturnDate);
    public Int32 Loan(Int32 BookFK, Int32 UserFK, DateTime PlannedReturnDate)
      => (Repository as ILoanRepository).Loan(BookFK, UserFK, PlannedReturnDate, UserFK);

    public Int32 Return(Int32 ID, Int32 UpdatedBy)
      => (Repository as ILoanRepository).Return(ID, UpdatedBy);
  }
}
