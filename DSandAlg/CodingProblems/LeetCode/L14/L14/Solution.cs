

// 14. Longest Common Prefix
// https://leetcode.com/problems/longest-common-prefix/

using System.Collections.Generic;
using NUnit.Framework;

namespace L14
{
    public class Solution
    {
        public string LongestCommonPrefix(string[] strs)
        {
            return LongestCommonPrefix1(strs);
        }

        public string LongestCommonPrefix1(string[] strs)
        {
            // SS: runtime complexity: O(N * L)
            // N: strs.Length, L = longest string

            if (strs.Length == 0)
            {
                return "";
            }

            var pl = 0;
            List<char> result = new List<char>();

            while (true)
            {
                var cnt = 0;

                var c = (char) 0;

                for (var i = 0; i < strs.Length; i++)
                {
                    var s = strs[i];
                    if (pl >= s.Length)
                    {
                        return s;
                    }

                    if (c == (char) 0)
                    {
                        c = s[pl];
                    }

                    if (c == s[pl])
                    {
                        cnt++;
                    }
                }

                if (cnt == strs.Length)
                {
                    pl++;
                    result.Add(c);
                }
                else
                {
                    return new string(result.ToArray());
                }
            }
        }

        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1()
            {
                // Arrange
                string[] strs = new[] {"flower", "flow", "flight"};

                // Act
                string lcp = new Solution().LongestCommonPrefix(strs);

                // Assert
                Assert.AreEqual("fl", lcp);
            }
            
            [Test]
            public void Test2()
            {
                // Arrange
                string[] strs = new[] {"dog","racecar","car"};

                // Act
                string lcp = new Solution().LongestCommonPrefix(strs);

                // Assert
                Assert.AreEqual("", lcp);
            }

        }
    }
}