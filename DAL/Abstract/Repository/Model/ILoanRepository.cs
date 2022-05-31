using DAL.Model;
using System;

namespace DAL.Abstract.Repository.Model
{
  public interface ILoanRepository : IModelRepository<LoanModel, int>
  {
    int Loan(LoanModel entity);
    int Loan(LoanModel entity, int CreatedBy);
    int Loan(int BookFK, int UserFK, DateTime PlannedReturnDate, int CreatedBy);
    int Return(LoanModel entity);
    int Return(LoanModel entity, int UpdatedBy);
    int Return(int ID, int UpdatedBy);
  }
}
