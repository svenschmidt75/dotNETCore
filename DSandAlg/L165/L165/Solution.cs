#region

using NUnit.Framework;

#endregion

// Problem: 165. Compare Version Numbers
// URL: https://leetcode.com/problems/compare-version-numbers/

namespace LeetCode
{
    public class Solution
    {
        public int CompareVersion(string version1, string version2)
        {
            // SS: runtime complexity: O(max(version1, version2))
            // space complexity: O(1)

            var idx1 = 0;
            var idx2 = 0;

            while (idx1 < version1.Length || idx2 < version2.Length)
            {
                int rev1;
                (idx1, rev1) = ReadRevision(version1, idx1);

                int rev2;
                (idx2, rev2) = ReadRevision(version2, idx2);

                if (rev1 < rev2)
                {
                    return -1;
                }

                if (rev1 > rev2)
                {
                    return 1;
                }
            }

            return 0;
        }

        private (int idx, int rev) ReadRevision(string version, int idx)
        {
            if (idx == version.Length)
            {
                return (idx, 0);
            }

            var a = idx;
            var b = a;
            while (b < version.Length && version[b] != '.')
            {
                b++;
            }

            var rev = int.Parse(version[a..b]);

            if (b < version.Length && version[b] == '.')
            {
                b++;
            }

            return (b, rev);
        }

        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1()
            {
                // Arrange
                var version1 = "1.01";
                var version2 = "1.001";

                // Act
                var cmp = new Solution().CompareVersion(version1, version2);

                // Assert
                Assert.AreEqual(0, cmp);
            }

            [Test]
            public void Test2()
            {
                // Arrange
                var version1 = "1.0";
                var version2 = "1.0.0";

                // Act
                var cmp = new Solution().CompareVersion(version1, version2);

                // Assert
                Assert.AreEqual(0, cmp);
            }

            [Test]
            public void Test3()
            {
                // Arrange
                var version1 = "0.1";
                var version2 = "1.1";

                // Act
                var cmp = new Solution().CompareVersion(version1, version2);

                // Assert
                Assert.AreEqual(-1, cmp);
            }

            [Test]
            public void Test4()
            {
                // Arrange
                var version1 = "1.0.1";
                var version2 = "1";

                // Act
                var cmp = new Solution().CompareVersion(version1, version2);

                // Assert
                Assert.AreEqual(1, cmp);
            }

            [Test]
            public void Test5()
            {
                // Arrange
                var version1 = "7.5.2.4";
                var version2 = "7.5.3";

                // Act
                var cmp = new Solution().CompareVersion(version1, version2);

                // Assert
                Assert.AreEqual(-1, cmp);
            }
        }
    }
}