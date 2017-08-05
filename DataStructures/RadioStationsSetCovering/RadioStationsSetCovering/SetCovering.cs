using System;
using System.Collections.Generic;
using System.Linq;
using Djikstra;

namespace RadioStationsSetCovering
{
    public static class SetCovering
    {
        /// <summary>
        /// The set cover problem is to identify the smallest sub-collection of
        /// S whose union equals the universe.
        /// </summary>
        public static IEnumerable<List<T>> Run<T>(IDictionary<int, HashSet<T>> set, HashSet<T> universe)
        {
            return Enumerable.Empty<List<T>>();
        }

        /// <summary>
        /// The power set, i.e. the combinations of a set with all other sets,
        /// is stored by radio station index, rather than doing the unions
        /// explicitly.
        /// </summary>
        public static List<HashSet<int>> CreatePowerSet<T>(IDictionary<int, HashSet<T>> set)
        {
            int nPowerSet = (int) Math.Pow(2, set.Count());
            var powerSet = new List<HashSet<int>>(nPowerSet);

            // add the empty set, so each set is added on its own
            powerSet.Add(new HashSet<int>());

            set.ForEach(item =>
            {
                int id = item.Key;

                var c = new List<HashSet<int>>();

                // combine item with every other element in the list so far
                powerSet.ForEach(pi =>
                {
                    // combine the two sets items and pi
                    var combined = new HashSet<int>();
                    combined.Union(pi);
                    combined.Add(id);
                    c.Add(combined);
                });
            });

            return powerSet;
        }
    }
}