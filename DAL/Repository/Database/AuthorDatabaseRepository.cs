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
  internal class AuthorDatabaseRepository : AbstractDatabaseRepository<AuthorModel, int>, IAuthorRepository
  {
    public override string EntityName => "Author";
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
        { "FName",          SqlDbType.NVarChar },
        { "LName",          SqlDbType.NVarChar },
        { "BirthDate",      SqlDbType.Date },
        { "ImagePath",      SqlDbType.NVarChar },
        { "Biography",      SqlDbType.NVarChar },
      };

    public IEnumerable<AuthorModel> ReadByBookFK(int BookFK)
    {
      IList<SqlParameter> parameters = new List<SqlParameter>()
      {
        new SqlParameter()
        {
          ParameterName = "@BookFK",
          Direction = ParameterDirection.Input,
          SqlDbType = SqlDbType.Int,
          Value = BookFK,
        }
      };

      SqlDataReader reader = SqlHelper.ExecuteReader(ConnectionString, CommandType.StoredProcedure, EntityName + nameof(ReadByBookFK), parameters.ToArray());

      while (reader.Read())
      {
        yield return DbKeyTypePairs.Keys.Aggregate(Activator.CreateInstance<AuthorModel>(), (obj, prop) =>
        {
          typeof(AuthorModel).GetProperty(prop).SetValue(obj, reader[prop]);
          return obj;
        });
      }
    }
  }
}
