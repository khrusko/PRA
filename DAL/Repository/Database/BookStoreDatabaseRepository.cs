using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Xml.Linq;

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
          ParameterName = "@Name",
          Direction = ParameterDirection.Input,
          SqlDbType = DbKeyTypePairs["Name"],
          Value = entity.Name,
        },
        new SqlParameter()
        {
          ParameterName = "@OIB",
          Direction = ParameterDirection.Input,
          SqlDbType = DbKeyTypePairs["OIB"],
          Value = entity.OIB,
        },
        new SqlParameter()
        {
          ParameterName = "@DelayPricePerDay",
          Direction = ParameterDirection.Input,
          SqlDbType = DbKeyTypePairs["DelayPricePerDay"],
          Value = entity.DelayPricePerDay,
        },
        new SqlParameter()
        {
          ParameterName = "@Email",
          Direction = ParameterDirection.Input,
          SqlDbType = DbKeyTypePairs["Email"],
          Value = entity.Email,
        },
        new SqlParameter()
        {
          ParameterName = "@UpdatedBy",
          Direction = ParameterDirection.Input,
          SqlDbType = DbKeyTypePairs["UpdatedBy"],
          Value = UpdatedBy,
        }
      };

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
