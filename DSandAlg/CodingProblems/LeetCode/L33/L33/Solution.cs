#region

using NUnit.Framework;

#endregion

// Problem: 33. Search in Rotated Sorted Array
// URL: https://leetcode.com/problems/search-in-rotated-sorted-array/

namespace LeetCode33
{
    public class Solution
    {
        public int Search(int[] nums, int target)
        {
            // SS: runtime complexity: O(log N)

            if (nums.Length == 1)
            {
                return nums[0] == target ? 0 : -1;
            }

            var pivotIndex = FindPivot(nums);

            int lowerIdx;
            int upperIdx;

            if (target < nums[0] && pivotIndex < nums.Length - 1)
            {
                lowerIdx = pivotIndex + 1;
                upperIdx = nums.Length - 1;
            }
            else
            {
                lowerIdx = 0;
                upperIdx = pivotIndex;
            }

            var index = LowerBound(nums, lowerIdx, upperIdx, target);

            return nums[index] == target ? index : -1;
        }

        private int LowerBound(int[] nums, int i1, int i2, int target)
        {
            // SS: BS in nums from [i1, i2] for target
            var min = i1;
            var max = i2;

            while (min < max)
            {
                var mid = min + (max - min) / 2;
                if (nums[mid] < target)
                {
                    min = mid + 1;
                }
                else
                {
                    max = mid;
                }
            }

            return min;
        }

        private int FindPivot(int[] nums)
        {
            // SS: return pivot index, i.e.
            // nums = [4,5,6,7,0,1,2] -> pivot index = 3

            // SS: if the entire array is sorted, i.e. no rotation,
            // the last index is the pivot index
            if (nums[0] < nums[^1])
            {
                return nums.Length - 1;
            }

            var min = 0;
            var max = nums.Length - 1;

            while (min < max)
            {
                var mid = min + (max - min) / 2;
                if (nums[min] >= nums[mid])
                {
                    max = mid;
                }
                else
                {
                    min = mid;
                }
            }

            return min;
        }


        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1()
            {
                // Arrange
                int[] nums = {4, 5, 6, 7, 0, 1, 2};

                // Act
                var index = new Solution().Search(nums, 0);

                // Assert
                Assert.AreEqual(4, index);
            }

            [Test]
            public void Test2()
            {
                // Arrange
                int[] nums = {4, 5, 6, 7, 0, 1, 2};

                // Act
                var index = new Solution().Search(nums, 3);

                // Assert
                Assert.AreEqual(-1, index);
            }

            [Test]
            public void Test3()
            {
                // Arrange
                int[] nums = {1};

                // Act
                var index = new Solution().Search(nums, 0);

                // Assert
                Assert.AreEqual(-1, index);
            }

            [Test]
            public void Test4()
            {
                // Arrange
                int[] nums = {1, 3};

                // Act
                var index = new Solution().Search(nums, 1);

                // Assert
                Assert.AreEqual(0, index);
            }

            [Test]
            public void Test5()
            {
                // Arrange
                int[] nums = {1, 3};

                // Act
                var index = new Solution().Search(nums, 3);

                // Assert
                Assert.AreEqual(1, index);
            }

            [Test]
            public void Test6()
            {
                // Arrange
                int[] nums = {1, 3};

                // Act
                var index = new Solution().Search(nums, 0);

                // Assert
                Assert.AreEqual(-1, index);
            }
        }
    }
}