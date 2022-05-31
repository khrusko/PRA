using DAL.Abstract.Model;

namespace DAL.Model
{
  public class PublisherModel : AbstractModel<int>
  {
    public string Name { get; set; }
  }
}
