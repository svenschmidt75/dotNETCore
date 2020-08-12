#region

using System;
using NUnit.Framework;

#endregion

namespace GoogleProblem28
{
    public class Solution
    {
        public int SolveTopDownDP(int startLocation, int[][] holidayByLocation, int[][] flightDurations)
        {
            var graph = CreateGraph(flightDurations);

            // SS: memoization array
            // space complexity: O(#locations^2 * 12), i.e. each cell in the #locations * 12 grid stores
            // the path of size #locations
            var memArray = new Payload[holidayByLocation.Length][];
            for (var i = 0; i < memArray.Length; i++)
            {
                memArray[i] = new Payload[holidayByLocation[i].Length];
            }

            var itinarary = MaximizeNationalHolidaysTopDownDP(graph, startLocation, 0, holidayByLocation, memArray);
            return itinarary.Holidays;
        }

        private static Graph CreateGraph(int[][] flightDurations)
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
                        graph.AddDirectedEdge(i, j);
                    }
                }
            }

            return graph;
        }

        private int MaximizeNationalHolidays(Graph graph, int currentLocation, int month, int[][] holidayByLocation)
        {
            // SS: divide and conquer approach
            // runtime complexity: O(#locations^12), i.e. exponential complexity

            if (month == 12)
            {
                // done
                return 0;
            }

            // SS: number of holidays for this month at the current location
            var n = holidayByLocation[currentLocation][month];

            var bestLocationHolidays = 0;

            // SS: find all locations that we can reach from this one (this includes the current one)
            var targetLocations = graph.AdjacencyList[currentLocation];
            for (var i = 0; i < targetLocations.Count; i++)
            {
                var targetLocation = targetLocations[i];
                var c = MaximizeNationalHolidays(graph, targetLocation, month + 1, holidayByLocation);
                if (c >= bestLocationHolidays)
                {
                    bestLocationHolidays = c;
                }
            }

            return n + bestLocationHolidays;
        }

        private Payload MaximizeNationalHolidaysTopDownDP(Graph graph, int currentLocation, int month, int[][] holidayByLocation, Payload[][] memArray)
        {
            // SS: top-down dynamic programming solution
            // runtime complexity: O(#locations * 12), i.e. O(#locations)

            const int maxMonths = 12;
            if (month == maxMonths)
            {
                // done
                return null;
            }

            var payload = memArray[currentLocation][month];
            if (payload != null)
            {
                return payload;
            }

            // SS: number of holidays for this month at the current location
            var n = holidayByLocation[currentLocation][month];

            var bestPayload = new Payload {Holidays = 0, Path = new int[maxMonths]};

            // SS: find all locations that we can reach from this one
            // (this includes the current one, i.e. we stay at the location we are)
            var targetLocations = graph.AdjacencyList[currentLocation];
            for (var i = 0; i < targetLocations.Count; i++)
            {
                var targetLocation = targetLocations[i];
                var c = MaximizeNationalHolidaysTopDownDP(graph, targetLocation, month + 1, holidayByLocation, memArray);
                if (c != null && c.Holidays >= bestPayload.Holidays)
                {
                    bestPayload = c;
                }
            }

            // SS: create copy of path
            var p = new int[maxMonths];
            Array.Copy(bestPayload.Path, p, maxMonths);
            p[month] = currentLocation + 1;

            payload = new Payload {Holidays = n + bestPayload.Holidays, Path = p};
            memArray[currentLocation][month] = payload;

            return payload;
        }

        private class Payload
        {
            public int Holidays { get; set; } = -1;
            public int[] Path { get; set; } = new int[12];
        }

        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1()
            {
                // Arrange

                // row: location
                // column: month
                var holidaysByLocation = new[]
                {
                    new[] {0, 0, 0, 0}
                    , new[] {0, 1, 0, 0}
                    , new[] {0, 0, 1, 0}
                    , new[] {0, 0, 0, 1}
                };

                // SS: flight durations don't have to be symmetric between locations...
                int[][] flightDuration =
                {
                    new[] {0, 2, 7, 7}
                    , new[] {2, 0, 2, 7}
                    , new[] {7, 2, 0, 2}
                    , new[] {7, 7, 7, 0}
                };

                // Act
                var itinerary = new Solution().SolveTopDownDP(0, holidaysByLocation, flightDuration);

                // Assert
                Assert.AreEqual(3, itinerary);
            }

            [Test]
            public void Test2()
            {
                // Arrange
                var holidaysByLocation = new[]
                {
                    new[] {0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}
                    , new[] {0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0}
                    , new[] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1}
                    , new[] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}
                    , new[] {0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0}
                };

                // SS: flight durations don't have to be symmetric between locations...
                int[][] flightDuration =
                {
                    new[] {0, 2, 7, 7, 7}
                    , new[] {2, 0, 2, 3, 7}
                    , new[] {7, 2, 0, 2, 7}
                    , new[] {7, 2, 3, 0, 3}
                    , new[] {7, 7, 7, 2, 0}
                };

                // Act
                var itinerary = new Solution().SolveTopDownDP(0, holidaysByLocation, flightDuration);

                // Assert
                Assert.AreEqual(4, itinerary);
            }
        }
    }
}