#region

using System.Collections.Generic;
using NUnit.Framework;

#endregion

// Problem: 131. Palindrome Partitioning
// URL: https://leetcode.com/problems/palindrome-partitioning/

namespace LeetCode
{
    public class Solution
    {
        public IList<IList<string>> Partition(string s)
        {
            // SS: solve using backtracking
            // runtime performance: O(s^2)

            var results = new List<IList<string>>();

            void Solve(int idx, IList<string> r)
            {
                if (idx == s.Length)
                {
                    results.Add(new List<string>(r));
                    return;
                }

                for (var i = idx; i < s.Length; i++)
                {
                    if (IsPalindrome(s, idx, i))
                    {
                        // SS: string between [left, right] is a palindrome
                        r.Add(s[idx..(i + 1)]);
                        Solve(i + 1, r);

                        // SS: backtrack
                        r.RemoveAt(r.Count - 1);
                    }
                }
            }

            Solve(0, new List<string>());

            return results;
        }

        private static bool IsPalindrome(string s, int left, int right)
        {
            // SS: O(right - left)
            while (left <= right)
            {
                if (s[left] != s[right])
                {
                    return false;
                }

                left++;
                right--;
            }

            return true;
        }

        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1()
            {
                // Arrange
                var s = "aab";

                // Act
                var results = new Solution().Partition(s);

                // Assert
                CollectionAssert.AreEquivalent(new[] {new[] {"a", "a", "b"}, new[] {"aa", "b"}}, results);
            }

            [Test]
            public void Test2()
            {
                // Arrange
                var s = "madam";

                // Act
                var results = new Solution().Partition(s);

                // Assert
                CollectionAssert.AreEquivalent(new[] {new[] {"m", "a", "d", "a", "m"}, new[] {"m", "ada", "m"}, new[] {"madam"}}, results);
            }

            [Test]
            public void Test3()
            {
                // Arrange
                var s = "paap";

                // Act
                var results = new Solution().Partition(s);

                // Assert
                CollectionAssert.AreEquivalent(new[] {new[] {"p", "a", "a", "p"}, new[] {"p", "aa", "p"}, new[] {"paap"}}, results);
            }
        }
    }
}