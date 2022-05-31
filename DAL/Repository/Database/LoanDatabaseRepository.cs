using DAL.Abstract.Repository.DAO;
using DAL.Abstract.Repository.Database;
using DAL.DAO;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository.Database
{
  internal class LoanDatabaseRepository : AbstractDatabaseRepository<LoanDAO, int>, ILoanRepository
  {
    public override string EntityName => "Loan";
    public override IDictionary<string, SqlDbType> DbKeyTypePairs { get; }
      = new Dictionary<string, SqlDbType>()
      {
        { "ID",                 SqlDbType.Int },
        { "CreateDate",         SqlDbType.DateTime },
        { "CreatedBy",          SqlDbType.Int },
        { "UpdateDate",         SqlDbType.DateTime },
        { "UpdatedBy",          SqlDbType.Int },
        { "DeleteDate",         SqlDbType.DateTime },
        { "DeletedBy",          SqlDbType.Int },
        { "BookFK",             SqlDbType.Int },
        { "UserFK",             SqlDbType.Int },
        { "LoanPrice",          SqlDbType.Decimal },
        { "LoanDate",           SqlDbType.DateTime },
        { "PlannedReturnDate",  SqlDbType.SmallDateTime },
        { "ReturnDate",         SqlDbType.SmallDateTime },
        { "DelayDays",          SqlDbType.Int },
        { "DelayPricePerDay",   SqlDbType.Decimal },
      };

    public int Loan(LoanDAO entity) => Loan(entity, entity.CreatedBy);
    public int Loan(LoanDAO entity, int CreatedBy) => Loan(entity.BookFK, entity.UserFK, entity.PlannedReturnDate, entity.CreatedBy);
    public int Loan(int BookFK, int UserFK, DateTime PlannedReturnDate, int CreatedBy)
    {
      IList<SqlParameter> parameters = new List<SqlParameter>()
      {
        new SqlParameter()
        {
          ParameterName = "@BookFK",
          Direction = ParameterDirection.Input,
          SqlDbType = DbKeyTypePairs["BookFK"],
          Value = BookFK,
        },
        new SqlParameter()
        {
          ParameterName = "@UserFK",
          Direction = ParameterDirection.Input,
          SqlDbType = DbKeyTypePairs["UserFK"],
          Value = UserFK,
        },
        new SqlParameter()
        {
          ParameterName = "@PlannedReturnDate",
          Direction = ParameterDirection.Input,
          SqlDbType = DbKeyTypePairs["PlannedReturnDate"],
          Value = PlannedReturnDate,
        },
        new SqlParameter()
        {
          ParameterName = "@CreatedBy",
          Direction = ParameterDirection.Input,
          SqlDbType = DbKeyTypePairs["CreatedBy"],
          Value = CreatedBy,
        }
      };

      SqlParameter returnValue = new SqlParameter()
      {
        Direction = ParameterDirection.ReturnValue
      };
      parameters.Add(returnValue);

      SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.StoredProcedure, EntityName + nameof(Loan), parameters.ToArray());

      return int.Parse(returnValue.Value.ToString());
    }

    public int Return(LoanDAO entity) => Return(entity, entity.UpdatedBy);
    public int Return(LoanDAO entity, int UpdatedBy) => Return(entity.ID, UpdatedBy);
    public int Return(int ID, int UpdatedBy)
    {
      IList<SqlParameter> parameters = new List<SqlParameter>()
      {
        new SqlParameter()
        {
          ParameterName = "@ID",
          Direction = ParameterDirection.Input,
          SqlDbType = DbKeyTypePairs["ID"],
          Value = ID,
        },
        new SqlParameter()
        {
          ParameterName = "@UpdatedBy",
          Direction = ParameterDirection.Input,
          SqlDbType = DbKeyTypePairs["UpdatedBy"],
          Value = UpdatedBy,
        }
      };

      SqlParameter returnValue = new SqlParameter()
      {
        Direction = ParameterDirection.ReturnValue
      };
      parameters.Add(returnValue);

      SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.StoredProcedure, EntityName + nameof(Return), parameters.ToArray());

      return int.Parse(returnValue.Value.ToString());
    }
  }
}
