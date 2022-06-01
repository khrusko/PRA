using BLL.Abstract.Projection;

namespace BLL.Projection
{
  public class BranchOfficeProjection : AbstractProjection<int>
  {
    public string Name { get; set; }
    public string Address { get; set; }
    public string Telephone { get; set; }
    public string Email { get; set; }
  }
}
