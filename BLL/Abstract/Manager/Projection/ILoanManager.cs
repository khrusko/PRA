using BLL.Projection;
using DAL.Model;
using System;

namespace BLL.Abstract.Manager.Projection
{
  public interface ILoanManager : IProjectionManager<LoanModel, LoanProjection, int>
  {
    int Loan(LoanProjection projection);
    int Loan(int BookFK, int UserFK, DateTime PlannedReturnDate);
    int Return(int ID, int UpdatedBy);
  }
}
