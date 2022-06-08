using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

using DAL.Abstract.Model;
using DAL.Abstract.Repository.Database;
using DAL.Abstract.Repository.Model;
using DAL.Model;

using Microsoft.ApplicationBlocks.Data;

namespace DAL.Repository.Database
{
  internal class BookDatabaseRepository : AbstractDatabaseRepository<BookModel, Int32>, IBookRepository
  {
    public override String EntityName => "Book";
    public override IDictionary<String, SqlDbType> DbKeyTypePairs { get; }
      = new Dictionary<String, SqlDbType>()
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

    public override Int32 Create(BookModel entity) => throw new NotImplementedException();
    public override Int32 Create(BookModel entity, Int32 CreatedBy) => throw new NotImplementedException();
    public Int32 Create(BookModel entity, IEnumerable<Int32> Authors, Int32 CreatedBy)
    {
      IList<SqlParameter> parameters = new List<SqlParameter>();
      String[] unusedProperties = typeof(IModel<Int32>).GetProperties().Select(x => x.Name).ToArray();

      foreach (KeyValuePair<String, SqlDbType> item in DbKeyTypePairs)
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

      var returnValue = new SqlParameter()
      {
        Direction = ParameterDirection.ReturnValue
      };
      parameters.Add(returnValue);

      _ = SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.StoredProcedure, EntityName + nameof(Create), parameters.ToArray());

      return Int32.Parse(returnValue.Value.ToString());
    }

    public override Int32 Update(BookModel entity) => throw new NotImplementedException();
    public override Int32 Update(Int32 ID, BookModel entity) => throw new NotImplementedException();
    public override Int32 Update(Int32 ID, BookModel entity, Int32 UpdatedBy) => throw new NotImplementedException();
    public Int32 Update(BookModel entity, IEnumerable<Int32> Authors, Int32 UpdatedBy) => Update(entity.ID, entity, Authors, UpdatedBy);
    public Int32 Update(Int32 ID, BookModel entity, IEnumerable<Int32> Authors, Int32 UpdatedBy)
    {
      IList<SqlParameter> parameters = new List<SqlParameter>();
      String[] unusedProperties = typeof(IModel<Int32>).GetProperties().Select(x => x.Name).ToArray();

      foreach (KeyValuePair<String, SqlDbType> item in DbKeyTypePairs)
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

      var returnValue = new SqlParameter()
      {
        Direction = ParameterDirection.ReturnValue
      };
      parameters.Add(returnValue);

      _ = SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.StoredProcedure, EntityName + nameof(Update), parameters.ToArray());

      return Int32.Parse(returnValue.Value.ToString());
    }

    public IEnumerable<BookModel> ReadByAuthorFK(Int32 AuthorFK)
    {
      IList<SqlParameter> parameters = new List<SqlParameter>()
      {
        new SqlParameter()
        {
          ParameterName = "@AuthorFK",
          Direction = ParameterDirection.Input,
          SqlDbType = SqlDbType.Int,
          Value = AuthorFK,
        }
      };

      SqlDataReader reader = SqlHelper.ExecuteReader(ConnectionString, CommandType.StoredProcedure, EntityName + nameof(ReadByAuthorFK), parameters.ToArray());

      while (reader.Read())
      {
        yield return DbKeyTypePairs.Keys.Aggregate(Activator.CreateInstance<BookModel>(), (obj, prop) =>
        {
          typeof(BookModel).GetProperty(prop).SetValue(obj, reader[prop] == DBNull.Value ? default : reader[prop]);
          return obj;
        });
      }
    }
  }
}
