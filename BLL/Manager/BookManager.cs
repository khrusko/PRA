using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using BLL.Abstract.Helper;
using BLL.Abstract.Manager.Projection;
using BLL.Factory;
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
        CreateDate = model.CreateDate,
        IsAvailable = model.DeleteDate != DateTime.MinValue,
        ISBN = model.ISBN,
        Title = model.Title,
        Summary = model.Summary,
        Description = model.Description,
        IsNew = model.IsNew,
        PublisherFK = model.PublisherFK,
        AuthorFK = model.AuthorFK,
        PageCount = model.PageCount,
        PurchasePrice = model.PurchasePrice,
        LoanPrice = model.LoanPrice,
        Quantity = model.Quantity,
        ImagePath = model.ImagePath
      };

    public BookModel Model(BookProjection projection)
      => new BookModel
      {
        ID = projection.ID,
        ISBN = projection.ISBN,
        Title = projection.Title,
        Summary = projection.Summary,
        Description = projection.Description,
        IsNew = projection.IsNew,
        PublisherFK = projection.PublisherFK,
        AuthorFK = projection.AuthorFK,
        PageCount = projection.PageCount,
        PurchasePrice = projection.PurchasePrice,
        LoanPrice = projection.LoanPrice,
        Quantity = projection.Quantity,
        ImagePath = projection.ImagePath
      };

    public BookProjection GetByID(Int32 ID, Boolean availabilityCheck = true)
    {
      BookModel model = availabilityCheck
        ? (Repository as IBookRepository).ReadByIDAvailable(ID)
        : (Repository as IBookRepository).ReadByID(ID);
      return model is null ? null : Project(model);
    }

    public IEnumerable<BookProjection> GetAll(Boolean availabilityCheck = true)
      => availabilityCheck
      ? (Repository as IBookRepository).ReadAllAvailable().Select(Project)
      : (Repository as IBookRepository).ReadAll().Select(Project);

    public Int32 Remove(Int32 ID, Int32 deletedBy)
      => (Repository as IBookRepository).Delete(ID, deletedBy);

    public IEnumerable<BookProjection> GetBooksByAuthorFK(Int32 authorFK)
      => (Repository as IBookRepository).ReadByAuthorFK(authorFK).Select(Project);

    public Int32 Create(BookProjection projection, HttpPostedFileBase file, Int32 createdBy)
    {
      if (!(file is null))
      {
        IImageSaver imageSaver = ImageSaverFactory.GetImageSaver();
        imageSaver.File = file;
        imageSaver.SaveImageAs();

        projection.ImagePath = imageSaver.RelativePath;
      }

      return (Repository as IBookRepository).Create(Model(projection), createdBy);
    }

    public Int32 Update(BookProjection projection, HttpPostedFileBase file, Int32 updatedBy)
    {
      if (!(file is null))
      {
        IImageSaver imageSaver = ImageSaverFactory.GetImageSaver();
        imageSaver.File = file;
        imageSaver.SaveImageAs();

        projection.ImagePath = imageSaver.RelativePath;
      }
      else
      {
        projection.ImagePath = GetByID(projection.ID).ImagePath;
      }

      return (Repository as IBookRepository).Update(projection.ID, Model(projection), updatedBy);
    }
  }
}
