using System;

using DAL.Abstract.Model;

namespace DAL.Model
{
  public class PublisherModel : AbstractModel<Int32>
  {
    public String Name { get; set; }
  }
}
