using BLL.Projection;

namespace UI.Models
{
  public class BookPublisherVM
  {
    public BookProjection Book { get; set; }
    public PublisherProjection Publisher { get; set; }
  }
}