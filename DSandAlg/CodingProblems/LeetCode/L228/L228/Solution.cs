#region

using System.Collections.Generic;
using NUnit.Framework;

#endregion

// Problem: 228. Summary Ranges
// URL: https://leetcode.com/problems/summary-ranges/

namespace LeetCode
{
    public class Solution
    {
        public IList<string> SummaryRanges(int[] nums)
        {
            // SS: runtime complexity: O(n)
            // space complexity: O(1)
            
            var output = new List<string>();

            var i = 0;
            while (i < nums.Length)
            {
                var j = i + 1;
                while (j < nums.Length && nums[j - 1] + 1 == nums[j])
                {
                    j++;
                }

                if (i + 1 == j)
                {
                    output.Add($"{nums[i]}");
                }
                else
                {
                    output.Add($"{nums[i]}->{nums[j - 1]}");
                }

                i = j;
            }

            return output;
        }

        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1()
            {
                // Arrange
                int[] nums = {0, 1, 2, 4, 5, 7};

                // Act
                var output = new Solution().SummaryRanges(nums);

                // Assert
                CollectionAssert.AreEqual(new[] {"0->2", "4->5", "7"}, output);
            }

            [Test]
            public void Test2()
            {
                // Arrange
                int[] nums = {0, 2, 3, 4, 6, 8, 9};

                // Act
                var output = new Solution().SummaryRanges(nums);

                // Assert
                CollectionAssert.AreEqual(new[] {"0", "2->4", "6", "8->9"}, output);
            }

            [Test]
            public void Test3()
            {
                // Arrange
                int[] nums = { };

                // Act
                var output = new Solution().SummaryRanges(nums);

                // Assert
                Assert.IsEmpty(output);
            }

            [Test]
            public void Test4()
            {
                // Arrange
                int[] nums = {-1};

                // Act
                var output = new Solution().SummaryRanges(nums);

                // Assert
                CollectionAssert.AreEqual(new[] {"-1"}, output);
            }

            [Test]
            public void Test5()
            {
                // Arrange
                int[] nums = {0};

                // Act
                var output = new Solution().SummaryRanges(nums);

                // Assert
                CollectionAssert.AreEqual(new[] {"0"}, output);
            }
        }
    }
}