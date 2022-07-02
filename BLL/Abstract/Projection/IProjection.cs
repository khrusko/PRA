using System;

using DAL.Abstract.Model;

namespace BLL.Abstract.Projection
{
  public interface IProjection<K> : IIdentifiable<K>
  {
    Boolean IsAvailable { get; set; }
  }
}
