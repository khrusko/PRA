using System;

using DAL.Abstract.Model;

namespace DAL.Model
{
  public class UserModel : AbstractModel<Int32>
  {
    public String UserID { get; set; }
    public String FName { get; set; }
    public String LName { get; set; }
    public String Email { get; set; }
    public String PasswordHash { get; set; }
    public String ImagePath { get; set; }
    public String Address { get; set; }
    public Boolean IsAdmin { get; set; }
    public Guid GUID { get; set; }
    public Boolean RegistrationIsApproved { get; set; }
    public DateTime RegistrationDate { get; set; }
    public Boolean ResetPasswordIsApproved { get; set; }
    public DateTime ResetPasswordDate { get; set; }
  }
}
