using DAL.Abstract.Repository.Model;
using DAL.Abstract.Repository.Database;
using DAL.Model;
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
  internal class UserDatabaseRepository : AbstractDatabaseRepository<UserModel, int>, IUserRepository
  {
    public override string EntityName => "User";
    public override IDictionary<string, SqlDbType> DbKeyTypePairs { get; }
      = new Dictionary<string, SqlDbType>()
      {
        { "ID",             SqlDbType.Int },
        { "CreateDate",     SqlDbType.DateTime },
        { "CreatedBy",      SqlDbType.Int },
        { "UpdateDate",     SqlDbType.DateTime },
        { "UpdatedBy",      SqlDbType.Int },
        { "DeleteDate",     SqlDbType.DateTime },
        { "DeletedBy",      SqlDbType.Int },
        { "UserID",         SqlDbType.Char },
        { "FName",          SqlDbType.NVarChar },
        { "LName",          SqlDbType.NVarChar },
        { "Email",          SqlDbType.NVarChar },
        { "PasswordHash",   SqlDbType.NVarChar },
        { "ImagePath",      SqlDbType.NVarChar },
        { "Address",        SqlDbType.NVarChar },
        { "IsAdmin",        SqlDbType.Bit },
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

    public UserModel Register(string UserID, string FName, string LName, string Email, string Password, bool IsAdmin, int CreatedBy)
    {
      IList<SqlParameter> parameters = new List<SqlParameter>()
      {
        new SqlParameter()
        {
          ParameterName = "@UserID",
          Direction = ParameterDirection.Input,
          SqlDbType = DbKeyTypePairs["UserID"],
          Value = UserID,
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
        },
        new SqlParameter()
        {
          ParameterName = "@CreatedBy",
          Direction = ParameterDirection.Input,
          SqlDbType = DbKeyTypePairs["CreatedBy"],
          Value = CreatedBy,
        },
      };

      SqlDataReader reader = SqlHelper.ExecuteReader(ConnectionString, CommandType.StoredProcedure, EntityName + nameof(Register), parameters.ToArray());

      return reader.Read()
        ? DbKeyTypePairs.Keys.Aggregate(Activator.CreateInstance<UserModel>(), (obj, prop) =>
        {
          typeof(UserModel).GetProperty(prop).SetValue(obj, reader[prop]);
          return obj;
        })
        : default(UserModel);
    }
  }
}
