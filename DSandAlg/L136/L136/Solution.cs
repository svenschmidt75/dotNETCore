using NUnit.Framework;

// Problem: 136. Single Number
// URL: https://leetcode.com/problems/single-number/

namespace LeetCode
{
    public class Solution
    {
        public int SingleNumber(int[] nums)
        {
            int n = 0;
            for (int i = 0; i < nums.Length; i++)
            {
                n ^= nums[i];
            }

            return n;
        }
        
        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1()
            {
                // Arrange

                // Act

                // Assert
            }
        }
    }
}