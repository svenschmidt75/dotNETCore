#region

using NUnit.Framework;

#endregion

// Problem: 35. Search Insert Position
// URL: https://leetcode.com/problems/search-insert-position/

namespace LeetCode35
{
    public class Solution
    {
        public int SearchInsert(int[] nums, int target)
        {
            // SS: binary search, so O(log N)

            var min = 0;
            var max = nums.Length;

            while (min < max)
            {
                var mid = min + (max - min) / 2;

                if (nums[mid] == target)
                {
                    return mid;
                }

                if (nums[mid] > target)
                {
                    // SS: left subtree
                    if (max == mid)
                    {
                        break;
                    }

                    max = mid;
                }
                else
                {
                    // SS: right subtree
                    if (min == mid)
                    {
                        break;
                    }

                    min = mid;
                }
            }

            return nums[min] < target ? min + 1 : min;
        }

        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1()
            {
                // Arrange
                int[] nums = {1, 3, 5, 6};

                // Act
                var idx = new Solution().SearchInsert(nums, 5);

                // Assert
                Assert.AreEqual(2, idx);
            }

            [Test]
            public void Test2()
            {
                // Arrange
                int[] nums = {1, 3, 5, 6};

                // Act
                var idx = new Solution().SearchInsert(nums, 2);

                // Assert
                Assert.AreEqual(1, idx);
            }

            [Test]
            public void Test3()
            {
                // Arrange
                int[] nums = {1, 3, 5, 6};

                // Act
                var idx = new Solution().SearchInsert(nums, 7);

                // Assert
                Assert.AreEqual(4, idx);
            }

            [Test]
            public void Test4()
            {
                // Arrange
                int[] nums = {1, 3, 5, 6};

                // Act
                var idx = new Solution().SearchInsert(nums, 0);

                // Assert
                Assert.AreEqual(0, idx);
            }

            [Test]
            public void Test5()
            {
                // Arrange
                int[] nums = {1};

                // Act
                var idx = new Solution().SearchInsert(nums, 0);

                // Assert
                Assert.AreEqual(0, idx);
            }
        }
    }
}