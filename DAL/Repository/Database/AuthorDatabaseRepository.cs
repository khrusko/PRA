using DAL.Abstract.Repository.Database;
using DAL.Abstract.Repository.Model;
using DAL.Model;
using System.Collections.Generic;
using System.Data;

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
  }
}
