using System;
using System.Collections.Generic;
using System.Linq;

namespace Kurkku.Util.Extensions
{
    public static class EnumerableExtension
    {
        public static T PickRandom<T>(this IEnumerable<T> source)
        {
            return source.PickRandom(1).Single();
        }

        public static IEnumerable<T> PickRandom<T>(this IEnumerable<T> source, int count)
        {
            return source.Shuffle().Take(count);
        }

        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source)
        {
            return source.OrderBy(x => Guid.NewGuid());
        }

        public static List<T> GetPage<T>(this IEnumerable<T> list, int page, int pageSize)
        {
            return list.Skip(page * pageSize).Take(pageSize).ToList();
        }

        public static int CountPages<T>(this IEnumerable<T> list, int pageSize)
        {
            return ((list.Count() - 1) / pageSize) + 1;
        }
    }
}
