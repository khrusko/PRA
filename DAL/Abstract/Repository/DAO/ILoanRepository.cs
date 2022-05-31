using DAL.DAO;
using System;

namespace DAL.Abstract.Repository.DAO
{
  public interface ILoanRepository : IDAORepository<LoanDAO, int>
  {
    int Loan(LoanDAO entity);
    int Loan(LoanDAO entity, int CreatedBy);
    int Loan(int BookFK, int UserFK, DateTime PlannedReturnDate, int CreatedBy);
    int Return(LoanDAO entity);
    int Return(LoanDAO entity, int UpdatedBy);
    int Return(int ID, int UpdatedBy);
  }
}
