using System;

using BLL.Abstract.Projection;

namespace BLL.Projection
{
  [Serializable]
  public class UserProjection : AbstractProjection<Int32>
  {
    public String UserID { get; set; }
    public String FName { get; set; }
    public String LName { get; set; }
    public String Email { get; set; }
    public String Password { get; set; }
    public String ImagePath { get; set; }
    public String Address { get; set; }
    public Boolean IsAdmin { get; set; }
    public Guid GUID { get; set; }
  }
}
