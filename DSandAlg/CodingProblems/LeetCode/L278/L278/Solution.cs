#region

using NUnit.Framework;

#endregion

// 278. First Bad Version
// https://leetcode.com/problems/first-bad-version/

namespace L278
{
    public class Solution : VersionControl
    {
        public int FirstBadVersion(int n)
        {
            // SS: runtime complexity: O(log N)
            var min = 1;
            var max = n;

            while (true)
            {
                if (min == max)
                {
                    return min;
                }

                var mid = min + (max - min) / 2;
                var isBad = IsBadVersion(mid);

                if (isBad)
                {
                    // SS: go left
                    max = mid;
                }
                else
                {
                    // SS: go right
                    min = mid + 1;
                }
            }
        }
    }

    public class VersionControl
    {
        protected virtual bool IsBadVersion(int version)
        {
            return false;
        }
    }

    [TestFixture]
    public class Tests
    {
        private class SolutionTest1 : Solution
        {
            protected override bool IsBadVersion(int version)
            {
                return version >= 1;
            }
        }

        private class SolutionTest2 : Solution
        {
            protected override bool IsBadVersion(int version)
            {
                return version >= 4;
            }
        }

        private class SolutionTest3 : Solution
        {
            protected override bool IsBadVersion(int version)
            {
                return version >= 1702766719;
            }
        }

        [Test]
        public void Test1()
        {
            // Arrange

            // Act
            var result = new SolutionTest1().FirstBadVersion(1);

            // Assert
            Assert.AreEqual(1, result);
        }

        [Test]
        public void Test2()
        {
            // Arrange

            // Act
            var result = new SolutionTest2().FirstBadVersion(5);

            // Assert
            Assert.AreEqual(4, result);
        }

        [Test]
        public void Test3()
        {
            // Arrange

            // Act
            var result = new SolutionTest3().FirstBadVersion(2126753390);

            // Assert
            Assert.AreEqual(1702766719, result);
        }
    }
}