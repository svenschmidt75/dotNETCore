using System.Collections.Generic;
using NUnit.Framework;

// Problem: 260. Single Number III
// URL: https://leetcode.com/problems/single-number-iii/

namespace LeetCode
{
    public class Solution
    {
        public int[] SingleNumber(int[] nums)
        {
            // return SingleNumber1(nums);
            return SingleNumber2(nums);
        }

        private int[] SingleNumber2(int[] nums)
        {
            // SS: runtime complexity: O(n)
            // space complexity: O(1)
            int xor = 0;
            
            foreach (var num in nums)
            {
                xor ^= num;
            }

            /* SS: Assume nums = [1, 2, 1, 3, 2, 5]. At this point, all pairs
             *     are cancelled out and xor = 3 ^ 5.
             * For the xor operator, if the bits of x = 3 and y = 5 are the same
             * at a specific position, we get 0, otherwise 1.
             * Thus, LSB(xor) is the 1st bit where x and y differ. We can use that
             * to partition nums into two sets: one with all elements where that
             * bit is set, and one where it is not.
            */
            int lsb = xor & (-xor);
            
            int x = 0;
            int y = 0;

            foreach (var num in nums)
            {
                if ((num & lsb) != 0)
                {
                    // SS: bit set
                    x ^= num;
                }
                else
                {
                    // SS: bit not set
                    y ^= num;
                }
            }
            
            return new[]{x, y};
        }

        private int[] SingleNumber1(int[] nums)
        {
            // SS: runtime complexity: O(n)
            // space complexity: O(n)
            var set = new HashSet<int>();

            foreach (var num in nums)
            {
                if (set.Contains(num))
                {
                    set.Remove(num);
                }
                else
                {
                    set.Add(num);
                }
            }

            var result = new int[2];
            int i = 0;
            foreach (var item in set)
            {
                result[i++] = item;

            }

            return result;
        }

        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1()
            {
                // Arrange
                int[] nums = {1, 2, 1, 3, 2, 5};

                // Act
                int[] result = new Solution().SingleNumber(nums);

                // Assert
                CollectionAssert.AreEquivalent(new[]{3, 5}, result);
            }

            [Test]
            public void Test2()
            {
                // Arrange
                int[] nums = {-1, 0};

                // Act
                int[] result = new Solution().SingleNumber(nums);

                // Assert
                CollectionAssert.AreEquivalent(new[]{-1, 0}, result);
            }

            [Test]
            public void Test3()
            {
                // Arrange
                int[] nums = {0, 1};

                // Act
                int[] result = new Solution().SingleNumber(nums);

                // Assert
                CollectionAssert.AreEquivalent(new[]{0, 1}, result);
            }
            
            [Test]
            public void Test4()
            {
                // Arrange
                int[] nums = {int.MinValue, int.MinValue, int.MaxValue, 1};

                // Act
                int[] result = new Solution().SingleNumber(nums);

                // Assert
                CollectionAssert.AreEquivalent(new[]{int.MaxValue, 1}, result);
            }

        }
    }
}