using System.Collections.Generic;
using System.Linq;

namespace RadioStationsSetCovering
{
    public static class SetCovering
    {
        public static IEnumerable<HashSet<T>> Run<T>(Dictionary<T, HashSet<T>> set, HashSet<T> universe)
        {
            return Enumerable.Empty<HashSet<T>>();
        }
    }
}