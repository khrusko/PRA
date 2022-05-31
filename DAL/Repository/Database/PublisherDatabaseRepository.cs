using DAL.Abstract.Repository.Model;
using DAL.Abstract.Repository.Database;
using DAL.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository.Database
{
  internal class PublisherDatabaseRepository : AbstractDatabaseRepository<PublisherModel, int>, IPublisherRepository
  {
    public override string EntityName => "Publisher";
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
        { "Name",           SqlDbType.NVarChar },
      };
  }
}
