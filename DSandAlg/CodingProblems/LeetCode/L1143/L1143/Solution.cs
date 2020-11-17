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
//            return LongestCommonSubsequenceTopDown(text1, text2);
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
                return 0;
            }

            if (memArray[pos1][pos2] > 0)
            {
                return memArray[pos1][pos2];
            }

            var l1 = LongestCommonSubsequenceTopDown(text1, text2, pos1 + 1, pos2, memArray);
            var l2 = LongestCommonSubsequenceTopDown(text1, text2, pos1, pos2 + 1, memArray);
            var l3 = LongestCommonSubsequenceTopDown(text1, text2, pos1 + 1, pos2 + 1, memArray) + (text1[pos1] == text2[pos2] ? 1 : 0);

            var total = Math.Max(Math.Max(l1, l2), l3);
            memArray[pos1][pos2] = total;

            return total;
        }

        private int LongestCommonSubsequenceBottomUp(string text1, string text2)
        {
            // SS: runtime complexity: O(text1.Length * text2.Length)
            // SS: space complexity: O(text2.Length)

            var row1 = new int[text2.Length];
            var row2 = new int[text2.Length];

            var tmp = new[] {row1, row2};

            var rowIdx1 = 1;
            var rowIdx2 = 0;

            for (var i = text1.Length - 1; i >= 0; i--)
            {
                for (var j = text2.Length - 1; j >= 0; j--)
                {
                    // SS: l1 from top-down solution
                    var l1 = tmp[rowIdx2][j];

                    // SS: l1 from top-down solution
                    var l2 = j <= text2.Length - 2 ? tmp[rowIdx1][j + 1] : 0;

                    // SS: l3 from top-down solution
                    var l3 = 0;
                    if (text1[i] == text2[j])
                    {
                        l3 = 1;
                        if (j <= text2.Length - 2)
                        {
                            l3 += tmp[rowIdx2][j + 1];
                        }
                    }

                    var total = Math.Max(Math.Max(l1, l2), l3);
                    tmp[rowIdx1][j] = total;
                }

                if (rowIdx1 == 1)
                {
                    rowIdx1 = 0;
                    rowIdx2 = 1;
                }
                else
                {
                    rowIdx1 = 1;
                    rowIdx2 = 0;
                }
            }

            return tmp[rowIdx2][0];
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