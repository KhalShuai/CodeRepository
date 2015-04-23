using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.Extensions
{
    public static partial class EnumerableExtensions
    {
        public static IEnumerable<IEnumerable<T>> Paging<T>(this IEnumerable<T> collection, int pageSize)
        {
            var list = new List<T>(collection);
            var pages = (int)Math.Ceiling((decimal)list.Count / pageSize);

            for (int pageIndex = 0; pageIndex < pages; pageIndex++)
            {
                yield return list.Skip(pageIndex * pageSize).Take(pageSize);
            }
        }
    }
}
