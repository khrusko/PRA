using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

using DAL.Abstract.Model;
using DAL.Abstract.Repository.Model;
using DAL.Method;
using DAL.Factory;

using Microsoft.ApplicationBlocks.Data;
using System.Reflection;

namespace DAL.Abstract.Repository.Database
{
  internal abstract class AbstractDatabaseRepository<T, K> : IDatabaseRepository<T, K>, IModelRepository<T, K>
    where T : class, IModel<K>
  {
    private static readonly String CREATE_PROCEDURE_NAME = "Create";
    private static readonly String READ_PROCEDURE_NAME   = "Read";
    private static readonly String UPDATE_PROCEDURE_NAME = "Update";
    private static readonly String DELETE_PROCEDURE_NAME = "Delete";
    private static readonly String[] UNUSED_PROPERTIES = { "ID", "CreatedBy", "UpdatedBy", "DeletedBy", "CreateDate", "UpdateDate", "DeleteDate" };

    public String ConnectionString => ConnectionStringFactory.GetConnectionString();
    public abstract String EntityName { get; }
    public abstract IDictionary<String, SqlDbType> DbKeyTypePairs { get; }

    public virtual K Create(T entity) => Create(entity, entity.CreatedBy);
    public virtual K Create(T entity, K CreatedBy)
    {
      IList<SqlParameter> parameters = new List<SqlParameter>();

      foreach (KeyValuePair<String, SqlDbType> item in DbKeyTypePairs)
      {
        if (UNUSED_PROPERTIES.Contains(item.Key)) continue;
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

      var returnValue = new SqlParameter()
      {
        Direction = ParameterDirection.ReturnValue
      };
      parameters.Add(returnValue);

      _ = SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.StoredProcedure, EntityName + CREATE_PROCEDURE_NAME, parameters.ToArray());

      Type targetType = typeof(K);
      Type conversionType = Nullable.GetUnderlyingType(targetType) ?? targetType;
      return (K)Convert.ChangeType(returnValue.Value, conversionType);
    }

    public virtual Int32 Delete(T entity) => Delete(entity.ID, entity);
    public virtual Int32 Delete(K ID, T entity) => Delete(ID, entity.DeletedBy);
    public virtual Int32 Delete(K ID, K DeletedBy)
    {
      IList<SqlParameter> parameters = new List<SqlParameter>();

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

      var returnValue = new SqlParameter()
      {
        Direction = ParameterDirection.ReturnValue
      };
      parameters.Add(returnValue);

      _ = SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.StoredProcedure, EntityName + DELETE_PROCEDURE_NAME, parameters.ToArray());

      return Int32.Parse(returnValue.Value.ToString());
    }

    public virtual T ReadByID(K ID)
    {
      IList<SqlParameter> parameters = new List<SqlParameter>
      {
        new SqlParameter
        {
          ParameterName = "@Method",
          Direction = ParameterDirection.Input,
          SqlDbType = SqlDbType.Int,
          Value = Convert.ToInt32(ReadMethod.BY_ID),
        },
        new SqlParameter
        {
          ParameterName = "@ID",
          Direction = ParameterDirection.Input,
          SqlDbType = DbKeyTypePairs["ID"],
          Value = ID
        }
      };

      SqlDataReader reader = SqlHelper.ExecuteReader(ConnectionString, CommandType.StoredProcedure, EntityName + READ_PROCEDURE_NAME, parameters.ToArray());

      return reader.Read()
        ? DbKeyTypePairs.Keys.Aggregate(Activator.CreateInstance<T>(), (obj, prop) =>
        {
          typeof(T).GetProperty(prop).SetValue(obj, reader[prop] == DBNull.Value ? default : reader[prop]);
          return obj;
        })
        : default;
    }

    public virtual T ReadByIDAvailable(K ID)
    {
      IList<SqlParameter> parameters = new List<SqlParameter>
      {
        new SqlParameter
        {
          ParameterName = "@Method",
          Direction = ParameterDirection.Input,
          SqlDbType = SqlDbType.Int,
          Value = Convert.ToInt32(ReadMethod.BY_ID_AVAILABLE),
        },
        new SqlParameter
        {
          ParameterName = "@ID",
          Direction = ParameterDirection.Input,
          SqlDbType = DbKeyTypePairs["ID"],
          Value = ID,
        }
      };

      SqlDataReader reader = SqlHelper.ExecuteReader(ConnectionString, CommandType.StoredProcedure, EntityName + READ_PROCEDURE_NAME, parameters.ToArray());

      return reader.Read()
        ? DbKeyTypePairs.Keys.Aggregate(Activator.CreateInstance<T>(), (obj, prop) =>
        {
          typeof(T).GetProperty(prop).SetValue(obj, reader[prop] == DBNull.Value ? default : reader[prop]);
          return obj;
        })
        : default;
    }

    public virtual IEnumerable<T> ReadAll()
    {
      IList<SqlParameter> parameters = new List<SqlParameter>
      {
        new SqlParameter
        {
          ParameterName = "@Method",
          Direction = ParameterDirection.Input,
          SqlDbType = SqlDbType.Int,
          Value = Convert.ToInt32(ReadMethod.ALL),
        },
        new SqlParameter
        {
          ParameterName = "@ID",
          Direction = ParameterDirection.Input,
          SqlDbType = DbKeyTypePairs["ID"],
          Value = DBNull.Value,
        }
      };

      SqlDataReader reader = SqlHelper.ExecuteReader(ConnectionString, CommandType.StoredProcedure, EntityName + READ_PROCEDURE_NAME, parameters.ToArray());

      while (reader.Read())
      {
        yield return DbKeyTypePairs.Keys.Aggregate(Activator.CreateInstance<T>(), (obj, prop) =>
        {
          typeof(T).GetProperty(prop).SetValue(obj, reader[prop] == DBNull.Value ? default : reader[prop]);
          return obj;
        });
      }
    }

    public virtual IEnumerable<T> ReadAllAvailable()
    {
      IList<SqlParameter> parameters = new List<SqlParameter>
      {
        new SqlParameter
        {
          ParameterName = "@Method",
          Direction = ParameterDirection.Input,
          SqlDbType = SqlDbType.Int,
          Value = Convert.ToInt32(ReadMethod.ALL_AVAILABLE),
        },
        new SqlParameter
        {
          ParameterName = "@ID",
          Direction = ParameterDirection.Input,
          SqlDbType = DbKeyTypePairs["ID"],
          Value = DBNull.Value,
        }
      };

      SqlDataReader reader = SqlHelper.ExecuteReader(ConnectionString, CommandType.StoredProcedure, EntityName + READ_PROCEDURE_NAME, parameters.ToArray());

      while (reader.Read())
      {
        yield return DbKeyTypePairs.Keys.Aggregate(Activator.CreateInstance<T>(), (obj, prop) =>
        {
          typeof(T).GetProperty(prop).SetValue(obj, reader[prop] == DBNull.Value ? default : reader[prop]);
          return obj;
        });
      }
    }

    public virtual Int32 Update(T entity) => Update(entity.ID, entity);
    public virtual Int32 Update(K ID, T entity) => Update(ID, entity, entity.UpdatedBy);
    public virtual Int32 Update(K ID, T entity, K UpdatedBy)
    {
      IList<SqlParameter> parameters = new List<SqlParameter>();

      foreach (KeyValuePair<String, SqlDbType> item in DbKeyTypePairs)
      {
        if (UNUSED_PROPERTIES.Contains(item.Key)) continue;
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

      var returnValue = new SqlParameter()
      {
        Direction = ParameterDirection.ReturnValue
      };
      parameters.Add(returnValue);

      _ = SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.StoredProcedure, EntityName + UPDATE_PROCEDURE_NAME, parameters.ToArray());

      return Int32.Parse(returnValue.Value.ToString());
    }
  }
}
