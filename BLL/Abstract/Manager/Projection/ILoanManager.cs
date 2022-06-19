using System;
using System.Collections.Generic;

using BLL.Projection;

using DAL.Model;

namespace BLL.Abstract.Manager.Projection
{
  public interface ILoanManager : IProjectionManager<LoanModel, LoanProjection, Int32>
  {
    Int32 Loan(LoanProjection projection);
    Int32 Loan(Int32 bookFK, Int32 userFK, DateTime plannedReturnDate);
    Int32 Return(Int32 ID, Int32 updatedBy);
    IEnumerable<LoanProjection> GetByUserFK(Int32 userFK);
    IEnumerable<LoanProjection> GetActiveByUserFK(Int32 userFK);
  }
}
