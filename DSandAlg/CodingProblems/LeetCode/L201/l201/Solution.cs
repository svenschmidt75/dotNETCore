#region

using NUnit.Framework;

#endregion

// Problem: 201. Bitwise AND of Numbers Range
// URL: https://leetcode.com/problems/bitwise-and-of-numbers-range/

namespace LeetCode
{
    public class Solution
    {
        public int RangeBitwiseAnd(int left, int right)
        {
//            return RangeBitwiseAnd1(left, right);
            return RangeBitwiseAnd2(left, right);
        }

        public int RangeBitwiseAnd1(int left, int right)
        {
            // SS: Find the most significant bit position where
            // left and right differ. All more significant bits
            // of right are the answer.
            // runtime complexity: O(1), i.e. 32bits
            // space complexity: O(1)

            if (left == right)
            {
                return left;
            }

            var v1 = left;
            var v2 = right;
            var i = 0;
            var pos = 0;
            var mask = int.MaxValue;
            while (v2 > 0)
            {
                // SS: LSB set?
                var bit1 = v1 & 1;
                var bit2 = v2 & 1;
                if (bit1 != bit2)
                {
                    pos = i;
                }

                i++;
                v1 /= 2;
                v2 /= 2;
            }

            pos++;
            mask >>= pos;
            mask <<= pos;
            var result = right & mask;

            return result;
        }

        public int RangeBitwiseAnd2(int left, int right)
        {
            // SS: slightly more efficient

            // SS: find MSB (most significant bit)
            var bitPos = left ^ right;
            var pos = 0;
            while (bitPos > 0)
            {
                pos++;
                bitPos /= 2;
            }

            var mask = int.MaxValue >> pos;
            mask <<= pos;
            var result = right & mask;

            return result;
        }

        [TestFixture]
        public class Tests
        {
            [TestCase(5, 7, 4)]
            [TestCase(17, 29, 16)]
            [TestCase(45, 53, 32)]
            [TestCase(1, int.MaxValue, 0)]
            [TestCase(12013, 12269, 11776)]
            [TestCase(0, 0, 0)]
            [TestCase(3, 4, 0)]
            public void Test1(int left, int right, int expected)
            {
                // Arrange

                // Act
                var value = new Solution().RangeBitwiseAnd(left, right);

                // Assert
                Assert.AreEqual(expected, value);
            }
        }
    }
}