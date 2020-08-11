#region

using System.Collections.Generic;
using NUnit.Framework;

#endregion

namespace GoogleProblem28
{
    public class Solution
    {
        public int Solve(int startLocation, int[][] holidayByLocation, int[][] flightDurations)
        {
            var graph = new Graph();

            // SS: for each location, add a vertex to the graph
            for (var i = 0; i < flightDurations.Length; i++)
            {
                graph.AddVertex(i);
            }

            for (var i = 0; i < flightDurations.Length; i++)
            {
                var destinations = flightDurations[i];

                for (var j = 0; j < destinations.Length; j++)
                {
                    var flightDuration = destinations[j];
                    if (flightDuration <= 6)
                    {
                        graph.AddUndirectedEdge(i, j);
                    }
                }
            }

            var itinarary = MaximizeNationalHolidays(graph, startLocation, 0, new List<int> {startLocation}, holidayByLocation);
            return itinarary;
        }

        private int MaximizeNationalHolidays(Graph graph, int currentLocation, int month, List<int> path, int[][] holidayByLocation)
        {
            // SS: divide and conquer approach

            if (month == 12)
            {
                // done
                return 0;
            }

            // SS: number of holidays for this month at the current location
            var n = holidayByLocation[currentLocation][month];

            var bestLocationHolidays = n;
            var bestLocation = currentLocation;

            // SS: find all locations that we can reach from this one (this includes the current one)
            var targetLocations = graph.AdjacencyList[currentLocation];
            for (var i = 0; i < targetLocations.Count; i++)
            {
                var targetLocation = targetLocations[i];
                var c = MaximizeNationalHolidays(graph, targetLocation, month + 1, path, holidayByLocation);
                if (c + n > bestLocationHolidays)
                {
                    bestLocationHolidays = c + n;
                    bestLocation = targetLocation;
                }
            }

//            path.Add(bestLocation);

            return bestLocationHolidays;
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
                    new[]{0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}
                    , new[]{0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0}
                    , new[]{0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1}
                    , new[]{0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}
                    , new[]{0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0}
                };

                // SS: flight durations don't have to be symmetric between locations...
                int[][] flightDuration = new[]
                {
                    new[]{0, 2, 7, 7, 7}
                    , new[]{2, 0, 2, 3, 7}
                    , new[]{7, 2, 0, 2, 7}
                    , new[]{7, 2, 3, 0, 3}
                    , new[]{7, 7, 7, 2, 0}
                };

                // Act
                var itinerary = new Solution().Solve(0, holidaysByLocation, flightDuration);

                // Assert
            }
        }
    }
}