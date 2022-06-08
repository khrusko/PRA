using System;
using System.Collections.Generic;

using DAL.Model;

namespace DAL.Abstract.Repository.Model
{
  public interface IAuthorRepository : IModelRepository<AuthorModel, Int32>
  {
    IEnumerable<AuthorModel> ReadByBookFK(Int32 BookFK);
  }
}
