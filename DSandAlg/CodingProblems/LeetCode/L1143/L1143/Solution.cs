#region

using System;
using NUnit.Framework;

#endregion

// Problem: 1143. Longest Common Subsequence
// URL: https://leetcode.com/problems/longest-common-subsequence/

namespace LeetCode1143
{
    public class Solution
    {
        public int LongestCommonSubsequence(string text1, string text2)
        {
//            return LongestCommonSubsequenceTopDown(text1, text2) - 1;
            return LongestCommonSubsequenceBottomUp(text1, text2);
        }

        public int LongestCommonSubsequenceTopDown(string text1, string text2)
        {
            // SS: runtime and space complexity: O(text1.Length * text2.Length)
            // (because we fill in a memoization grid of dimension text1.Length * text2.Length

            var memArray = new int[text1.Length][];
            for (var i = 0; i < text1.Length; i++)
            {
                memArray[i] = new int[text2.Length];
            }

            LongestCommonSubsequenceTopDown(text1, text2, 0, 0, memArray);

            return memArray[0][0];
        }

        private int LongestCommonSubsequenceTopDown(string text1, string text2, int pos1, int pos2, int[][] memArray)
        {
            if (pos1 == text1.Length || pos2 == text2.Length)
            {
                return 1;
            }

            if (memArray[pos1][pos2] > 0)
            {
                return memArray[pos1][pos2];
            }

            var l1 = LongestCommonSubsequenceTopDown(text1, text2, pos1 + 1, pos2, memArray) - 1;
            var l2 = LongestCommonSubsequenceTopDown(text1, text2, pos1, pos2 + 1, memArray) - 1;
            var l3 = LongestCommonSubsequenceTopDown(text1, text2, pos1 + 1, pos2 + 1, memArray) + (text1[pos1] == text2[pos2] ? 1 : 0) - 1;

            var total = Math.Max(Math.Max(l1, l2), l3);
            memArray[pos1][pos2] = total + 1;

            return total + 1;
        }

        private int LongestCommonSubsequenceBottomUp(string text1, string text2)
        {
            // SS: runtime complexity: O(text1.Length * text2.Length)
            // SS: space complexity: O(text2.Length)

            var now = new int[text2.Length];
            var prev = new int[text2.Length];

            for (var i = text1.Length - 1; i >= 0; i--)
            {
                for (var j = text2.Length - 1; j >= 0; j--)
                {
                    // SS: l1 from top-down solution
                    var l1 = prev[j];

                    // SS: l1 from top-down solution
                    var l2 = j <= text2.Length - 2 ? now[j + 1] : 0;

                    // SS: l3 from top-down solution
                    var l3 = 0;
                    if (text1[i] == text2[j])
                    {
                        l3 = 1;
                        if (j <= text2.Length - 2)
                        {
                            l3 += prev[j + 1];
                        }
                    }

                    var total = Math.Max(Math.Max(l1, l2), l3);
                    now[j] = total;
                }

                var tmp = now;
                now = prev;
                prev = tmp;
            }

            return prev[0];
        }

        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1()
            {
                // Arrange
                var text1 = "abcde";
                var text2 = "ace";

                // Act
                var maxLength = new Solution().LongestCommonSubsequence(text1, text2);

                // Assert
                Assert.AreEqual(3, maxLength);
            }

            [Test]
            public void Test2()
            {
                // Arrange
                var text1 = "abc";
                var text2 = "abc";

                // Act
                var maxLength = new Solution().LongestCommonSubsequence(text1, text2);

                // Assert
                Assert.AreEqual(3, maxLength);
            }

            [Test]
            public void Test3()
            {
                // Arrange
                var text1 = "abc";
                var text2 = "def";

                // Act
                var maxLength = new Solution().LongestCommonSubsequence(text1, text2);

                // Assert
                Assert.AreEqual(0, maxLength);
            }

            [Test]
            public void Test4()
            {
                // Arrange
                var text1 = "bsbininm";
                var text2 = "jmjkbkjkv";

                // Act
                var maxLength = new Solution().LongestCommonSubsequence(text1, text2);

                // Assert
                Assert.AreEqual(1, maxLength);
            }
        }
    }
}