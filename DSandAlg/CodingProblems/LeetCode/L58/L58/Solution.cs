#region

using NUnit.Framework;

#endregion

// Problem: 58. Length of Last Word
// URL: https://leetcode.com/problems/length-of-last-word/

namespace LeetCode
{
    public class Solution
    {
        public int LengthOfLastWord(string s)
        {
            return LengthOfLastWordSplit(s);
        }

        public int LengthOfLastWordSplit(string s)
        {
            var words = s.Split(' ');

            var idx = words.Length - 1;
            while (idx >= 0)
            {
                var length = words[idx].Length;
                if (length > 0)
                {
                    return length;
                }

                idx--;
            }

            return 0;
        }

        [TestFixture]
        public class Tests
        {
            [TestCase("a ", 1)]
            [TestCase("a  ", 1)]
            public void Test(string s, int expectedLength)
            {
                // Arrange

                // Act
                var length = new Solution().LengthOfLastWord(s);

                // Assert
                Assert.AreEqual(expectedLength, length);
            }
        }
    }
}