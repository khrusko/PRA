using DAL.Abstract.Repository.DAO;
using DAL.Abstract.Repository.Database;
using DAL.DAO;
using DAL.Factory;
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
  internal class BookStoreDatabaseRepository : IDatabaseRepository<BookStoreDAO, int>, IBookStoreRepository
  {
    public string ConnectionString => ConnectionStringFactory.GetConnectionString();
    public string EntityName => "BookStore";
    public IDictionary<string, SqlDbType> DbKeyTypePairs { get; }
      = new Dictionary<string, SqlDbType>()
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

    public IEnumerable<BookStoreDAO> Read()
    {
      IList<SqlParameter> parameters = new List<SqlParameter>();

      SqlDataReader reader = SqlHelper.ExecuteReader(ConnectionString, CommandType.StoredProcedure, EntityName + nameof(Read));

      while (reader.Read())
      {
        yield return DbKeyTypePairs.Keys.Aggregate(Activator.CreateInstance<BookStoreDAO>(), (obj, prop) =>
        {
          typeof(BookStoreDAO).GetProperty(prop).SetValue(obj, reader[prop]);
          return obj;
        });
      }
    }

    public int Update(BookStoreDAO entity) => Update(entity.ID, entity, entity.UpdatedBy);
    public int Update(int ID, BookStoreDAO entity) => Update(ID, entity, entity.UpdatedBy);
    public int Update(int ID, BookStoreDAO entity, int UpdatedBy)
    {
      IList<SqlParameter> parameters = new List<SqlParameter>();
      string[] unusedProperties = typeof(BookStoreDAO).GetProperties().Select(x => x.Name).ToArray();

      foreach (var item in DbKeyTypePairs)
      {
        if (unusedProperties.Contains(item.Key)) continue;
        parameters.Add(new SqlParameter()
        {
          ParameterName = "@" + item.Key,
          Direction = ParameterDirection.Input,
          SqlDbType = item.Value,
          Value = typeof(BookStoreDAO).GetProperty(item.Key).GetValue(entity),
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

      SqlParameter returnValue = new SqlParameter()
      {
        Direction = ParameterDirection.ReturnValue
      };
      parameters.Add(returnValue);

      SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.StoredProcedure, EntityName + nameof(Update), parameters.ToArray());

      return int.Parse(returnValue.Value.ToString());
    }

    public int Create(BookStoreDAO entity) => throw new NotImplementedException();
    public int Create(BookStoreDAO entity, int CreatedBy) => throw new NotImplementedException();
    public int Delete(int ID, BookStoreDAO entity) => throw new NotImplementedException();
    public int Delete(BookStoreDAO entity) => throw new NotImplementedException();
    public int Delete(int ID, int DeletedBy) => throw new NotImplementedException();
    public BookStoreDAO Read(int ID) => throw new NotImplementedException();
  }
}
