#region

using NUnit.Framework;

#endregion

// https://leetcode.com/discuss/interview-question/398026/

namespace MinMovesObtainStringWithout3IdenticalConsecutiveLetters
{
    public class Solution
    {
        public int MinSwaps(string input)
        {
            if (input.Length < 3)
            {
                return 0;
            }

            var nSwaps = 0;

            var prevChar = input[0];

            var i = 0;
            var j = 1;
            int width;

            while (j < input.Length)
            {
                var c = input[j];
                if (c == prevChar)
                {
                    j++;
                    continue;
                }

                width = j - i;
                while (width >= 3)
                {
                    nSwaps++;
                    i += 2;

                    if (width >= 4)
                    {
                        i++;
                    }

                    width = j - i;
                }

                prevChar = c;
                i = j;
                j++;
            }

            // SS: handle remainder
            width = j - i;
            while (width >= 3)
            {
                nSwaps++;
                i += 2;

                if (width >= 4)
                {
                    i++;
                }

                width = j - i;
            }

            return nSwaps;
        }

        [TestFixture]
        public class Test
        {
            [Test]
            public void Test1()
            {
                // Arrange
                var input = "";

                // Act
                var nSwaps = new Solution().MinSwaps(input);

                // Assert
                Assert.AreEqual(0, nSwaps);
            }

            [Test]
            public void Test2()
            {
                // Arrange
                var input = "baaaaa";

                // Act
                var nSwaps = new Solution().MinSwaps(input);

                // Assert
                Assert.AreEqual(1, nSwaps);
            }

            [Test]
            public void Test3()
            {
                // Arrange
                var input = "baaabbaabbba";

                // Act
                var nSwaps = new Solution().MinSwaps(input);

                // Assert
                Assert.AreEqual(2, nSwaps);
            }

            [Test]
            public void Test4()
            {
                // Arrange
                var input = "baabab";

                // Act
                var nSwaps = new Solution().MinSwaps(input);

                // Assert
                Assert.AreEqual(0, nSwaps);
            }
        }
    }
}