using DAL.Abstract.Model;
using DAL.Abstract.Repository.Database;
using DAL.Abstract.Repository.Model;
using DAL.Model;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace DAL.Repository.Database
{
  internal class BookDatabaseRepository : AbstractDatabaseRepository<BookModel, int>, IBookRepository
  {
    public override string EntityName => "Book";
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
        { "ISBN",           SqlDbType.Char },
        { "Title",          SqlDbType.NVarChar },
        { "Summary",        SqlDbType.NVarChar },
        { "Description",    SqlDbType.NVarChar },
        { "IsNew",          SqlDbType.Bit },
        { "PublisherFK",    SqlDbType.Int },
        { "PageCount",      SqlDbType.Int },
        { "PurchasePrice",  SqlDbType.Decimal },
        { "LoanPrice",      SqlDbType.Decimal },
        { "Quantity",       SqlDbType.Int },
        { "ImagePath",      SqlDbType.NVarChar },
      };

    public override int Create(BookModel entity) => throw new NotImplementedException();
    public override int Create(BookModel entity, int CreatedBy) => throw new NotImplementedException();
    public int Create(BookModel entity, IEnumerable<int> Authors, int CreatedBy)
    {
      IList<SqlParameter> parameters = new List<SqlParameter>();
      string[] unusedProperties = typeof(IModel<int>).GetProperties().Select(x => x.Name).ToArray();

      foreach (var item in DbKeyTypePairs)
      {
        if (unusedProperties.Contains(item.Key)) continue;
        parameters.Add(new SqlParameter()
        {
          ParameterName = "@" + item.Key,
          Direction = ParameterDirection.Input,
          SqlDbType = item.Value,
          Value = typeof(BookModel).GetProperty(item.Key).GetValue(entity),
        });
      }

      parameters.Add(new SqlParameter()
      {
        ParameterName = "@Authors",
        Direction = ParameterDirection.Input,
        SqlDbType = SqlDbType.Structured,
        Value = Authors,
      });

      parameters.Add(new SqlParameter()
      {
        ParameterName = "@CreatedBy",
        Direction = ParameterDirection.Input,
        SqlDbType = DbKeyTypePairs["CreatedBy"],
        Value = CreatedBy,
      });

      SqlParameter returnValue = new SqlParameter()
      {
        Direction = ParameterDirection.ReturnValue
      };
      parameters.Add(returnValue);

      SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.StoredProcedure, EntityName + nameof(Create), parameters.ToArray());

      return int.Parse(returnValue.Value.ToString());
    }

    public override int Update(BookModel entity) => throw new NotImplementedException();
    public override int Update(int ID, BookModel entity) => throw new NotImplementedException();
    public override int Update(int ID, BookModel entity, int UpdatedBy) => throw new NotImplementedException();
    public int Update(BookModel entity, IEnumerable<int> Authors, int UpdatedBy) => Update(entity.ID, entity, Authors, UpdatedBy);
    public int Update(int ID, BookModel entity, IEnumerable<int> Authors, int UpdatedBy)
    {
      IList<SqlParameter> parameters = new List<SqlParameter>();
      string[] unusedProperties = typeof(IModel<int>).GetProperties().Select(x => x.Name).ToArray();

      foreach (var item in DbKeyTypePairs)
      {
        if (unusedProperties.Contains(item.Key)) continue;
        parameters.Add(new SqlParameter()
        {
          ParameterName = "@" + item.Key,
          Direction = ParameterDirection.Input,
          SqlDbType = item.Value,
          Value = typeof(BookModel).GetProperty(item.Key).GetValue(entity),
        });
      }

      parameters.Add(new SqlParameter()
      {
        ParameterName = "@ID",
        Direction = ParameterDirection.Input,
        SqlDbType = DbKeyTypePairs["ID"],
        Value = ID,
      });

      parameters.Add(new SqlParameter()
      {
        ParameterName = "@Authors",
        Direction = ParameterDirection.Input,
        SqlDbType = SqlDbType.Structured,
        Value = Authors,
      });

      parameters.Add(new SqlParameter()
      {
        ParameterName = "@UpdatedBy",
        Direction = ParameterDirection.Input,
        SqlDbType = DbKeyTypePairs["UpdatedBy"],
        Value = UpdatedBy,
      });

      SqlParameter returnValue = new SqlParameter()
      {
        Direction = ParameterDirection.ReturnValue
      };
      parameters.Add(returnValue);

      SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.StoredProcedure, EntityName + nameof(Update), parameters.ToArray());

      return int.Parse(returnValue.Value.ToString());
    }
  }
}
