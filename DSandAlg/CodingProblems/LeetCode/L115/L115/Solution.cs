#region

using NUnit.Framework;

#endregion

// Problem: 115. Distinct Subsequences
// URL: https://leetcode.com/problems/distinct-subsequences/

namespace LeetCode
{
    public class Solution
    {
        public int NumDistinct(string s, string t)
        {
//            return NumDistinctDivideConquer(s, t);
            return NumDistinctBottomUp(s, t);
        }

        private int NumDistinctBottomUp(string s, string t)
        {
            // SS: runtime complexity: O(s * t)
            // space complexity: O(s)
            if (s.Length == 0)
            {
                return t.Length == 0 ? 1 : 0;
            }

            if (t.Length == 0)
            {
                return 1;
            }

            var dp1 = new int[s.Length + 1];
            var dp2 = new int[s.Length + 1];

            // SS: set boundary conditions
            for (var i = 0; i <= s.Length; i++)
            {
                // SS: if t is exhausted, we have a match
                dp2[i] = 1;
            }

            for (var i = t.Length - 1; i >= 0; i--)
            {
                // SS: set no match when s is exhausted but t isn't
                dp1[^1] = 0;

                for (var j = s.Length - 1; j >= 0; j--)
                {
                    var n = 0;

                    if (s[j] == t[i])
                    {
                        n = dp2[j + 1];
                    }

                    n += dp1[j + 1];
                    dp1[j] = n;
                }

                var tmp = dp1;
                dp1 = dp2;
                dp2 = tmp;
            }

            return dp2[0];
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

                var cnt = 0;

                // SS: both chars equal
                if (s[sIdx] == t[tIdx])
                {
                    cnt = Solve(sIdx + 1, tIdx + 1);
                }

                // SS: skip one char in s
                cnt += Solve(sIdx + 1, tIdx);

                return cnt;
            }

            var n = Solve(0, 0);
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
                var n = new Solution().NumDistinct(s, t);

                // Assert
                Assert.AreEqual(nExpected, n);
            }
        }
    }
}