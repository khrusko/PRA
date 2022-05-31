using DAL.Abstract.Model;

namespace DAL.Model
{
  public class BranchOfficeModel : AbstractModel<int>
  {
    public int BookStoreFK { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public string Telephone { get; set; }
    public string Email { get; set; }
  }
}
