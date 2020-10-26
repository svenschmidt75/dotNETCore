#region

using NUnit.Framework;

#endregion

// 724. Find Pivot Index
// https://leetcode.com/problems/find-pivot-index/

namespace L724
{
    public class Solution
    {
        public int PivotIndex(int[] nums)
        {
            // SS: runtime complexity: O(N)
            var prefixSum = new int[nums.Length + 1];
            prefixSum[0] = 0;
            for (var i = 0; i < nums.Length; i++)
            {
                prefixSum[i + 1] = prefixSum[i] + nums[i];
            }

            int left;
            int right;
            for (var i = 0; i < nums.Length; i++)
            {
                left = prefixSum[i];
                right = prefixSum[^1] - prefixSum[i + 1];

                if (left == right)
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
                int[] nums = {1, 7, 3, 6, 5, 6};

                // Act
                var pivotIndex = new Solution().PivotIndex(nums);

                // Assert
                Assert.AreEqual(3, pivotIndex);
            }

            [Test]
            public void Test2()
            {
                // Arrange
                int[] nums = {1, 2, 3};

                // Act
                var pivotIndex = new Solution().PivotIndex(nums);

                // Assert
                Assert.AreEqual(-1, pivotIndex);
            }

            [Test]
            public void Test3()
            {
                // Arrange
                int[] nums = {-1, -1, -1, 0, 1, 1};

                // Act
                var pivotIndex = new Solution().PivotIndex(nums);

                // Assert
                Assert.AreEqual(0, pivotIndex);
            }
        }
    }
}