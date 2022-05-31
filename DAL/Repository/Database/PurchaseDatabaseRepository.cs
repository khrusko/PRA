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
  internal class PurchaseDatabaseRepository : AbstractDatabaseRepository<PurchaseDAO, int>, IPurchaseRepository
  {
    public override string EntityName => "Purchase";
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
        { "BookFK",         SqlDbType.Int },
        { "UserFK",         SqlDbType.Int },
        { "Quantity",       SqlDbType.Int },
        { "UnitPrice",      SqlDbType.Decimal },
        { "PurchaseDate",   SqlDbType.DateTime },
      };

    public int Purchase(PurchaseDAO entity) => Purchase(entity, entity.CreatedBy);
    public int Purchase(PurchaseDAO entity, int CreatedBy) => Purchase(entity.BookFK, entity.UserFK, entity.Quantity, CreatedBy);
    public int Purchase(int BookFK, int UserFK, int Quantity, int CreatedBy)
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
          ParameterName = "@Quantity",
          Direction = ParameterDirection.Input,
          SqlDbType = DbKeyTypePairs["Quantity"],
          Value = Quantity,
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

      SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.StoredProcedure, EntityName + nameof(Purchase), parameters.ToArray());

      return int.Parse(returnValue.Value.ToString());
    }
  }
}
