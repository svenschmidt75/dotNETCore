#region

using System;
using NUnit.Framework;

#endregion

namespace GoogleProblem28
{
    public class Solution
    {
        private const int MaxMonths = 12;

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

            var itinerary = MaximizeNationalHolidaysTopDownDP(graph, startLocation, 0, holidayByLocation, memArray);
            return itinerary.Holidays;
        }

        public Payload SolveBottomUpDP(int startLocation, int[][] holidayByLocation, int[][] flightDurations)
        {
            // SS: bottom-up DP solution
            // runtime complexity: O(12 * #locations * #locations) = O(#locations^2)
            // space complexity: O(#locations) (that's a big difference to top-down DP!) 

            var graph = CreateGraph(flightDurations);

            var row1 = new Payload[holidayByLocation.Length];
            for (var i = 0; i < row1.Length; i++)
            {
                row1[i] = new Payload();
            }

            var row2 = new Payload[holidayByLocation.Length];
            for (var i = 0; i < row2.Length; i++)
            {
                row2[i] = new Payload();
            }

            // SS: fill in 1st row with holidays
            for (var i = 0; i < holidayByLocation.Length; i++)
            {
                var locationHolidays = holidayByLocation[i];
                row1[i].Holidays = locationHolidays[MaxMonths - 1];
                row1[i].Path[MaxMonths - 1] = i;
            }

            var currentRow = row2;
            var prevRow = row1;

            var month = MaxMonths - 2;
            while (month >= 0)
            {
                for (var currentLocation = 0; currentLocation < holidayByLocation.Length; currentLocation++)
                {
                    var bestPayload = new Payload {Holidays = 0, Path = new int[MaxMonths]};

                    // SS: find all locations that we can reach from this one
                    // (this includes the current one, i.e. we stay at the location we are)
                    var targetLocations = graph.AdjacencyList[currentLocation];
                    for (var j = 0; j < targetLocations.Count; j++)
                    {
                        var targetLocation = targetLocations[j];
                        var c = prevRow[targetLocation];
                        if (c.Holidays >= bestPayload.Holidays)
                        {
                            bestPayload = c;
                        }
                    }

                    var holidaysCurrentLocation = holidayByLocation[currentLocation];
                    var n = holidaysCurrentLocation[month];

                    currentRow[currentLocation].Holidays = n + bestPayload.Holidays;

                    // SS: copy best path to currentLocation
                    Array.Copy(bestPayload.Path, currentRow[currentLocation].Path, MaxMonths);
                    currentRow[currentLocation].Path[month] = currentLocation;
                }

                var tmp = currentRow;
                currentRow = prevRow;
                prevRow = tmp;

                month--;
            }

            var itinerary = prevRow[startLocation];
            return itinerary;
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

            // SS: find all locations that we can reach from this one
            // (this includes the current one, i.e. we stay at the location we are)
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
            // runtime complexity: O(#locations * #locations * 12), i.e. O(#locations^2)

            if (month == MaxMonths)
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

            var bestPayload = new Payload {Holidays = 0, Path = new int[MaxMonths]};

            // SS: find all locations that we can reach from this one
            // (this includes the current one, i.e. we stay at the location we are)
            var targetLocations = graph.AdjacencyList[currentLocation];
            for (var i = 0; i < targetLocations.Count; i++)
            {
                var targetLocation = targetLocations[i];
                var c = MaximizeNationalHolidaysTopDownDP(graph, targetLocation, month + 1, holidayByLocation
                    , memArray);
                if (c != null && c.Holidays >= bestPayload.Holidays)
                {
                    bestPayload = c;
                }
            }

            // SS: create copy of path
            var p = new int[MaxMonths];
            Array.Copy(bestPayload.Path, p, MaxMonths);
            p[month] = currentLocation + 1;

            payload = new Payload {Holidays = n + bestPayload.Holidays, Path = p};
            memArray[currentLocation][month] = payload;

            return payload;
        }

        public class Payload
        {
            public int Holidays { get; set; } = -1;
            public int[] Path { get; set; } = new int[12];
        }

        [TestFixture]
        public class Tests
        {
            private static int HolidaysForPath(int[] path, int[][] holidayByLocation)
            {
                var n = 0;
                for (var month = 0; month < 12; month++)
                {
                    var location = path[month];
                    var j = holidayByLocation[location][month];
                    n += j;
                }

                return n;
            }

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
                var itinerary1 = new Solution().SolveTopDownDP(0, holidaysByLocation, flightDuration);
                var itinerary2 = new Solution().SolveBottomUpDP(0, holidaysByLocation, flightDuration);

                // Assert
                Assert.AreEqual(4, itinerary1);
                Assert.AreEqual(itinerary2.Holidays, itinerary1);
                Assert.AreEqual(itinerary2.Holidays, HolidaysForPath(itinerary2.Path, holidaysByLocation));
            }
        }
    }
}