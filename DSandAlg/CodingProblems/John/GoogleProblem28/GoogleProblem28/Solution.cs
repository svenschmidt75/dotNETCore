#region

using System.Collections.Generic;
using NUnit.Framework;

#endregion

namespace GoogleProblem28
{
    public class Solution
    {
        public int Solve(int startLocation, int[][] holidayByLocation, int[][] flightDuration)
        {
            var graph = new Graph();

            // SS: for each location, add a vertex to the graph
            for (var i = 0; i < flightDuration.Length; i++)
            {
                graph.AddVertex(i);
            }

            for (var i = 0; i < flightDuration.Length; i++)
            {
                var destinations = flightDuration[i];

                for (var j = 0; j < destinations.Length; j++)
                {
                    var flightDur = destinations[j];

                    if (flightDur <= 6)
                    {
                        graph.AddUndirectedEdge(i, j);
                    }
                }
            }

            var itinarary = MaximizeNationalHolidays(graph, startLocation, 0, new List<int> {startLocation}, 0
                , holidayByLocation);

            return itinarary;
        }

        private int MaximizeNationalHolidays(Graph graph, int currentLocation, int month, List<int> path, int nHolidays
            , int[][] holidayByLocation)
        {
            // SS: divide and conquer approach

            if (month == 12)
            {
                // done
                return 0;
            }

            // SS: number of holidays for this month at the current location
            var n = holidayByLocation[currentLocation][month];

            var bestLocationHolidays = -1;
            var bestLocation = -1;

            // SS: find all locations that we can reach from this one (this includes the current one)
            var targetLocations = graph.AdjacencyList[currentLocation];
            for (var i = 0; i < targetLocations.Count; i++)
            {
                var targetLocation = targetLocations[i];
                var c = MaximizeNationalHolidays(graph, targetLocation, month + 1, path, nHolidays, holidayByLocation);
                if (bestLocation == -1 || c > bestLocation)
                {
                    bestLocationHolidays = c;
                    bestLocation = targetLocation;
                }
            }

            if (bestLocation > -1)
            {
                path.Add(bestLocation);
                return n + bestLocationHolidays;
            }

            return nHolidays;
        }

        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1()
            {
                // Arrange
                var holidaysByLocation = new[]
                {
                    new[]{0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}
                    , new[]{0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}
                    , new[]{0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}
                    , new[]{0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}
                    , new[]{0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}
                };

                // SS: flight durations don't have to be symmetric between locations...
                int[][] flightDuration = new[]
                {
                    new[]{0, 2, 7, 9, 3}
                    , new[]{2, 0, 7, 9, 3}
                    , new[]{2, 7, 0, 9, 3}
                    , new[]{2, 7, 9, 0, 3}
                    , new[]{2, 9, 9, 3, 0}
                };

                // Act
                var itinerary = new Solution().Solve(0, holidaysByLocation, flightDuration);

                // Assert
            }
        }
    }
}