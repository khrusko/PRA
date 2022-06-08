using System;
using System.Collections.Generic;
using System.Linq;

using BLL.Abstract.Manager.Projection;
using BLL.Projection;

using DAL.Abstract.Repository;
using DAL.Abstract.Repository.Model;
using DAL.Factory;
using DAL.Model;

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

    public AuthorProjection GetByID(Int32 ID)
    {
      AuthorModel model = (Repository as IAuthorRepository).Read(ID);
      return model is null ? null : Project(model);
    }

    public IEnumerable<AuthorProjection> GetAll()
      => (Repository as IAuthorRepository).Read().Select(Project);

    public IEnumerable<AuthorProjection> GetAuthorsByBookFK(Int32 BookFK)
      => (Repository as IAuthorRepository).ReadByBookFK(BookFK).Select(Project);
  }
}
