#region

using System.Collections.Generic;
using NUnit.Framework;

#endregion

// 315. Count of Smaller Numbers After Self
// https://leetcode.com/problems/count-of-smaller-numbers-after-self/

namespace L315
{
    public class Solution
    {
        public IList<int> CountSmaller(int[] nums)
        {
            var result = new int[nums.Length];

            for (var i = 0; i < nums.Length; i++)
            {
                var cnt = 0;

                for (var j = i + 1; j < nums.Length; j++)
                {
                    if (nums[j] < nums[i])
                    {
                        cnt++;
                    }
                }

                result[i] = cnt;
            }

            return result;
        }

        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test11()
            {
                // Arrange
                int[] nums = {5, 2, 6, 1};

                // Act
                var result = new Solution().CountSmaller(nums);

                // Assert
                CollectionAssert.AreEqual(new[] {2, 1, 1, 0}, result);
            }
        }
    }
}