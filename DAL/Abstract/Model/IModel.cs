using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Abstract.Model
{
  public interface IModel<K> : IIdentifiable<K>, IManageable<K>
  {
  }
}
