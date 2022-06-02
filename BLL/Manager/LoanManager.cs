using BLL.Abstract.Manager.Projection;
using BLL.Projection;
using DAL.Abstract.Repository;
using DAL.Abstract.Repository.Model;
using DAL.Factory;
using DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;

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

    public LoanProjection GetByID(int ID)
    {
      LoanModel model = (Repository as ILoanRepository).Read(ID);
      return model is null ? null : Project(model);
    }

    public IEnumerable<LoanProjection> GetAll()
      => (Repository as ILoanRepository).Read().Select(Project);

    public int Loan(LoanProjection projection)
      => Loan(projection.BookFK, projection.UserFK, projection.PlannedReturnDate);
    public int Loan(int BookFK, int UserFK, DateTime PlannedReturnDate)
      => (Repository as ILoanRepository).Loan(BookFK, UserFK, PlannedReturnDate, UserFK);

    public int Return(int ID, int UpdatedBy)
      => (Repository as ILoanRepository).Return(ID, UpdatedBy);
  }
}
