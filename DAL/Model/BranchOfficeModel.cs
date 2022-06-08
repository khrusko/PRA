using System;

using DAL.Abstract.Model;

namespace DAL.Model
{
  public class BranchOfficeModel : AbstractModel<Int32>
  {
    public Int32 BookStoreFK { get; set; }
    public String Name { get; set; }
    public String Address { get; set; }
    public String Telephone { get; set; }
    public String Email { get; set; }
  }
}
