using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

using BLL.Abstract.Manager.Projection;
using BLL.Helper;
using BLL.Projection;

using DAL.Abstract.Repository;
using DAL.Abstract.Repository.Model;
using DAL.Factory;
using DAL.Model;

namespace BLL.Manager
{
  public class BookManager : IBookManager
  {
    private static readonly String _dirPath = $"{AppSettingsHelper.ImageDirPath}/Books/";
    private static readonly String[] _allowedExtensions = AppSettingsHelper.ImageAllowedExtensions;

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
        PageCount = projection.PageCount,
        PurchasePrice = projection.PurchasePrice,
        LoanPrice = projection.LoanPrice,
        Quantity = projection.Quantity,
        ImagePath = projection.ImagePath
      };

    public BookProjection GetByID(Int32 ID)
    {
      BookModel model = (Repository as IBookRepository).Read(ID);
      return model is null ? null : Project(model);
    }

    public IEnumerable<BookProjection> GetAll()
      => (Repository as IBookRepository).Read().Select(Project);

    public Int32 Remove(Int32 ID, Int32 DeletedBy)
      => (Repository as IBookRepository).Delete(ID, DeletedBy);

    public IEnumerable<BookProjection> GetBooksByAuthorFK(Int32 AuthorFK)
      => (Repository as IBookRepository).ReadByAuthorFK(AuthorFK).Select(Project);

    public Int32 Create(BookProjection projection, HttpPostedFileBase Image, IEnumerable<Int32> Authors, Int32 CreatedBy) 
    {
      if (!_allowedExtensions.Contains(Path.GetExtension(Image.FileName))) return 0;

      projection.ImagePath = SaveImageAs(Image, projection.ID);
      return (Repository as IBookRepository).Create(Model(projection), Authors, CreatedBy);
    }

    public Int32 Update(BookProjection projection, HttpPostedFileBase Image, IEnumerable<Int32> Authors, Int32 UpdatedBy)
    {
      if (!_allowedExtensions.Contains(Path.GetExtension(Image.FileName))) return 0;

      projection.ImagePath = SaveImageAs(Image, projection.ID);
      return (Repository as IBookRepository).Update(projection.ID, Model(projection), Authors, UpdatedBy);
    }

    private String GetImagePath(Int32 ID, String FileName)
    {
      String dirPath = $"{_dirPath}/{ID}";
      _ = Directory.CreateDirectory(dirPath);

      return $"{dirPath}/{Path.GetFileName(FileName)}.{Path.GetExtension(FileName)}";
    }

    private String SaveImageAs(HttpPostedFileBase Image, Int32 ID)
    {
      String imagePath = GetImagePath(ID, Image.FileName);
      Image.SaveAs(HttpContext.Current.Server.MapPath(imagePath));
      return imagePath;
    }
  }
}
