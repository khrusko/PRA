using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using UI.Models;
using UI.Models.Enums;

namespace UI
{
  public static class Extensions
  {
    public static IEnumerable<T> SortBy<T, K>(this IEnumerable<T> enumerable,
                                                        Func<T, K> keySelector,
                                                        SortDirection sortDirection)
      => sortDirection == SortDirection.ASCENDING ? enumerable.OrderBy(keySelector) : enumerable.OrderByDescending(keySelector);
  }
}