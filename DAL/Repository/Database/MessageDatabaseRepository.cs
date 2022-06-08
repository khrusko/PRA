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
  internal class MessageDatabaseRepository : AbstractDatabaseRepository<MessageModel, Int32>, IMessageRepository
  {
    public override String EntityName => "Message";
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
        { "SenderUserFK",     SqlDbType.Int },
        { "SenderDate",       SqlDbType.DateTime },
        { "SenderMessage",    SqlDbType.NVarChar },
        { "ResponderUserFK",  SqlDbType.Int },
        { "ResponderDate",    SqlDbType.DateTime },
        { "ResponderMessage", SqlDbType.NVarChar },
      };

    public Int32 Send(MessageModel entity) => Send(entity, entity.CreatedBy);
    public Int32 Send(MessageModel entity, Int32 CreatedBy) => Send(entity.SenderUserFK, entity.SenderMessage, CreatedBy);
    public Int32 Send(Int32 SenderUserFK, String SenderMessage, Int32 CreatedBy)
    {
      IList<SqlParameter> parameters = new List<SqlParameter>()
      {
        new SqlParameter()
        {
          ParameterName = "@SenderUserFK",
          Direction = ParameterDirection.Input,
          SqlDbType = DbKeyTypePairs["SenderUserFK"],
          Value = SenderUserFK,
        },
        new SqlParameter()
        {
          ParameterName = "@SenderMessage",
          Direction = ParameterDirection.Input,
          SqlDbType = DbKeyTypePairs["SenderMessage"],
          Value = SenderMessage,
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

      _ = SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.StoredProcedure, EntityName + nameof(Send), parameters.ToArray());

      return Int32.Parse(returnValue.Value.ToString());
    }

    public Int32 Respond(MessageModel entity) => Respond(entity, entity.UpdatedBy);
    public Int32 Respond(MessageModel entity, Int32 UpdatedBy) => Respond(entity.ID, entity.ResponderUserFK, entity.ResponderMessage, UpdatedBy);
    public Int32 Respond(Int32 ID, Int32 ResponderUserFK, String ResponderMessage, Int32 UpdatedBy)
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
          ParameterName = "@ResponderUserFK",
          Direction = ParameterDirection.Input,
          SqlDbType = DbKeyTypePairs["ResponderUserFK"],
          Value = ResponderUserFK,
        },
        new SqlParameter()
        {
          ParameterName = "@ResponderMessage",
          Direction = ParameterDirection.Input,
          SqlDbType = DbKeyTypePairs["ResponderMessage"],
          Value = ResponderMessage,
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

      _ = SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.StoredProcedure, EntityName + nameof(Send), parameters.ToArray());

      return Int32.Parse(returnValue.Value.ToString());
    }
  }
}
