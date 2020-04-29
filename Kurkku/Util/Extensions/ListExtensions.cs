using System.Linq;

namespace System.Collections.Generic
{
    public static class List
    {
        public static List<T> Create<T>(T value)
        {
            return new List<T>(1) { value };
        }
    }
}
