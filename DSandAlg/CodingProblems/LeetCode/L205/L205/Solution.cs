#region

using System.Collections.Generic;
using NUnit.Framework;

#endregion

// Problem: 205. Isomorphic Strings
// URL: https://leetcode.com/problems/isomorphic-strings/

namespace LeetCode
{
    public class Solution
    {
        public bool IsIsomorphic(string s, string t)
        {
            // SS: runtime complexity: O(n)
            // space complexity: O(1)

            var dict = new Dictionary<char, char>();

            for (var i = 0; i < s.Length; i++)
            {
                var c1 = s[i];
                var c2 = t[i];

                if (dict.ContainsKey(c1))
                {
                    var m = dict[c1];
                    if (m != c2)
                    {
                        return false;
                    }
                }
                else if (dict.ContainsValue(c2))
                {
                    // SS: we have mapped a char != c1 to c2, so
                    // not isomorphic
                    return false;
                }
                else
                {
                    dict[c1] = c2;
                }
            }

            return true;
        }

        [TestFixture]
        public class Tests
        {
            [TestCase("egg", "add", true)]
            [TestCase("foo", "bar", false)]
            [TestCase("paper", "title", true)]
            [TestCase("badc", "baba", false)]
            public void Test1(string s, string t, bool expectedIsomorphic)
            {
                // Arrange

                // Act
                var isomorphic = new Solution().IsIsomorphic(s, t);

                // Assert
                Assert.AreEqual(expectedIsomorphic, isomorphic);
            }
        }
    }
}