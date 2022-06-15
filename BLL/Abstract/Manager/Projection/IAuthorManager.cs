using System;
using System.Collections.Generic;

using BLL.Projection;

using DAL.Model;

namespace BLL.Abstract.Manager.Projection
{
  public interface IAuthorManager : IProjectionManager<AuthorModel, AuthorProjection, Int32>
  {
  }
}
