using System;
using System.Collections.Generic;
using System.Linq;
using Djikstra;

namespace RadioStationsSetCovering
{
    public static class SetCovering
    {
        public static HashSet<int> EmptySet => new HashSet<int>();

        /// <summary>
        ///     The set cover problem is to identify the smallest sub-collection of
        ///     S whose union equals the universe.
        /// </summary>
        public static HashSet<int> Run<T>(IDictionary<int, HashSet<T>> set, HashSet<T> universe)
        {
            var powerSet = CreatePowerSet(set);

            // map each set of radio stations to the union of the states they cover
            var comb = MapRadioStationsToStates(set, powerSet);

            // find the smallest set of radio stations, whose union of states equals
            // universe
            var solutions = comb.Where(pair =>
            {
                var states = pair.Value;
                return states.SetEquals(universe);
            }).OrderBy(pair => pair.Key.Count);

            // return the minimum set of radio stations whose union of states
            // equal universe
            return solutions.First().Key;
        }

        private static Dictionary<HashSet<int>, HashSet<T>> MapRadioStationsToStates<T>(IDictionary<int, HashSet<T>> set, List<HashSet<int>> powerSet)
        {
            var comb = new Dictionary<HashSet<int>, HashSet<T>>();
            powerSet.ForEach(radioStations =>
            {
                var hashSet = new HashSet<T>();
                comb[radioStations] = hashSet;
                radioStations.ForEach(radioStation =>
                {
                    var st = set[radioStation];
                    hashSet.UnionWith(st);
                });
            });
            return comb;
        }

        /// <summary>
        ///     The power set, i.e. the combinations of a set with all other sets,
        ///     is stored by radio station index, rather than doing the unions
        ///     explicitly.
        /// </summary>
        public static List<HashSet<int>> CreatePowerSet<T>(IDictionary<int, HashSet<T>> set)
        {
            var nPowerSet = (int) Math.Pow(2, set.Count());
            var powerSet = new List<HashSet<int>>(nPowerSet);

            // add the empty set, so each set is added on its own
            powerSet.Add(EmptySet);

            set.ForEach(item =>
            {
                var id = item.Key;

                var c = new List<HashSet<int>>();

                // combine item with every other element in the list so far
                powerSet.ForEach(pi =>
                {
                    // combine the two sets items and pi
                    var combined = new HashSet<int>();
                    combined.UnionWith(pi);
                    combined.Add(id);
                    c.Add(combined);
                });

                // add all combination with item to the power set
                powerSet.AddRange(c);
            });

            return powerSet;
        }
    }
}