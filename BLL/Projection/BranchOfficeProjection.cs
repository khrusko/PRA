using System;

using BLL.Abstract.Projection;

namespace BLL.Projection
{
  public class BranchOfficeProjection : AbstractProjection<Int32>
  {
    public String Name { get; set; }
    public String Address { get; set; }
    public String Telephone { get; set; }
    public String Email { get; set; }
  }
}
