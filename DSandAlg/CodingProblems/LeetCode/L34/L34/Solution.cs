#region

using NUnit.Framework;

#endregion

// 34. Find First and Last Position of Element in Sorted Array
// https://leetcode.com/problems/find-first-and-last-position-of-element-in-sorted-array/

namespace L34
{
    public class Solution
    {
        public int[] SearchRange(int[] nums, int target)
        {
            if (nums.Length == 0)
            {
                return new[] {-1, -1};
            }

            var lowerIdx = FindLower(nums, target, 0, nums.Length - 1);
            var higherIdx = FindHigher(nums, target, 0, nums.Length - 1);
            return new[] {lowerIdx, higherIdx};
        }

        private static int FindHigher(int[] nums, int target, int min, int max)
        {
            if (min == max)
            {
                return nums[min] == target ? min : -1;
            }

            var mid = (min + max) / 2;
            var m1 = FindHigher(nums, target, min, mid);
            var m2 = FindHigher(nums, target, mid + 1, max);

            return m2 == -1 ? m1 : m2;
        }

        private static int FindLower(int[] nums, int target, int min, int max)
        {
            if (min == max)
            {
                return nums[min] == target ? min : -1;
            }

            var mid = (min + max) / 2;
            var m1 = FindLower(nums, target, min, mid);
            var m2 = FindLower(nums, target, mid + 1, max);

            return m1 == -1 ? m2 : m1;
        }

        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1()
            {
                // Arrange
                var nums = new[] {5, 7, 7, 8, 8, 10};
                var target = 8;

                // Act
                var result = new Solution().SearchRange(nums, target);

                // Assert
                Assert.AreEqual(3, result[0]);
                Assert.AreEqual(4, result[1]);
            }

            [Test]
            public void Test2()
            {
                // Arrange
                var nums = new[] {5, 7, 7, 8, 8, 10};
                var target = 6;

                // Act
                var result = new Solution().SearchRange(nums, target);

                // Assert
                Assert.AreEqual(-1, result[0]);
                Assert.AreEqual(-1, result[1]);
            }

            [Test]
            public void Test3()
            {
                // Arrange
                var nums = new[] {5, 7, 7, 8, 8, 10};
                var target = 0;

                // Act
                var result = new Solution().SearchRange(nums, target);

                // Assert
                Assert.AreEqual(-1, result[0]);
                Assert.AreEqual(-1, result[1]);
            }
        }
    }
}