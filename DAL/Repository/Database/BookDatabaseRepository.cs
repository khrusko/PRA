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
        { "AuthorFK",       SqlDbType.Int },
        { "PageCount",      SqlDbType.Int },
        { "PurchasePrice",  SqlDbType.Decimal },
        { "LoanPrice",      SqlDbType.Decimal },
        { "Quantity",       SqlDbType.Int },
        { "ImagePath",      SqlDbType.NVarChar },
      };

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
