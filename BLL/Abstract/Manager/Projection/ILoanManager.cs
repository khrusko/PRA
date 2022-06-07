using System;

using BLL.Projection;

using DAL.Model;

namespace BLL.Abstract.Manager.Projection
{
  public interface ILoanManager : IProjectionManager<LoanModel, LoanProjection, Int32>
  {
    Int32 Loan(LoanProjection projection);
    Int32 Loan(Int32 BookFK, Int32 UserFK, DateTime PlannedReturnDate);
    Int32 Return(Int32 ID, Int32 UpdatedBy);
  }
}
