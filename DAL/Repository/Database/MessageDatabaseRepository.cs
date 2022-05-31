using DAL.Abstract.Repository.Database;
using DAL.Abstract.Repository.Model;
using DAL.Model;
using Microsoft.ApplicationBlocks.Data;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace DAL.Repository.Database
{
  internal class MessageDatabaseRepository : AbstractDatabaseRepository<MessageModel, int>, IMessageRepository
  {
    public override string EntityName => "Message";
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
        { "SenderUserFK",     SqlDbType.Int },
        { "SenderDate",       SqlDbType.DateTime },
        { "SenderMessage",    SqlDbType.NVarChar },
        { "ResponderUserFK",  SqlDbType.Int },
        { "ResponderDate",    SqlDbType.DateTime },
        { "ResponderMessage", SqlDbType.NVarChar },
      };

    public int Send(MessageModel entity) => Send(entity, entity.CreatedBy);
    public int Send(MessageModel entity, int CreatedBy) => Send(entity.SenderUserFK, entity.SenderMessage, CreatedBy);
    public int Send(int SenderUserFK, string SenderMessage, int CreatedBy)
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

      SqlParameter returnValue = new SqlParameter()
      {
        Direction = ParameterDirection.ReturnValue
      };
      parameters.Add(returnValue);

      SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.StoredProcedure, EntityName + nameof(Send), parameters.ToArray());

      return int.Parse(returnValue.Value.ToString());
    }

    public int Respond(MessageModel entity) => Respond(entity, entity.UpdatedBy);
    public int Respond(MessageModel entity, int UpdatedBy) => Respond(entity.ID, entity.ResponderUserFK, entity.ResponderMessage, UpdatedBy);
    public int Respond(int ID, int ResponderUserFK, string ResponderMessage, int UpdatedBy)
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

      SqlParameter returnValue = new SqlParameter()
      {
        Direction = ParameterDirection.ReturnValue
      };
      parameters.Add(returnValue);

      SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.StoredProcedure, EntityName + nameof(Send), parameters.ToArray());

      return int.Parse(returnValue.Value.ToString());
    }
  }
}
