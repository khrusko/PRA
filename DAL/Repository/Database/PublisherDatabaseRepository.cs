using System;
using System.Collections.Generic;
using System.Data;

using DAL.Abstract.Repository.Database;
using DAL.Abstract.Repository.Model;
using DAL.Model;

namespace DAL.Repository.Database
{
  internal class PublisherDatabaseRepository : AbstractDatabaseRepository<PublisherModel, Int32>, IPublisherRepository
  {
    public override String EntityName => "Publisher";
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
        { "Name",           SqlDbType.NVarChar },
      };
  }
}
