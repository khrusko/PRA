using DAL.Abstract.Repository.Database;
using DAL.Abstract.Repository.Model;
using DAL.Model;
using System.Collections.Generic;
using System.Data;

namespace DAL.Repository.Database
{
  internal class BranchOfficeDatabaseRepository : AbstractDatabaseRepository<BranchOfficeModel, int>, IBranchOfficeRepository
  {
    public override string EntityName => "BranchOffice";
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
        { "BookStoreFK",    SqlDbType.Int },
        { "Name",           SqlDbType.NVarChar },
        { "Address",        SqlDbType.NVarChar },
        { "Telephone",      SqlDbType.NVarChar },
        { "Email",          SqlDbType.NVarChar },
      };
  }
}
