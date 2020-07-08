#region

using System;
using System.Linq;
using NUnit.Framework;

#endregion

namespace GoogleProblem22
{
    public class GP22
    {
        /// <summary>
        /// 1. Floyd-Warshall All Pair Shortest Path at O(N^3), then test each vertex at O(N), so O(N^4)
        /// 2. Dijkstra's Shortest Path at O((E + V) * log V), for each vertex as starting vertex, so O((E + V) * V log V)
        /// 3. Center of mass, at O(N)
        /// 4. For each vertex, calc. total distance, so O(N^2) 
        /// </summary>
        /// <param name="people"></param>
        /// <returns></returns>
        public (int minDistX, int minDistY) GetMeetingLocation2(int[][] people)
        {
            // SS: calculate center of mass
            // runtime complexity: O(N), where N = number of vertices between (minX, maxX), (minY, maxY)
            double x = 0;
            double y = 0;

            for (var i = 0; i < people.Length; i++)
            {
                var position = people[i];
                x += position[0];
                y += position[1];
            }

            x /= people.Length;
            y /= people.Length;

            var minX = (int) Math.Round(x, MidpointRounding.AwayFromZero);
            var minY = (int) Math.Round(y, MidpointRounding.AwayFromZero);

            return (minX, minY);
        }

        public (int minDistX, int minDistY) GetMeetingLocation1(int[][] people)
        {
            // runtime complexity: O(N^2), where N = number of vertices between (minX, maxX), (minY, maxY)

            var minX = int.MaxValue;
            var maxX = int.MinValue;

            var minY = int.MaxValue;
            var maxY = int.MinValue;

            for (var k = 0; k < people.Length; k++)
            {
                var position = people[k];

                minX = Math.Min(minX, position[0]);
                maxX = Math.Max(maxX, position[0]);

                minY = Math.Min(minY, position[1]);
                maxY = Math.Max(maxY, position[1]);
            }

            var minDist = int.MaxValue;
            var minDistX = 0;
            var minDistY = 0;

            for (var i = minX; i <= maxX; i++)
            {
                for (var j = minY; j <= maxY; j++)
                {
                    // check all people's distances from position (i, j)
                    var dist = TotalDistance(people, i, j);
                    if (dist < minDist)
                    {
                        minDist = dist;
                        minDistX = i;
                        minDistY = j;
                    }
                }
            }

            return (minDistX, minDistY);
        }

        public static int TotalDistance(int[][] people, int i, int j)
        {
            var dist = 0;
            for (var k = 0; k < people.Length; k++)
            {
                var position = people[k];
                var d = GetManhattanDistance(i, j, position[0], position[1]);
                dist += d;
            }

            return dist;
        }

        public static int GetManhattanDistance(int x1, int y1, int x2, int y2)
        {
            var dx = Math.Abs(x1 - x2);
            var dy = Math.Abs(y1 - y2);
            return dx + dy;
        }
    }

    [TestFixture]
    public class Tests
    {
        [Test]
        public void Test11()
        {
            // Arrange
            int[][] people = {new[] {1, 1}, new[] {2, 3}, new[] {4, 2}, new[] {4, 4}};

            // Act
            var (minX, minY) = new GP22().GetMeetingLocation1(people);

            // Assert
            var distance = GP22.TotalDistance(people, minX, minY);
            Assert.AreEqual(9, distance);
        }

        [Test]
        public void Test12()
        {
            // Arrange
            int[][] people = {new[] {1, 1}, new[] {4, 1}, new[] {2, 2}, new[] {6, 3}, new[] {3, 4}, new[] {0, 5}};

            // Act
            var (minX, minY) = new GP22().GetMeetingLocation1(people);

            // Assert
            var distance = GP22.TotalDistance(people, minX, minY);
            Assert.AreEqual(18, distance);
        }

        [Test]
        public void Test13()
        {
            // Arrange
            var random = new Random(DateTime.Now.Millisecond);
            var nPeoples = random.Next(5, 100);
            var people = Enumerable.Range(0, nPeoples).Select(i => new[] {random.Next(1, 100), random.Next(1, 100)})
                .ToArray();

            // Act
            var (minX, minY) = new GP22().GetMeetingLocation2(people);
            var distance1 = GP22.TotalDistance(people, minX, minY);

            (minX, minY) = new GP22().GetMeetingLocation2(people);
            var distance2 = GP22.TotalDistance(people, minX, minY);

            // Assert
            Assert.AreEqual(distance1, distance2);
        }

        [Test]
        public void Test21()
        {
            // Arrange
            int[][] people = {new[] {1, 1}, new[] {2, 3}, new[] {4, 2}, new[] {4, 4}};

            // Act
            var (minX, minY) = new GP22().GetMeetingLocation2(people);

            // Assert
            var distance = GP22.TotalDistance(people, minX, minY);
            Assert.AreEqual(9, distance);
        }

        [Test]
        public void Test22()
        {
            // Arrange
            int[][] people = {new[] {1, 1}, new[] {4, 1}, new[] {2, 2}, new[] {6, 3}, new[] {3, 4}, new[] {0, 5}};

            // Act
            var (minX, minY) = new GP22().GetMeetingLocation2(people);

            // Assert
            var distance = GP22.TotalDistance(people, minX, minY);
            Assert.AreEqual(18, distance);
        }
    }
}