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
        { "SenderFName",      SqlDbType.NVarChar },
        { "SenderLName",      SqlDbType.NVarChar },
        { "SenderEmail",      SqlDbType.NVarChar },
        { "SenderDate",       SqlDbType.DateTime },
        { "SenderMessage",    SqlDbType.NVarChar },
        { "ResponderUserFK",  SqlDbType.Int },
        { "ResponderDate",    SqlDbType.DateTime },
        { "ResponderMessage", SqlDbType.NVarChar },
      };

    public Int32 Send(MessageModel entity) => Send(entity.SenderFName, entity.SenderLName, entity.SenderEmail, entity.SenderMessage);
    public Int32 Send(String SenderFName, String SenderLName, String SenderEmail, String SenderMessage)
    {
      IList<SqlParameter> parameters = new List<SqlParameter>()
      {
        new SqlParameter()
        {
          ParameterName = "@SenderFName",
          Direction = ParameterDirection.Input,
          SqlDbType = DbKeyTypePairs["SenderFName"],
          Value = SenderFName,
        },
        new SqlParameter()
        {
          ParameterName = "@SenderLName",
          Direction = ParameterDirection.Input,
          SqlDbType = DbKeyTypePairs["SenderLName"],
          Value = SenderLName,
        },
        new SqlParameter()
        {
          ParameterName = "@SenderEmail",
          Direction = ParameterDirection.Input,
          SqlDbType = DbKeyTypePairs["SenderEmail"],
          Value = SenderEmail,
        },
        new SqlParameter()
        {
          ParameterName = "@SenderMessage",
          Direction = ParameterDirection.Input,
          SqlDbType = DbKeyTypePairs["SenderMessage"],
          Value = SenderMessage,
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

    public Int32 Respond(MessageModel entity) => Respond(entity.ID, entity.ResponderUserFK, entity.ResponderMessage);
    public Int32 Respond(Int32 ID, Int32 ResponderUserFK, String ResponderMessage)
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
