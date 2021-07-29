using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

// Problem: Dynamic Programming - Learn to Solve Algorithmic Problems & Coding Challenges, howSum
// URL: https://www.youtube.com/watch?v=oBt53YbR9Kk

namespace LeetCode
{
    public class Solution
    {
        public IList<int> HowSum(int[] array, int target)
        {
            return HowSum1(array, target);
        }

        private IList<int> HowSum1(int[] array, int target)
        {
            // SS: Divide & Conquer
            // runtime complexity: O(m^target)
            // space complexity: O(target + n)
            // This can be solved via DP because we can short-circuit
            // the evaluation once we have a non-null return array...
            
            // SS: base case
            if (target < 0)
            {
                return null;
            }

            if (target == 0)
            {
                return new List<int>();
            }

            for (int i = 0; i < array.Length; i++)
            {
                int v = array[i];
                var res = HowSum1(array, target - v);
                if (res != null)
                {
                    res.Add(v);
                    return res;
                }
            }

            return null;
        }

        [TestFixture]
        public class Tests
        {
            [TestCase(new[] { 5, 3, 4, 7 }, 7)]
            [TestCase(new[] { 2, 4, 5 }, 8)]
            public void Test1(int[] array, int target)
            {
                // Arrange

                // Act
                var result = new Solution().HowSum(array, target);

                // Assert
                Assert.AreEqual(target, result.Sum());
            }

            [TestCase(new[] { 2, 4 }, 7)]
            public void Test2(int[] array, int target)
            {
                // Arrange

                // Act
                var result = new Solution().HowSum(array, target);

                // Assert
                Assert.IsNull(result);
            }
        }
    }
}