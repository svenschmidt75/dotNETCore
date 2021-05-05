#region

using System.Collections.Generic;
using NUnit.Framework;

#endregion

// Problem: 167. Two Sum II - Input array is sorted
// URL: https://leetcode.com/problems/two-sum-ii-input-array-is-sorted/

namespace LeetCode
{
    public class Solution
    {
        public int[] TwoSum(int[] numbers, int target)
        {
//            return TwoSum1(numbers, target);
            return TwoSum2(numbers, target);
        }

        private int[] TwoSum2(int[] numbers, int target)
        {
            // SS: sliding window approach
            // runtime complexity: O(n)
            // space complexity: O(1)
            
            var i = 0;
            var j = numbers.Length - 1;

            while (i < j)
            {
                var n = numbers[i] + numbers[j];

                if (n > target)
                {
                    j--;
                }
                else if (n < target)
                {
                    i++;
                }
                else
                {
                    return new[] {i + 1, j + 1};
                }
            }

            return new int[0];
        }

        private int[] TwoSum1(int[] numbers, int target)
        {
            // SS: here we are not utilizing the fact that numbers is sorted
            // runtime complexity: O(n)
            // space complexity: O(n)

            var map = new Dictionary<int, int>();

            for (var i = 0; i < numbers.Length; i++)
            {
                var v1 = numbers[i];
                var d = target - v1;

                if (map.TryGetValue(v1, out var idx))
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
                int[] numbers = {2, 7, 11, 15};
                var target = 9;

                // Act
                var result = new Solution().TwoSum(numbers, target);

                // Assert
                Assert.AreEqual(1, result[0]);
                Assert.AreEqual(2, result[1]);
            }

            [Test]
            public void Test2()
            {
                // Arrange
                int[] numbers = {2, 3, 4};
                var target = 6;

                // Act
                var result = new Solution().TwoSum(numbers, target);

                // Assert
                Assert.AreEqual(1, result[0]);
                Assert.AreEqual(3, result[1]);
            }

            [Test]
            public void Test3()
            {
                // Arrange
                int[] numbers = {-1, 0};
                var target = -1;

                // Act
                var result = new Solution().TwoSum(numbers, target);

                // Assert
                Assert.AreEqual(1, result[0]);
                Assert.AreEqual(2, result[1]);
            }

            [Test]
            public void Test4()
            {
                // Arrange
                int[] numbers = {0, 0, 3, 4};
                var target = 0;

                // Act
                var result = new Solution().TwoSum(numbers, target);

                // Assert
                Assert.AreEqual(1, result[0]);
                Assert.AreEqual(2, result[1]);
            }
        }
    }
}