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
            /* SS: runtime complexity: For each char, we so a linear search to find the char from the other end,
             * which is O(N). We then swap, which is again O(N), so O(2N). But, it is not guaranteed that we can
             * move on to the next char, for example:
             * 0 1 2 3 4
             * m a m a d
             *
             * i=0 and j=4. Then, find char 'm' from the right, O(N). idx=2
             * Now swap twice, so
             * 0 1 2 3 4
             * m a m a d
             * Since at j=4 we have 'd', we have to do so again.
             * Hence, for each char, we have to do up to O(N^2) swaps.
             * We do this for each char, so O(N^3) in total.
             *
             * Turns out we don't have to do the linear searches. Instead, we just swap until the char is
             * where we expect it to be. So total runtime is then O(N^2).
             *
             * Space complexity: O(N)
            */

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
                var idx = FindCharFromRight(str, j - 1, c1);
                if (idx == -1 || idx == i)
                {
                    // SS: only one char c1 is in the string, so odd-sized palindrome
                    idx = FindCharFromLeft(str, i + 1, c2);
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

        private int FindCharFromLeft(char[] str, int start, char c)
        {
            for (var i = start; i < str.Length; i++)
            {
                if (str[i] == c)
                {
                    return i;
                }
            }

            return -1;
        }

        private int FindCharFromRight(char[] str, int start, char c)
        {
            for (var i = start; i >= 0; i--)
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

            [Test]
            public void Test6()
            {
                // Arrange
                var input = "aaadd";

                // Act
                var nSwaps = new Solution().Solve(input);

                // Assert
                Assert.AreEqual(3, nSwaps);
            }
        }
    }
}