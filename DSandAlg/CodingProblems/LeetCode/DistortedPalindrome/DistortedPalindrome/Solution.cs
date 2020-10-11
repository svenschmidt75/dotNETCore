#region

using NUnit.Framework;

#endregion

// https://leetcode.com/discuss/interview-question/351783/
// https://www.codechef.com/problems/ENCD12

namespace DistortedPalindrome
{
    public class Solution
    {
        public int Solve(string input)
        {
            if (input.Length <= 1)
            {
                return 0;
            }

            var str = input.ToCharArray();

            var i = 0;
            var j = input.Length - 1;

            var nSwaps = 0;

            while (i < j)
            {
                var c1 = str[i];
                var c2 = str[j];

                if (c1 == c2)
                {
                    i++;
                    j--;
                    continue;
                }

                // SS: try to find char 'c1' from the right
                var idx = FindCharFromRight(str, c1);
                if (idx == -1 || idx == i)
                {
                    // SS: only one char c1 is in the string, so odd-sized palindrome
                    idx = FindCharFromLeft(str, c2);
                    if (idx == -1 || idx == j)
                    {
                        // SS: not a palindrome
                        return -1;
                    }

                    // SS: swap to move char c1 to position idx
                    var k = i;
                    while (k <= idx - 1)
                    {
                        var tmp = str[k];
                        str[k] = str[k + 1];
                        str[k + 1] = tmp;
                        k++;
                        nSwaps++;
                    }
                }
                else
                {
                    // SS: swap to move char c2 to position idx
                    var k = idx;
                    while (k <= j - 1)
                    {
                        var tmp = str[k];
                        str[k] = str[k + 1];
                        str[k + 1] = tmp;
                        k++;
                        nSwaps++;
                    }
                }
            }

            return nSwaps;
        }

        private int FindCharFromLeft(char[] str, char c)
        {
            for (var i = 0; i < str.Length; i++)
            {
                if (str[i] == c)
                {
                    return i;
                }
            }

            return -1;
        }

        private int FindCharFromRight(char[] str, char c)
        {
            for (var i = str.Length - 1; i >= 0; i--)
            {
                if (str[i] == c)
                {
                    return i;
                }
            }

            return -1;
        }

        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1()
            {
                // Arrange
                var input = "mamad";

                // Act
                var nSwaps = new Solution().Solve(input);

                // Assert
                Assert.AreEqual(3, nSwaps);
            }

            [Test]
            public void Test2()
            {
                // Arrange
                var input = "asflkj";

                // Act
                var nSwaps = new Solution().Solve(input);

                // Assert
                Assert.AreEqual(-1, nSwaps);
            }

            [Test]
            public void Test3()
            {
                // Arrange
                var input = "aabb";

                // Act
                var nSwaps = new Solution().Solve(input);

                // Assert
                Assert.AreEqual(2, nSwaps);
            }

            [Test]
            public void Test4()
            {
                // Arrange
                var input = "ntiin";

                // Act
                var nSwaps = new Solution().Solve(input);

                // Assert
                Assert.AreEqual(1, nSwaps);
            }

            [Test]
            public void Test5()
            {
                // Arrange
                var input = "dmaam";

                // Act
                var nSwaps = new Solution().Solve(input);

                // Assert
                Assert.AreEqual(2, nSwaps);
            }
        }
    }
}