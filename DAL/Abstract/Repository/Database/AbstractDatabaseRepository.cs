using DAL.Abstract.Model;
using DAL.Abstract.Repository.Model;
using DAL.Factory;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace DAL.Abstract.Repository.Database
{
  internal abstract class AbstractDatabaseRepository<T, K> : IDatabaseRepository<T, K>, IModelRepository<T, K>
    where T : class, IModel<K>
  {
    public string ConnectionString => ConnectionStringFactory.GetConnectionString();
    public abstract string EntityName { get; }
    public abstract IDictionary<string, SqlDbType> DbKeyTypePairs { get; }

    public virtual int Create(T entity) => Create(entity, entity.CreatedBy);
    public virtual int Create(T entity, K CreatedBy)
    {
      IList<SqlParameter> parameters = new List<SqlParameter>();
      string[] unusedProperties = typeof(IModel<K>).GetProperties().Select(x => x.Name).ToArray();

      foreach (var item in DbKeyTypePairs)
      {
        if (unusedProperties.Contains(item.Key)) continue;
        parameters.Add(new SqlParameter()
        {
          ParameterName = "@" + item.Key,
          Direction = ParameterDirection.Input,
          SqlDbType = item.Value,
          Value = typeof(T).GetProperty(item.Key).GetValue(entity),
        });
      }

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

    public virtual int Delete(T entity) => Delete(entity.ID, entity);
    public virtual int Delete(K ID, T entity) => Delete(ID, entity.DeletedBy);
    public virtual int Delete(K ID, K DeletedBy)
    {
      IList<SqlParameter> parameters = new List<SqlParameter>();
      string[] unusedProperties = typeof(IModel<K>).GetProperties().Select(x => x.Name).ToArray();

      parameters.Add(new SqlParameter()
      {
        ParameterName = "@ID",
        Direction = ParameterDirection.Input,
        SqlDbType = DbKeyTypePairs["ID"],
        Value = ID,
      });

      parameters.Add(new SqlParameter()
      {
        ParameterName = "@DeletedBy",
        Direction = ParameterDirection.Input,
        SqlDbType = DbKeyTypePairs["DeletedBy"],
        Value = DeletedBy,
      });

      SqlParameter returnValue = new SqlParameter()
      {
        Direction = ParameterDirection.ReturnValue
      };
      parameters.Add(returnValue);

      SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.StoredProcedure, EntityName + nameof(Delete), parameters.ToArray());

      return int.Parse(returnValue.Value.ToString());
    }

    public virtual T Read(K ID)
    {
      IList<SqlParameter> parameters = new List<SqlParameter>();

      parameters.Add(new SqlParameter()
      {
        ParameterName = "@ID",
        Direction = ParameterDirection.Input,
        SqlDbType = DbKeyTypePairs["ID"],
        Value = ID,
      });

      SqlDataReader reader = SqlHelper.ExecuteReader(ConnectionString, CommandType.StoredProcedure, EntityName + nameof(Read), parameters.ToArray());

      return reader.Read()
        ? DbKeyTypePairs.Keys.Aggregate(Activator.CreateInstance<T>(), (obj, prop) =>
        {
          typeof(T).GetProperty(prop).SetValue(obj, reader[prop] == DBNull.Value ? default : reader[prop]);
          return obj;
        })
        : default(T);
    }

    public virtual IEnumerable<T> Read()
    {
      IList<SqlParameter> parameters = new List<SqlParameter>();

      parameters.Add(new SqlParameter()
      {
        ParameterName = "@ID",
        Direction = ParameterDirection.Input,
        SqlDbType = DbKeyTypePairs["ID"],
        Value = DBNull.Value,
      });

      SqlDataReader reader = SqlHelper.ExecuteReader(ConnectionString, CommandType.StoredProcedure, EntityName + nameof(Read), parameters.ToArray());

      while (reader.Read())
      {
        yield return DbKeyTypePairs.Keys.Aggregate(Activator.CreateInstance<T>(), (obj, prop) =>
        {
          typeof(T).GetProperty(prop).SetValue(obj, reader[prop] == DBNull.Value ? default : reader[prop]);
          return obj;
        });
      }
    }

    public virtual int Update(T entity) => Update(entity.ID, entity);
    public virtual int Update(K ID, T entity) => Update(ID, entity, entity.UpdatedBy);
    public virtual int Update(K ID, T entity, K UpdatedBy)
    {
      IList<SqlParameter> parameters = new List<SqlParameter>();
      string[] unusedProperties = typeof(IModel<K>).GetProperties().Select(x => x.Name).ToArray();

      foreach (var item in DbKeyTypePairs)
      {
        if (unusedProperties.Contains(item.Key)) continue;
        parameters.Add(new SqlParameter()
        {
          ParameterName = "@" + item.Key,
          Direction = ParameterDirection.Input,
          SqlDbType = item.Value,
          Value = typeof(T).GetProperty(item.Key).GetValue(entity),
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
  }
}
