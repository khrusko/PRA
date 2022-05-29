using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Abstract.DAO
{
  public interface IDAO<K> : IIdentifiable<K>, IManageable<K>
  {
  }
}
