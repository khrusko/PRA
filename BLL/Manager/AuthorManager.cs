using BLL.Abstract.Manager.Projection;
using BLL.Projection;
using DAL.Abstract.Repository;
using DAL.Abstract.Repository.Model;
using DAL.Factory;
using DAL.Model;
using System.Collections.Generic;
using System.Linq;

namespace BLL.Manager
{
  public class AuthorManager : IAuthorManager
  {
    public IRepository<AuthorModel> Repository { get; } = AuthorRepositoryFactory.GetRepository();

    public AuthorProjection Project(AuthorModel model)
      => new AuthorProjection
      {
        ID = model.ID,
        FName = model.FName,
        LName = model.LName,
        BirthDate = model.BirthDate,
        ImagePath = model.ImagePath,
        Biography = model.Biography
      };

    public IEnumerable<AuthorProjection> GetAuthorsByBookFK(int BookFK)
      => (Repository as IAuthorRepository).ReadByBookFK(BookFK).Select(Project);

    public IEnumerable<AuthorProjection> GetAll()
      => (Repository as IAuthorRepository).Read().Select(Project);
  }
}
