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
  internal class SubscriptionDatabaseRepository : AbstractDatabaseRepository<SubscriptionModel, int>, ISubscriptionRepository
  {
    public override string EntityName => "Subscription";
    public override IDictionary<string, SqlDbType> DbKeyTypePairs { get; }
      = new Dictionary<string, SqlDbType>()
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

    public int Subscribe(SubscriptionModel entity) => Subscribe(entity, entity.CreatedBy);
    public int Subscribe(SubscriptionModel entity, int CreatedBy) => Subscribe(entity.BookFK, entity.UserFK, CreatedBy);
    public int Subscribe(int BookFK, int UserFK, int CreatedBy)
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

      SqlParameter returnValue = new SqlParameter()
      {
        Direction = ParameterDirection.ReturnValue
      };
      parameters.Add(returnValue);

      SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.StoredProcedure, EntityName + nameof(Subscribe), parameters.ToArray());

      return int.Parse(returnValue.Value.ToString());
    }
  }
}
