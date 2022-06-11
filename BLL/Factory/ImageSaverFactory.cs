using BLL.Abstract.Helper;
using BLL.Helper;

namespace BLL.Factory
{
  public static class ImageSaverFactory
  {
    public static IImageSaver GetImageSaver() => new ImageSaver();
  }
}
