using System;

namespace DAL.Abstract.Model
{
  public interface IManageable<K>
  {
    DateTime? CreateDate { get; set; }
    K CreatedBy { get; set; }
    DateTime? UpdateDate { get; set; }
    K UpdatedBy { get; set; }
    DateTime? DeleteDate { get; set; }
    K DeletedBy { get; set; }
  }
}
