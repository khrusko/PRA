using BLL.Projection;
using DAL.Model;
using System.Collections.Generic;

namespace BLL.Abstract.Manager.Projection
{
  public interface IAuthorManager : IProjectionManager<AuthorModel, AuthorProjection, int>
  {
    IEnumerable<AuthorProjection> GetAuthorsByBookFK(int BookFK);
  }
}
