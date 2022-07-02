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
  internal class SubscriptionDatabaseRepository : AbstractDatabaseRepository<SubscriptionModel, Int32>, ISubscriptionRepository
  {
    public override String EntityName => "Subscription";
    public override IDictionary<String, SqlDbType> DbKeyTypePairs { get; }
      = new Dictionary<String, SqlDbType>()
      {
        { "ID",               SqlDbType.Int },
        { "CreateDate",       SqlDbType.DateTime },
        { "CreatedBy",        SqlDbType.Int },
        { "UpdateDate",       SqlDbType.DateTime },
        { "UpdatedBy",        SqlDbType.Int },
        { "DeleteDate",       SqlDbType.DateTime },
        { "DeletedBy",        SqlDbType.Int },
        { "BookFK",           SqlDbType.Int },
        { "UserFK",           SqlDbType.Int },
        { "SubscriptionDate", SqlDbType.DateTime },
        { "IsResolved",       SqlDbType.Bit },
        { "ResolvedDate",     SqlDbType.DateTime },
      };

    public Int32 Subscribe(SubscriptionModel entity) => Subscribe(entity, entity.CreatedBy);
    public Int32 Subscribe(SubscriptionModel entity, Int32 CreatedBy) => Subscribe(entity.BookFK, entity.UserFK, CreatedBy);
    public Int32 Subscribe(Int32 BookFK, Int32 UserFK, Int32 CreatedBy)
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
          ParameterName = "@CreatedBy",
          Direction = ParameterDirection.Input,
          SqlDbType = DbKeyTypePairs["CreatedBy"],
          Value = CreatedBy,
        },
      };

      var returnValue = new SqlParameter()
      {
        Direction = ParameterDirection.ReturnValue
      };
      parameters.Add(returnValue);

      _ = SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.StoredProcedure, EntityName + nameof(Subscribe), parameters.ToArray());

      return Int32.Parse(returnValue.Value.ToString());
    }

    public IEnumerable<SubscriptionModel> ReadAllUnresolved()
    {
      SqlDataReader reader = SqlHelper.ExecuteReader(ConnectionString, CommandType.StoredProcedure, EntityName + nameof(ReadAllUnresolved));

      while (reader.Read())
      {
        yield return DbKeyTypePairs.Keys.Aggregate(Activator.CreateInstance<SubscriptionModel>(), (obj, prop) =>
        {
          typeof(SubscriptionModel).GetProperty(prop).SetValue(obj, reader[prop] == DBNull.Value ? default : reader[prop]);
          return obj;
        });
      }
    }

    public Int32 Resolve(SubscriptionModel entity) => Resolve(entity.ID);
    public Int32 Resolve(Int32 ID)
    {
      IList<SqlParameter> parameters = new List<SqlParameter>()
      {
        new SqlParameter()
        {
          ParameterName = "@ID",
          Direction = ParameterDirection.Input,
          SqlDbType = DbKeyTypePairs["ID"],
          Value = ID,
        }
      };

      var returnValue = new SqlParameter()
      {
        Direction = ParameterDirection.ReturnValue
      };
      parameters.Add(returnValue);

      _ = SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.StoredProcedure, EntityName + nameof(Resolve), parameters.ToArray());

      return Int32.Parse(returnValue.Value.ToString());
    }
  }
}
