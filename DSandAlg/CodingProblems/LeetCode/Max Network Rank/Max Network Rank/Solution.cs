#region

using System;
using System.Collections.Generic;
using NUnit.Framework;

#endregion

// Microsoft | OA 2019 | Max Network Rank
// https://leetcode.com/discuss/interview-question/364760/

namespace Max_Network_Rank
{
    public class Solution
    {
        public int MaxNetworkRank(int[] a, int[] b, int n)
        {
            // SS: runtime complexity: O(V + E)

            var toVisit = new HashSet<int>();

            // SS: create graph
            var adjMatrix = new Dictionary<int, List<int>>();

            for (var i = 1; i <= n; i++)
            {
                adjMatrix[i] = new List<int>();
            }

            for (var i = 0; i < a.Length; i++)
            {
                var city1 = a[i];
                var city2 = b[i];

                adjMatrix[city1].Add(city2);
                adjMatrix[city2].Add(city1);

                toVisit.Add(city1);
                toVisit.Add(city2);
            }

            // SS: visit each city
            var maxRank = 0;

            for (var i = 1; i <= n; i++)
            {
                if (toVisit.Contains(i) == false)
                {
                    // SS: we have already visited this city
                    continue;
                }

                toVisit.Remove(i);

                var neighbors = adjMatrix[i];
                var rank = neighbors.Count;

                for (var j = 0; j < neighbors.Count; j++)
                {
                    var neighbor = neighbors[j];
                    var otherRank = adjMatrix[neighbor].Count;
                    maxRank = Math.Max(maxRank, rank + otherRank - 1);
                }
            }

            return maxRank;
        }

        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1()
            {
                // Arrange
                int[] a = {1, 2, 3, 3};
                int[] b = {2, 3, 1, 4};

                // Act
                var maxNetworkRank = new Solution().MaxNetworkRank(a, b, 4);

                // Assert
                Assert.AreEqual(4, maxNetworkRank);
            }
        }
    }
}