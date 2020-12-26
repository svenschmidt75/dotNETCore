#region

using System.Collections.Generic;
using NUnit.Framework;

#endregion

// Problem: 60. Permutation Sequence
// URL: https://leetcode.com/problems/permutation-sequence/

namespace LeetCode
{
    public class Solution
    {
        public string GetPermutation(int n, int k)
        {
            // SS: runtime complexity: O(n^2)
            // space complexity: O(n)

            if (n == 1)
            {
                return "1";
            }

            var digits = new List<int>();
            for (var i = 0; i < n; i++)
            {
                digits.Add(i + 1);
            }

            // SS: number of combinations placing (n - 1)
            // digits at position 1 (0-based)
            var combinations = 1;
            for (var i = n - 1; i >= 2; i--)
            {
                combinations *= i;
            }

            var tmpResult = new char[n];
            var myK = k - 1;
            var combIdx = n - 1;

            for (var i = 0; i < n; i++)
            {
                var nComb = myK / combinations;
                var d = (char) (digits[nComb] + '0');
                tmpResult[i] = d;

                // SS: remove from digits array
                digits.RemoveAt(nComb);

                myK -= nComb * combinations;

                if (combIdx > 0)
                {
                    combinations /= combIdx;
                    combIdx--;
                }
            }

            var result = new string(tmpResult);
            return result;
        }

        [TestFixture]
        public class Tests
        {
            [TestCase(1, 2, "1")]
            [TestCase(3, 3, "213")]
            [TestCase(4, 9, "2314")]
            [TestCase(6, 17, "125634")]
            [TestCase(9, 6480, "134987652")]
            public void Test1(int n, int k, string expected)
            {
                // Arrange

                // Act
                var result = new Solution().GetPermutation(n, k);

                // Assert
                Assert.AreEqual(expected, result);
            }
        }
    }
}