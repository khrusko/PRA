using System;

using DAL.Model;

namespace DAL.Abstract.Repository.Model
{
  public interface ILoanRepository : IModelRepository<LoanModel, Int32>
  {
    Int32 Loan(LoanModel entity);
    Int32 Loan(LoanModel entity, Int32 CreatedBy);
    Int32 Loan(Int32 BookFK, Int32 UserFK, DateTime PlannedReturnDate, Int32 CreatedBy);
    Int32 Return(LoanModel entity);
    Int32 Return(LoanModel entity, Int32 UpdatedBy);
    Int32 Return(Int32 ID, Int32 UpdatedBy);
  }
}
