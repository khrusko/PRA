using DAL.Model;
using System.Collections.Generic;

namespace DAL.Abstract.Repository.Model
{
  public interface IAuthorRepository : IModelRepository<AuthorModel, int>
  {
    IEnumerable<AuthorModel> ReadByBookFK(int BookFK);
  }
}
