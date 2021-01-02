#region

using NUnit.Framework;

#endregion

// Problem: 134. Gas Station
// URL: https://leetcode.com/problems/gas-station/

namespace LeetCode
{
    public class Solution
    {
        public int CanCompleteCircuit(int[] gas, int[] cost)
        {
            // SS: runtime complexity: O(N^2)

            for (var i = 0; i < gas.Length; i++)
            {
                var tank = gas[i];
                var c = cost[i];

                var j = i + 1;
                while (true)
                {
                    tank -= c;
                    if (tank < 0)
                    {
                        // SS: no a solution
                        break;
                    }

                    if (j == gas.Length)
                    {
                        j = 0;
                    }

                    if (j == i)
                    {
                        // SS: found a solution
                        return i;
                    }

                    tank += gas[j];
                    c = cost[j];

                    j++;
                }
            }

            return -1;
        }

        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1()
            {
                // Arrange
                int[] gas = {1, 2, 3, 4, 5};
                int[] cost = {3, 4, 5, 1, 2};

                // Act
                var stationIndex = new Solution().CanCompleteCircuit(gas, cost);

                // Assert
                Assert.AreEqual(3, stationIndex);
            }

            [Test]
            public void Test2()
            {
                // Arrange
                int[] gas = {2, 3, 4};
                int[] cost = {3, 4, 3};

                // Act
                var stationIndex = new Solution().CanCompleteCircuit(gas, cost);

                // Assert
                Assert.AreEqual(-1, stationIndex);
            }
        }
    }
}