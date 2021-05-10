#region

using System.Collections.Generic;
using NUnit.Framework;

#endregion

// Problem: 784. Letter Case Permutation
// URL: https://leetcode.com/problems/letter-case-permutation/

namespace LeetCode
{
    public class Solution
    {
        public IList<string> LetterCasePermutation(string s)
        {
            // SS: do preorder DFS traversal of call tree
            // runtime complexity: O(2^s)
            // space complexity: O(s) (recursive call stack)

            var result = new List<string>();

            void DFS(int sIdx, string str)
            {
                // SS: pre-order traversal
                if (sIdx == s.Length)
                {
                    if (string.IsNullOrWhiteSpace(str) == false)
                    {
                        result.Add(str);
                    }

                    return;
                }

                // SS: ignore digits
                if (char.IsDigit(s[sIdx]))
                {
                    DFS(sIdx + 1, $"{str}{s[sIdx]}");
                }
                else
                {
                    DFS(sIdx + 1, $"{str}{char.ToLower(s[sIdx])}");
                    DFS(sIdx + 1, $"{str}{char.ToUpper(s[sIdx])}");
                }
            }

            DFS(0, "");
            return result;
        }

        [TestFixture]
        public class Tests
        {
            [TestCase("a1b1", new[] {"a1b1", "a1B1", "A1b1", "A1B1"})]
            [TestCase("3z4", new[] {"3z4", "3Z4"})]
            [TestCase("12345", new[] {"12345"})]
            [TestCase("0", new[] {"0"})]
            public void Test(string s, string[] expected)
            {
                // Arrange

                // Act
                var result = new Solution().LetterCasePermutation(s);

                // Assert
                CollectionAssert.AreEquivalent(expected, result);
            }
        }
    }
}