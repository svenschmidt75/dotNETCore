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
            // SS: runtime complexity: O(n), using variant of Kadane's
            // algorithm.
            // Larry's solution: https://www.youtube.com/watch?v=EjvUgZYChII

            int currentGas = 0;
            int currentLength = 0;
            int maxLength = 0;
            int startIndex = -1;
            
            for (int i = 0; i < 2 * gas.Length; i++)
            {
                int idx = i % gas.Length;

                currentLength++;
                
                // SS: gas after driving to next station
                currentGas += gas[idx] - cost[idx];

                if (currentGas < 0)
                {
                    // SS: invalid
                    currentGas = 0;
                    currentLength = 0;
                }

                if (maxLength < currentLength)
                {
                    // SS: we found a longer way
                    maxLength = currentLength;

                    // SS: start index for this path length
                    startIndex = i - currentLength + 1;
                }
            }

            return maxLength < gas.Length ? -1 : startIndex % gas.Length;
        }

        public int CanCompleteCircuitMine(int[] gas, int[] cost)
        {
            // SS: runtime complexity: O(n), using variant of Kadane's
            // algorithm.

            int tank = 0;
            int startIndex = 0;
            
            for (int i = 0; i < 2 * gas.Length; i++)
            {
                int idx = i % gas.Length;
                
                // SS: gas after driving to next station
                tank += gas[idx] - cost[idx];

                if (tank < 0)
                {
                    // SS: invalid
                    tank = 0;
                    startIndex = i + 1;
                }
                else if (i - startIndex == gas.Length)
                {
                    return startIndex % gas.Length;
                }
            }

            return -1;
        }

        public int CanCompleteCircuitSlow(int[] gas, int[] cost)
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

            [Test]
            public void Test3()
            {
                // Arrange
                int[] gas = {3, 3, 4};
                int[] cost = {3, 4, 4};

                // Act
                var stationIndex = new Solution().CanCompleteCircuit(gas, cost);

                // Assert
                Assert.AreEqual(-1, stationIndex);
            }
            
            [Test]
            public void Test4()
            {
                // Arrange
                int[] gas = {3, 1, 1};
                int[] cost = {1, 2, 2};

                // Act
                var stationIndex = new Solution().CanCompleteCircuit(gas, cost);

                // Assert
                Assert.AreEqual(0, stationIndex);
            }

            [Test]
            public void Test5()
            {
                // Arrange
                int[] gas = {5, 8, 2, 8};
                int[] cost = {6, 5, 6, 6};

                // Act
                var stationIndex = new Solution().CanCompleteCircuit(gas, cost);

                // Assert
                Assert.AreEqual(3, stationIndex);
            }
        }
    }
}