using System;
using System.IO;
using System.Linq;
using System.Web;

using BLL.Abstract.Helper;

namespace BLL.Helper
{
  public class ImageSaver : IImageSaver
  {
    private HttpPostedFileBase _file;
    public HttpPostedFileBase File
    {
      get => _file;
      set
      {
        if (!IsAllowedExtension(Path.GetExtension(value.FileName)))
          throw new ArgumentException("Ekstenzija datoteke nije valjana");

        if (value.ContentLength >= 4 * 1024 * 1024)
          throw new ArgumentException("Veličina datoteke premašuje 4MB");

        _file = value;
        RelativePath = GetRelativeImagePath();
        AbsolutePath = GetAbsoluteImagePath();
      }
    }
    public String DirPath { get; } = AppSettingsHelper.FILE_IMAGE_DIR_PATH;
    public String[] AllowedExtensions { get; set; } = AppSettingsHelper.FILE_IMAGE_ALLOWED_EXTENSIONS;
    public String RelativePath { get; set; }
    public String AbsolutePath { get; set; }

    public Boolean IsAllowedExtension(String extension) => AllowedExtensions.Contains(extension);

    public void SaveImageAs()
    {
      CreateDirectory();
      _file.SaveAs(AbsolutePath);
    }

    private String GetRelativeImagePath() => $"{DirPath}/{Guid.NewGuid()}.{Path.GetExtension(_file.FileName)}";
    private String GetAbsoluteImagePath() => HttpContext.Current.Server.MapPath(RelativePath);
    private String MapServerDirPath() => HttpContext.Current.Server.MapPath($"{DirPath}");
    private void CreateDirectory() => _ = Directory.CreateDirectory(MapServerDirPath());
  }
}
