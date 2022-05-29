using DAL.Abstract.DAO;

namespace DAL.DAO
{
  public class BranchOfficeDAO : AbstractDAO<int>
  {
    public int BookStoreFK { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public string Telephone { get; set; }
    public string Email { get; set; }
  }
}
