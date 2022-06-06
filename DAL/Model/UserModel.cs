using DAL.Abstract.Model;
using System;

namespace DAL.Model
{
  public class UserModel : AbstractModel<int>
  {
    public string UserID { get; set; }
    public string FName { get; set; }
    public string LName { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public string ImagePath { get; set; }
    public string Address { get; set; }
    public bool IsAdmin { get; set; }
    public Guid GUID { get; set; }
    public bool RegistrationIsApproved { get; set; }
    public DateTime RegistrationDate { get; set; }
    public bool ResetPasswordIsApproved { get; set; }
    public DateTime ResetPasswordDate { get; set; }
  }
}
