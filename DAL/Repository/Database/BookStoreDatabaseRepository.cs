using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

using DAL.Abstract.Repository.Database;
using DAL.Abstract.Repository.Model;
using DAL.Factory;
using DAL.Model;

using Microsoft.ApplicationBlocks.Data;

namespace DAL.Repository.Database
{
  internal class BookStoreDatabaseRepository : IDatabaseRepository<BookStoreModel, Int32>, IBookStoreRepository
  {
    public String ConnectionString => ConnectionStringFactory.GetConnectionString();
    public String EntityName => "BookStore";
    public IDictionary<String, SqlDbType> DbKeyTypePairs { get; }
      = new Dictionary<String, SqlDbType>()
      {
        { "ID",               SqlDbType.Int },
        { "CreateDate",       SqlDbType.DateTime },
        { "CreatedBy",        SqlDbType.Int },
        { "UpdateDate",       SqlDbType.DateTime },
        { "UpdatedBy",        SqlDbType.Int },
        { "DeleteDate",       SqlDbType.DateTime },
        { "DeletedBy",        SqlDbType.Int },
        { "Name",             SqlDbType.NVarChar },
        { "OIB",              SqlDbType.Char },
        { "DelayPricePerDay", SqlDbType.Decimal },
        { "Email",            SqlDbType.NVarChar },
      };

    public IEnumerable<BookStoreModel> ReadAll()
    {
      IList<SqlParameter> parameters = new List<SqlParameter>();

      SqlDataReader reader = SqlHelper.ExecuteReader(ConnectionString, CommandType.StoredProcedure, EntityName + "Read");

      while (reader.Read())
      {
        yield return DbKeyTypePairs.Keys.Aggregate(Activator.CreateInstance<BookStoreModel>(), (obj, prop) =>
        {
          typeof(BookStoreModel).GetProperty(prop).SetValue(obj, reader[prop] == DBNull.Value ? default : reader[prop]);
          return obj;
        });
      }
    }

    public Int32 Update(BookStoreModel entity) => Update(entity.ID, entity, entity.UpdatedBy);
    public Int32 Update(Int32 ID, BookStoreModel entity) => Update(ID, entity, entity.UpdatedBy);
    public Int32 Update(Int32 ID, BookStoreModel entity, Int32 UpdatedBy)
    {
      IList<SqlParameter> parameters = new List<SqlParameter>();
      String[] unusedProperties = typeof(BookStoreModel).GetProperties().Select(x => x.Name).ToArray();

      foreach (KeyValuePair<String, SqlDbType> item in DbKeyTypePairs)
      {
        if (unusedProperties.Contains(item.Key)) continue;
        parameters.Add(new SqlParameter()
        {
          ParameterName = "@" + item.Key,
          Direction = ParameterDirection.Input,
          SqlDbType = item.Value,
          Value = typeof(BookStoreModel).GetProperty(item.Key).GetValue(entity),
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

    public Int32 Create(BookStoreModel entity) => throw new NotImplementedException();
    public Int32 Create(BookStoreModel entity, Int32 CreatedBy) => throw new NotImplementedException();
    public Int32 Delete(Int32 ID, BookStoreModel entity) => throw new NotImplementedException();
    public Int32 Delete(BookStoreModel entity) => throw new NotImplementedException();
    public Int32 Delete(Int32 ID, Int32 DeletedBy) => throw new NotImplementedException();
    public IEnumerable<BookStoreModel> ReadAllAvailable() => throw new NotImplementedException();
    public BookStoreModel ReadByID(Int32 ID) => throw new NotImplementedException();
    public BookStoreModel ReadByIDAvailable(Int32 ID) => throw new NotImplementedException();
  }
}
