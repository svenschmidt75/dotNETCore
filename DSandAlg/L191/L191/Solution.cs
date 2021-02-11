using NUnit.Framework;

// Problem: 191. Number of 1 Bits
// URL: https://leetcode.com/problems/number-of-1-bits/

namespace LeetCode
{
    public class Solution
    {
        public int HammingWeight(uint n)
        {
//            return HammingWeight1(n);
            return HammingWeightLSB(n);
        }

        private int HammingWeightLSB(uint n)
        {
            // SS: keep clearing the LSB while u > 0
            int nBitsSet = 0;

            while (n > 0)
            {
                nBitsSet++;

                // SS: clear least significant bit
                int lsb = (int)n & (- (int)n);
                uint a = (uint)(n & ~lsb);
                n = a;
            }

            return nBitsSet;
        }

        private int HammingWeight1(uint n)
        {
            const int nBits = sizeof(uint) * 8;

            int nBitsSet = 0;
            
            for (int i = 0; i < nBits; i++)
            {
                // SS: lowest  bit set?
                if ((n & 1) > 0)
                {
                    nBitsSet++;
                }

                n >>= 1;
            }

            return nBitsSet;
        }

        [TestFixture]
        public class Tests
        {
            [TestCase((uint)0b1011, 3)]
            [TestCase((uint)0b10000000, 1)]
            [TestCase((uint)0b11111111111111111111111111111101, 31)]
            public void Test1(uint n, int expectedBitsSet)
            {
                // Arrange

                // Act

                // Assert
                Assert.AreEqual(expectedBitsSet, new Solution().HammingWeight(n));
            }
        }
    }
}