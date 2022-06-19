using System;
using System.Web;

using BLL.Projection;

using DAL.Model;

namespace BLL.Abstract.Manager.Projection
{
  public interface IAuthorManager : IProjectionManager<AuthorModel, AuthorProjection, Int32>
  {
    Int32 Update(AuthorProjection projection, HttpPostedFileBase file, Int32 updatedBy);
    Int32 Create(AuthorProjection projection, HttpPostedFileBase file, Int32 createdBy);
  }
}
