using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

using DAL.Abstract.Repository.Database;
using DAL.Abstract.Repository.Model;
using DAL.Model;

using Microsoft.ApplicationBlocks.Data;

namespace DAL.Repository.Database
{
  internal class LoanDatabaseRepository : AbstractDatabaseRepository<LoanModel, Int32>, ILoanRepository
  {
    public override String EntityName => "Loan";
    public override IDictionary<String, SqlDbType> DbKeyTypePairs { get; }
      = new Dictionary<String, SqlDbType>()
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

    public Int32 Loan(LoanModel entity) => Loan(entity, entity.CreatedBy);
    public Int32 Loan(LoanModel entity, Int32 CreatedBy) => Loan(entity.BookFK, entity.UserFK, entity.PlannedReturnDate, entity.CreatedBy);
    public Int32 Loan(Int32 BookFK, Int32 UserFK, DateTime PlannedReturnDate, Int32 CreatedBy)
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

      var returnValue = new SqlParameter()
      {
        Direction = ParameterDirection.ReturnValue
      };
      parameters.Add(returnValue);

      _ = SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.StoredProcedure, EntityName + nameof(Loan), parameters.ToArray());

      return Int32.Parse(returnValue.Value.ToString());
    }

    public Int32 Return(LoanModel entity) => Return(entity, entity.UpdatedBy);
    public Int32 Return(LoanModel entity, Int32 UpdatedBy) => Return(entity.ID, UpdatedBy);
    public Int32 Return(Int32 ID, Int32 UpdatedBy)
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

      var returnValue = new SqlParameter()
      {
        Direction = ParameterDirection.ReturnValue
      };
      parameters.Add(returnValue);

      _ = SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.StoredProcedure, EntityName + nameof(Return), parameters.ToArray());

      return Int32.Parse(returnValue.Value.ToString());
    }

    public IEnumerable<LoanModel> ReadByUserFK(Int32 UserFK)
    {
      IList<SqlParameter> parameters = new List<SqlParameter>()
      {
        new SqlParameter()
        {
          ParameterName = "@Active",
          Direction = ParameterDirection.Input,
          SqlDbType = SqlDbType.Bit,
          Value = false,
        },
        new SqlParameter()
        {
          ParameterName = "@UserFK",
          Direction = ParameterDirection.Input,
          SqlDbType = SqlDbType.Int,
          Value = UserFK,
        }
      };

      SqlDataReader reader = SqlHelper.ExecuteReader(ConnectionString, CommandType.StoredProcedure, EntityName + nameof(ReadByUserFK), parameters.ToArray());

      while (reader.Read())
      {
        yield return DbKeyTypePairs.Keys.Aggregate(Activator.CreateInstance<LoanModel>(), (obj, prop) =>
        {
          typeof(LoanModel).GetProperty(prop).SetValue(obj, reader[prop] == DBNull.Value ? default : reader[prop]);
          return obj;
        });
      }
    }
    public IEnumerable<LoanModel> ReadByUserFKActive(Int32 UserFK)
    {
      IList<SqlParameter> parameters = new List<SqlParameter>()
      {
        new SqlParameter()
        {
          ParameterName = "@Active",
          Direction = ParameterDirection.Input,
          SqlDbType = SqlDbType.Bit,
          Value = true,
        },
        new SqlParameter()
        {
          ParameterName = "@UserFK",
          Direction = ParameterDirection.Input,
          SqlDbType = SqlDbType.Int,
          Value = UserFK,
        }
      };

      SqlDataReader reader = SqlHelper.ExecuteReader(ConnectionString, CommandType.StoredProcedure, EntityName + nameof(ReadByUserFK), parameters.ToArray());

      while (reader.Read())
      {
        yield return DbKeyTypePairs.Keys.Aggregate(Activator.CreateInstance<LoanModel>(), (obj, prop) =>
        {
          typeof(LoanModel).GetProperty(prop).SetValue(obj, reader[prop] == DBNull.Value ? default : reader[prop]);
          return obj;
        });
      }
    }

    public IEnumerable<LoanModel> ReadLoansInTimeout()
    {
      SqlDataReader reader = SqlHelper.ExecuteReader(ConnectionString, CommandType.StoredProcedure, EntityName + nameof(ReadLoansInTimeout));

      while (reader.Read())
      {
        yield return DbKeyTypePairs.Keys.Aggregate(Activator.CreateInstance<LoanModel>(), (obj, prop) =>
        {
          typeof(LoanModel).GetProperty(prop).SetValue(obj, reader[prop] == DBNull.Value ? default : reader[prop]);
          return obj;
        });
      }
    }

    public Int32 CountByBookFK(Int32 BookFK)
    {
      IList<SqlParameter> parameters = new List<SqlParameter>()
      {
        new SqlParameter()
        {
          ParameterName = "@BookFK",
          Direction = ParameterDirection.Input,
          SqlDbType = DbKeyTypePairs["BookFK"],
          Value = BookFK,
        }
      };

      return Int32.Parse(SqlHelper.ExecuteScalar(ConnectionString, CommandType.StoredProcedure, EntityName + nameof(CountByBookFK), parameters.ToArray()).ToString());
    }
  }
}
