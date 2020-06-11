#region

using System.Linq;
using NUnit.Framework;

#endregion

// https://leetcode.com/problems/palindrome-permutation/

namespace L266
{
    public class Solution
    {
        public bool Solve1(string input)
        {
            if (string.IsNullOrWhiteSpace(input) || input.Length < 2)
            {
                return false;
            }

            // SS: O(n log n)
            var sorted = input.OrderBy(c => c).ToArray();

            var i = 0;
            var hasOddChar = false;
            while (i <= sorted.Length - 2)
            {
                var c1 = sorted[i];
                var c2 = sorted[i + 1];
                if (c1 != c2)
                {
                    if (hasOddChar)
                    {
                        return false;
                    }

                    hasOddChar = true;
                    i++;
                }
                else
                {
                    i += 2;
                }
            }

            return true;
        }

    }

    [TestFixture]
    public class Tests
    {
        [Test]
        public void Test1()
        {
            // Arrange
            var input = "code";

            // Act
            var result = new Solution().Solve1(input);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void Test2()
        {
            // Arrange
            var input = "aab";

            // Act
            var result = new Solution().Solve1(input);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void Test3()
        {
            // Arrange
            var input = "carerac";

            // Act
            var result = new Solution().Solve1(input);

            // Assert
            Assert.IsTrue(result);
        }
    }
}