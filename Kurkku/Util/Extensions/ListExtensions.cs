using System.Linq;

namespace System.Collections.Generic
{
    public static class List
    {
        public static List<T> Create<T>(params T[] values)
        {
            return new List<T>(values);
        }
    }
}
