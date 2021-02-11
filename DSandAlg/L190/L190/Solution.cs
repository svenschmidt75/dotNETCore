#region

using NUnit.Framework;

#endregion

// Problem: 190. Reverse Bits
// URL: https://leetcode.com/problems/reverse-bits/

namespace LeetCode
{
    public class Solution
    {
        public uint reverseBits(uint n)
        {
            uint result = 0;

            while (n > 0)
            {
                var lsb = n & -n;
                n -= (uint) lsb;

                var bit = 32;
                while (lsb > 0)
                {
                    bit--;
                    lsb >>= 1;
                }

                result |= (uint) (1 << bit);
            }

            return result;
        }

        [TestFixture]
        public class Tests
        {
            [TestCase((uint) 0b00000010100101000001111010011100, (uint) 964176192)]
            [TestCase(0b11111111111111111111111111111101, 3221225471)]
            public void Test1(uint n, uint expected)
            {
                // Arrange

                // Act

                // Assert
                Assert.AreEqual(expected, new Solution().reverseBits(n));
            }
        }
    }
}