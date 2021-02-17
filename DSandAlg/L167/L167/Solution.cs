using System.Collections.Generic;
using NUnit.Framework;

// Problem: 167. Two Sum II - Input array is sorted
// URL: https://leetcode.com/problems/two-sum-ii-input-array-is-sorted/

namespace LeetCode
{
    public class Solution
    {
        public int[] TwoSum(int[] numbers, int target)
        {
            return TwoSum1(numbers, target);
        }

        private int[] TwoSum1(int[] numbers, int target)
        {
            // SS: here we are not utilizing the fact that numbers is sorted
            // runtime complexity: O(n)
            // space complexity: O(n)
            
            var map = new Dictionary<int, int>();

            for (int i = 0; i < numbers.Length; i++)
            {
                int v1 = numbers[i];
                int d = target - v1;
                
                if (map.TryGetValue(v1, out int idx))
                {
                    int[] result = {idx + 1, i + 1};
                    return result;
                }

                map[d] = i;
            }

            return new int[0];
        }

        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1()
            {
                // Arrange
                int[] numbers = {2,7,11,15};
                int target = 9;

                // Act
                int[] result = new Solution().TwoSum(numbers, target);

                // Assert
                Assert.AreEqual(1, result[0]);
                Assert.AreEqual(2, result[1]);
            }
            
            [Test]
            public void Test2()
            {
                // Arrange
                int[] numbers = {2,3, 4};
                int target = 6;

                // Act
                int[] result = new Solution().TwoSum(numbers, target);

                // Assert
                Assert.AreEqual(1, result[0]);
                Assert.AreEqual(3, result[1]);
            }
            
            [Test]
            public void Test3()
            {
                // Arrange
                int[] numbers = {-1, 0};
                int target = -1;

                // Act
                int[] result = new Solution().TwoSum(numbers, target);

                // Assert
                Assert.AreEqual(1, result[0]);
                Assert.AreEqual(2, result[1]);
            }
            
            [Test]
            public void Test4()
            {
                // Arrange
                int[] numbers = {0, 0, 3, 4};
                int target = 0;

                // Act
                int[] result = new Solution().TwoSum(numbers, target);

                // Assert
                Assert.AreEqual(1, result[0]);
                Assert.AreEqual(2, result[1]);
            }
            
        }
    }
}