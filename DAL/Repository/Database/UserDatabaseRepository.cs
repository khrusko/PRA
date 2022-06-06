using DAL.Abstract.Repository.Database;
using DAL.Abstract.Repository.Model;
using DAL.Status;
using DAL.Model;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace DAL.Repository.Database
{
  internal class UserDatabaseRepository : AbstractDatabaseRepository<UserModel, int>, IUserRepository
  {
    public override string EntityName => "User";
    public override IDictionary<string, SqlDbType> DbKeyTypePairs { get; }
      = new Dictionary<string, SqlDbType>()
      {
        { "ID",                       SqlDbType.Int },
        { "CreateDate",               SqlDbType.DateTime },
        { "CreatedBy",                SqlDbType.Int },
        { "UpdateDate",               SqlDbType.DateTime },
        { "UpdatedBy",                SqlDbType.Int },
        { "DeleteDate",               SqlDbType.DateTime },
        { "DeletedBy",                SqlDbType.Int },
        { "UserID",                   SqlDbType.Char },
        { "FName",                    SqlDbType.NVarChar },
        { "LName",                    SqlDbType.NVarChar },
        { "Email",                    SqlDbType.NVarChar },
        { "PasswordHash",             SqlDbType.NVarChar },
        { "ImagePath",                SqlDbType.NVarChar },
        { "Address",                  SqlDbType.NVarChar },
        { "IsAdmin",                  SqlDbType.Bit },
        { "GUID",                     SqlDbType.UniqueIdentifier },
        { "RegistrationIsApproved",   SqlDbType.Bit },
        { "RegistrationDate",         SqlDbType.DateTime },
        { "ResetPasswordIsApproved",  SqlDbType.Bit },
        { "ResetPasswordDate",        SqlDbType.DateTime },
      };

    public UserModel Login(string Email, string Password)
    {
      IList<SqlParameter> parameters = new List<SqlParameter>()
      {
        new SqlParameter()
        {
          ParameterName = "@Email",
          Direction = ParameterDirection.Input,
          SqlDbType = DbKeyTypePairs["Email"],
          Value = Email,
        },
        new SqlParameter()
        {
          ParameterName = "@Password",
          Direction = ParameterDirection.Input,
          SqlDbType = SqlDbType.NVarChar,
          Value = Password,
        }
      };

      SqlDataReader reader = SqlHelper.ExecuteReader(ConnectionString, CommandType.StoredProcedure, EntityName + nameof(Login), parameters.ToArray());

      return reader.Read()
        ? DbKeyTypePairs.Keys.Aggregate(Activator.CreateInstance<UserModel>(), (obj, prop) =>
        {
          typeof(UserModel).GetProperty(prop).SetValue(obj, reader[prop]);
          return obj;
        })
        : default(UserModel);
    }

    public int Register(string FName, string LName, string Email, string Password, bool IsAdmin)
    {
      IList<SqlParameter> parameters = new List<SqlParameter>()
      {
        new SqlParameter()
        {
          ParameterName = "@FName",
          Direction = ParameterDirection.Input,
          SqlDbType = DbKeyTypePairs["FName"],
          Value = FName,
        },
        new SqlParameter()
        {
          ParameterName = "@LName",
          Direction = ParameterDirection.Input,
          SqlDbType = DbKeyTypePairs["LName"],
          Value = LName,
        },
        new SqlParameter()
        {
          ParameterName = "@Email",
          Direction = ParameterDirection.Input,
          SqlDbType = DbKeyTypePairs["Email"],
          Value = Email,
        },
        new SqlParameter()
        {
          ParameterName = "@Password",
          Direction = ParameterDirection.Input,
          SqlDbType = SqlDbType.NVarChar,
          Value = Password,
        },
        new SqlParameter()
        {
          ParameterName = "@IsAdmin",
          Direction = ParameterDirection.Input,
          SqlDbType = DbKeyTypePairs["IsAdmin"],
          Value = IsAdmin,
        }
      };

      SqlParameter returnValue = new SqlParameter()
      {
        Direction = ParameterDirection.ReturnValue
      };
      parameters.Add(returnValue);

      SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.StoredProcedure, EntityName + nameof(Register), parameters.ToArray());

      return int.Parse(returnValue.Value.ToString());
    }

    public RegistrationStatus CheckRegistrationStatus(Guid GUID)
    {
      IList<SqlParameter> parameters = new List<SqlParameter>()
      {
        new SqlParameter()
        {
          ParameterName = "@GUID",
          Direction = ParameterDirection.Input,
          SqlDbType = DbKeyTypePairs["GUID"],
          Value = GUID,
        }
      };

      SqlParameter returnValue = new SqlParameter()
      {
        Direction = ParameterDirection.ReturnValue
      };
      parameters.Add(returnValue);

      SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.StoredProcedure, EntityName + nameof(CheckRegistrationStatus), parameters.ToArray());

      return (RegistrationStatus)Enum.Parse(typeof(RegistrationStatus), returnValue.Value.ToString());
    }

    public int ConfirmRegistration(Guid GUID)
    {
      IList<SqlParameter> parameters = new List<SqlParameter>()
      {
        new SqlParameter()
        {
          ParameterName = "@GUID",
          Direction = ParameterDirection.Input,
          SqlDbType = DbKeyTypePairs["GUID"],
          Value = GUID,
        }
      };

      SqlParameter returnValue = new SqlParameter()
      {
        Direction = ParameterDirection.ReturnValue
      };
      parameters.Add(returnValue);

      SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.StoredProcedure, EntityName + nameof(ConfirmRegistration), parameters.ToArray());

      return int.Parse(returnValue.Value.ToString());
    }

    public UserModel ReadByEmail(string Email)
    {
      IList<SqlParameter> parameters = new List<SqlParameter>()
      {
        new SqlParameter()
        {
          ParameterName = "@Email",
          Direction = ParameterDirection.Input,
          SqlDbType = DbKeyTypePairs["Email"],
          Value = Email,
        }
      };

      SqlDataReader reader = SqlHelper.ExecuteReader(ConnectionString, CommandType.StoredProcedure, EntityName + nameof(ReadByEmail), parameters.ToArray());

      return reader.Read()
        ? DbKeyTypePairs.Keys.Aggregate(Activator.CreateInstance<UserModel>(), (obj, prop) =>
        {
          typeof(UserModel).GetProperty(prop).SetValue(obj, reader[prop]);
          return obj;
        })
        : default(UserModel);
    }

    public int RequestResetPassword(string Email)
    {
      IList<SqlParameter> parameters = new List<SqlParameter>()
      {
        new SqlParameter()
        {
          ParameterName = "@Email",
          Direction = ParameterDirection.Input,
          SqlDbType = DbKeyTypePairs["Email"],
          Value = Email,
        }
      };

      SqlParameter returnValue = new SqlParameter()
      {
        Direction = ParameterDirection.ReturnValue
      };
      parameters.Add(returnValue);

      SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.StoredProcedure, EntityName + nameof(RequestResetPassword), parameters.ToArray());

      return int.Parse(returnValue.Value.ToString());
    }

    public ResetPasswordStatus CheckResetPasswordStatus(Guid GUID)
    {
      IList<SqlParameter> parameters = new List<SqlParameter>()
      {
        new SqlParameter()
        {
          ParameterName = "@GUID",
          Direction = ParameterDirection.Input,
          SqlDbType = DbKeyTypePairs["GUID"],
          Value = GUID,
        }
      };

      SqlParameter returnValue = new SqlParameter()
      {
        Direction = ParameterDirection.ReturnValue
      };
      parameters.Add(returnValue);

      SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.StoredProcedure, EntityName + nameof(CheckResetPasswordStatus), parameters.ToArray());

      return (ResetPasswordStatus)Enum.Parse(typeof(ResetPasswordStatus), returnValue.Value.ToString());
    }

    public int ResetPassword(Guid GUID, string Password)
    {
      IList<SqlParameter> parameters = new List<SqlParameter>()
      {
        new SqlParameter()
        {
          ParameterName = "@GUID",
          Direction = ParameterDirection.Input,
          SqlDbType = DbKeyTypePairs["GUID"],
          Value = GUID,
        },
        new SqlParameter()
        {
          ParameterName = "@Password",
          Direction = ParameterDirection.Input,
          SqlDbType = SqlDbType.NVarChar,
          Value = Password,
        }
      };

      SqlParameter returnValue = new SqlParameter()
      {
        Direction = ParameterDirection.ReturnValue
      };
      parameters.Add(returnValue);

      SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.StoredProcedure, EntityName + nameof(ResetPassword), parameters.ToArray());

      return int.Parse(returnValue.Value.ToString());
    }
  }
}
