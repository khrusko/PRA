using System;
using System.Web;

namespace BLL.Abstract.Helper
{
  public interface IImageSaver
  {
    HttpPostedFileBase File { get; set; }
    String DirPath { get; }
    String[] AllowedExtensions { get; }
    String RelativePath { get; }
    String AbsolutePath { get; }
    Boolean IsAllowedExtension(String extension);
    void SaveImageAs();
  }
}
