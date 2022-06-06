using BLL.Abstract.Projection;
using System;

namespace BLL.Projection
{
  public class UserProjection : AbstractProjection<int>
  {
    public string UserID { get; set; }
    public string FName { get; set; }
    public string LName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string ImagePath { get; set; }
    public string Address { get; set; }
    public bool IsAdmin { get; set; }
    public Guid ConfirmationGUID { get; set; }
  }
}
