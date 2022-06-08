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
  public class BookManager : IBookManager
  {
    public IRepository<BookModel> Repository { get; } = BookRepositoryFactory.GetRepository();

    public BookProjection Project(BookModel model)
      => new BookProjection
      {
        ID = model.ID,
        ISBN = model.ISBN,
        Title = model.Title,
        Summary = model.Summary,
        Description = model.Description,
        IsNew = model.IsNew,
        PublisherFK = model.PublisherFK,
        PageCount = model.PageCount,
        PurchasePrice = model.PurchasePrice,
        LoanPrice = model.LoanPrice,
        Quantity = model.Quantity,
        ImagePath = model.ImagePath,
      };

    public BookProjection GetByID(Int32 ID)
    {
      BookModel model = (Repository as IBookRepository).Read(ID);
      return model is null ? null : Project(model);
    }

    public IEnumerable<BookProjection> GetAll()
      => (Repository as IBookRepository).Read().Select(Project);

    public IEnumerable<BookProjection> GetBooksByAuthorFK(Int32 AuthorFK)
      => (Repository as IBookRepository).ReadByAuthorFK(AuthorFK).Select(Project);
  }
}
