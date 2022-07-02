using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

using DAL.Abstract.Repository.Database;
using DAL.Abstract.Repository.Model;
using DAL.Model;
using DAL.Status;

using Microsoft.ApplicationBlocks.Data;

namespace DAL.Repository.Database
{
  internal class UserDatabaseRepository : AbstractDatabaseRepository<UserModel, Int32>, IUserRepository
  {
    public override String EntityName => "User";
    public override IDictionary<String, SqlDbType> DbKeyTypePairs { get; }
      = new Dictionary<String, SqlDbType>()
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

    public UserModel Login(String Email, String Password)
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
          typeof(UserModel).GetProperty(prop).SetValue(obj, reader[prop] == DBNull.Value ? default : reader[prop]);
          return obj;
        })
        : default;
    }

    public Int32 Register(String FName, String LName, String Email, String Password, Boolean IsAdmin)
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

      var returnValue = new SqlParameter()
      {
        Direction = ParameterDirection.ReturnValue
      };
      parameters.Add(returnValue);

      _ = SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.StoredProcedure, EntityName + nameof(Register), parameters.ToArray());

      return Int32.Parse(returnValue.Value.ToString());
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

      var returnValue = new SqlParameter()
      {
        Direction = ParameterDirection.ReturnValue
      };
      parameters.Add(returnValue);

      _ = SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.StoredProcedure, EntityName + nameof(CheckRegistrationStatus), parameters.ToArray());

      return (RegistrationStatus)Enum.Parse(typeof(RegistrationStatus), returnValue.Value.ToString());
    }

    public Int32 ConfirmRegistration(Guid GUID)
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

      var returnValue = new SqlParameter()
      {
        Direction = ParameterDirection.ReturnValue
      };
      parameters.Add(returnValue);

      _ = SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.StoredProcedure, EntityName + nameof(ConfirmRegistration), parameters.ToArray());

      return Int32.Parse(returnValue.Value.ToString());
    }

    public UserModel ReadByEmail(String Email)
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
          typeof(UserModel).GetProperty(prop).SetValue(obj, reader[prop] == DBNull.Value ? default : reader[prop]);
          return obj;
        })
        : default;
    }

    public Int32 RequestResetPassword(String Email)
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

      var returnValue = new SqlParameter()
      {
        Direction = ParameterDirection.ReturnValue
      };
      parameters.Add(returnValue);

      _ = SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.StoredProcedure, EntityName + nameof(RequestResetPassword), parameters.ToArray());

      return Int32.Parse(returnValue.Value.ToString());
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

      var returnValue = new SqlParameter()
      {
        Direction = ParameterDirection.ReturnValue
      };
      parameters.Add(returnValue);

      _ = SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.StoredProcedure, EntityName + nameof(CheckResetPasswordStatus), parameters.ToArray());

      return (ResetPasswordStatus)Enum.Parse(typeof(ResetPasswordStatus), returnValue.Value.ToString());
    }

    public Int32 ResetPassword(Guid GUID, String Password)
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

      var returnValue = new SqlParameter()
      {
        Direction = ParameterDirection.ReturnValue
      };
      parameters.Add(returnValue);

      _ = SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.StoredProcedure, EntityName + nameof(ResetPassword), parameters.ToArray());

      return Int32.Parse(returnValue.Value.ToString());
    }

    public Int32 EditProfile(UserModel entity) => EditProfile(entity, entity.UpdatedBy);
    public Int32 EditProfile(UserModel entity, Int32 UpdatedBy) 
      => EditProfile(entity.ID, entity.FName, entity.LName, entity.ImagePath, entity.Address, UpdatedBy);
    public Int32 EditProfile(Int32 ID, String FName, String LName, String ImagePath, String Address, Int32 UpdatedBy)
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
          ParameterName = "@ImagePath",
          Direction = ParameterDirection.Input,
          SqlDbType = DbKeyTypePairs["ImagePath"],
          Value = ImagePath,
        },
        new SqlParameter()
        {
          ParameterName = "@Address",
          Direction = ParameterDirection.Input,
          SqlDbType = DbKeyTypePairs["Address"],
          Value = Address,
        },
        new SqlParameter()
        {
          ParameterName = "@UpdatedBy",
          Direction = ParameterDirection.Input,
          SqlDbType = DbKeyTypePairs["UpdatedBy"],
          Value = UpdatedBy,
        },
      };

      var returnValue = new SqlParameter()
      {
        Direction = ParameterDirection.ReturnValue
      };
      parameters.Add(returnValue);

      _ = SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.StoredProcedure, EntityName + nameof(EditProfile), parameters.ToArray());

      return Int32.Parse(returnValue.Value.ToString());
    }
  }
}
