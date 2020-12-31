using NUnit.Framework;

// Problem: 115. Distinct Subsequences
// URL: https://leetcode.com/problems/distinct-subsequences/

namespace LeetCode
{
    public class Solution
    {
        public int NumDistinct(string s, string t)
        {
            return NumDistinctDivideConquer(s, t);
        }

        private int NumDistinctDivideConquer(string s, string t)
        {
            if (s.Length == 0 || t.Length == 0)
            {
                return 0;
            }

            int Solve(int sIdx, int tIdx)
            {
                if (tIdx == t.Length)
                {
                    // SS: tIdx only advances when s[sIdx] and t[tIdx] match,
                    // so if we reach the end of t, we have a match
                    return 1;
                }

                if (sIdx == s.Length)
                {
                    // SS: s exhausted, but not t, no match
                    return 0;
                }

                int cnt = 0;

                // SS: both chars equal
                if (s[sIdx] == t[tIdx])
                {
                    cnt = Solve(sIdx + 1, tIdx + 1);
                }

                // SS: skip one char in s
                cnt += Solve(sIdx + 1, tIdx);

                return cnt;
            }

            int n = Solve(0, 0);
            return n;
        }

        [TestFixture]
        public class Tests
        {
            [TestCase("rabbbit", "rabbit", 3)]
            [TestCase("babgbag", "bag", 5)]
            [TestCase("rbababbit", "rabbit", 4)]
            public void Test(string s, string t, int nExpected)
            {
                // Arrange

                // Act
                int n = new Solution().NumDistinct(s, t);

                // Assert
                Assert.AreEqual(nExpected, n);
            }
            
        }
    }
}