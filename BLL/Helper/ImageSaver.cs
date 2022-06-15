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
    public HttpPostedFileBase File {
      get => _file;
      set
      {
        if (!IsAllowedExtension(Path.GetExtension(value.FileName)))
          throw new ArgumentException("Invalid file extension.");

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

    private String GetRelativeImagePath() => $"{DirPath}/{new Guid(Path.GetFileName(_file.FileName))}.{Path.GetExtension(_file.FileName)}";
    private String GetAbsoluteImagePath() => HttpContext.Current.Server.MapPath(RelativePath);
    private String MapServerDirPath() => HttpContext.Current.Server.MapPath($"{DirPath}");
    private void CreateDirectory() => _ = Directory.CreateDirectory(MapServerDirPath());
  }
}
