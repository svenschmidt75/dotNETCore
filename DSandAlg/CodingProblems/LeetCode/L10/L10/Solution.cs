#region

using NUnit.Framework;

#endregion

namespace L10
{
    public class Solution
    {
        public bool IsMatch(string s, string p)
        {
            return IsMatch(s, p, 0, 0);
        }

        private bool IsMatch(string s, string p, int sIdx, int pIdx)
        {
            // SS: divide and conquer
            // runtime complexity: O(s + p), because we always advance either s or p.
            if (pIdx == p.Length)
            {
                return sIdx == s.Length;
            }

            bool isMatch;
            
            // Kleene operator?
            if (pIdx <= p.Length - 2 && p[pIdx + 1] == '*')
            {
                // match 0 characters
                var s1 = IsMatch(s, p, sIdx, pIdx + 2);
                if (s1)
                {
                    return true;
                }

                if (sIdx == s.Length)
                {
                    return false;
                }

                // match 1 character
                isMatch = s[sIdx] == p[pIdx] || p[pIdx] == '.';
                var s2 = isMatch && IsMatch(s, p, sIdx + 1, pIdx);
                return s2;
            }

            if (sIdx == s.Length)
            {
                return false;
            }

            // single-character pattern
            isMatch = s[sIdx] == p[pIdx] || p[pIdx] == '.';
            return isMatch && IsMatch(s, p, sIdx + 1, pIdx + 1);
        }


        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1()
            {
                // Arrange
                var s = "aa";
                var p = "a";

                // Act
                var isMatch = new Solution().IsMatch(s, p);

                // Assert
                Assert.False(isMatch);
            }

            [Test]
            public void Test2()
            {
                // Arrange
                var s = "aa";
                var p = "a*";

                // Act
                var isMatch = new Solution().IsMatch(s, p);

                // Assert
                Assert.True(isMatch);
            }

            [Test]
            public void Test3()
            {
                // Arrange
                var s = "ab";
                var p = ".*";

                // Act
                var isMatch = new Solution().IsMatch(s, p);

                // Assert
                Assert.True(isMatch);
            }

            [Test]
            public void Test4()
            {
                // Arrange
                var s = "aab";
                var p = "c*a*b";

                // Act
                var isMatch = new Solution().IsMatch(s, p);

                // Assert
                Assert.True(isMatch);
            }

            [Test]
            public void Test5()
            {
                // Arrange
                var s = "mississippi";
                var p = "mis*is*p*.";

                // Act
                var isMatch = new Solution().IsMatch(s, p);

                // Assert
                Assert.False(isMatch);
            }

            [Test]
            public void Test6()
            {
                // Arrange
                var s = "mississippi";
                var p = "mis*is*ip*.";

                // Act
                var isMatch = new Solution().IsMatch(s, p);

                // Assert
                Assert.True(isMatch);
            }

            [Test]
            public void Test7()
            {
                // Arrange
                var s = "";
                var p = ".*";

                // Act
                var isMatch = new Solution().IsMatch(s, p);

                // Assert
                Assert.True(isMatch);
            }

            [Test]
            public void Test8()
            {
                // Arrange
                var s = "a";
                var p = "";

                // Act
                var isMatch = new Solution().IsMatch(s, p);

                // Assert
                Assert.False(isMatch);
            }

            [Test]
            public void Test9()
            {
                // Arrange
                var s = "ab";
                var p = ".*c";

                // Act
                var isMatch = new Solution().IsMatch(s, p);

                // Assert
                Assert.False(isMatch);
            }
        }
    }
}