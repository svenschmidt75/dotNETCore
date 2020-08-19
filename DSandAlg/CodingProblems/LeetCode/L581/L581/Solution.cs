#region

using System;
using NUnit.Framework;

#endregion

namespace L581
{
    public class Solution
    {
        public int FindUnsortedSubarray1(int[] nums)
        {
            // SS: runtime complexity: O(N log N)
            // space complexity: O(N)

            if (nums.Length == 0)
            {
                return 0;
            }

            var copy = new int[nums.Length];
            Array.Copy(nums, copy, nums.Length);
            Array.Sort(copy);

            var left = 0;
            while (left < nums.Length && nums[left] == copy[left])
            {
                left++;
            }

            if (left == nums.Length)
            {
                // SS: array is already sorted
                return 0;
            }

            var right = nums.Length - 1;
            while (right > left && nums[right] == copy[right])
            {
                right--;
            }

            var windowWidth = right - left + 1;
            return windowWidth;
        }

        public int FindUnsortedSubarray2(int[] nums)
        {
            // SS: runtime complexity: O(N)
            // space complexity: O(1)

            if (nums.Length == 0)
            {
                return 0;
            }

            var i = 0;
            while (i + 1 < nums.Length && nums[i] <= nums[i + 1])
            {
                i++;
            }

            if (i == nums.Length - 1)
            {
                // SS: array is sorted
                return 0;
            }

            var min = nums[i];
            var max = nums[i];

            var upperIndex = i;
            var k = upperIndex;
            while (k < nums.Length)
            {
                var v = nums[k];

                min = Math.Min(min, v);
                max = Math.Max(max, v);

                // SS: if the current element is smaller than the maximum element
                // we have seen so far, we need to grow the window
                if (v < max)
                {
                    upperIndex = k;
                }
                else if (k + 1 < nums.Length && v > nums[k + 1])
                {
                    // SS: wrong order, so keep growing window
                    upperIndex = k;
                }

                k++;
            }

            // SS: grow window to the left to find the 1st item less than nums[i]
            var lowerIndex = i;
            while (lowerIndex > 0 && nums[lowerIndex - 1] > min)
            {
                lowerIndex--;
            }

            var windowWidth = upperIndex - lowerIndex + 1;
            return windowWidth;
        }

        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test11()
            {
                // Arrange
                var nums = new[] {1, 2, 3, 7, 5, 6, 4, 8};

                // Act
                var result = new Solution().FindUnsortedSubarray1(nums);

                // Act
                Assert.AreEqual(4, result);
            }

            [Test]
            public void Test12()
            {
                // Arrange
                var nums = new[] {1, 2, 3, 7, 5, 6, 4, 8};

                // Act
                var result = new Solution().FindUnsortedSubarray2(nums);

                // Act
                Assert.AreEqual(4, result);
            }

            [Test]
            public void Test21()
            {
                // Arrange
                var nums = new[] {1, 3, 2, 7, 5, 6, 4, 8};

                // Act
                var result = new Solution().FindUnsortedSubarray1(nums);

                // Act
                Assert.AreEqual(6, result);
            }

            [Test]
            public void Test22()
            {
                // Arrange
                var nums = new[] {1, 3, 2, 7, 5, 6, 4, 8};

                // Act
                var result = new Solution().FindUnsortedSubarray2(nums);

                // Act
                Assert.AreEqual(6, result);
            }

            [Test]
            public void Test31()
            {
                // Arrange
                var nums = new[] {3, 4, 5, 2, 6};

                // Act
                var result = new Solution().FindUnsortedSubarray1(nums);

                // Act
                Assert.AreEqual(4, result);
            }

            [Test]
            public void Test32()
            {
                // Arrange
                var nums = new[] {3, 4, 5, 2, 6};

                // Act
                var result = new Solution().FindUnsortedSubarray2(nums);

                // Act
                Assert.AreEqual(4, result);
            }

            [Test]
            public void Test41()
            {
                // Arrange
                var nums = new[] {2, 6, 4, 8, 10, 9, 15};

                // Act
                var result = new Solution().FindUnsortedSubarray1(nums);

                // Act
                Assert.AreEqual(5, result);
            }

            [Test]
            public void Test42()
            {
                // Arrange
                var nums = new[] {2, 6, 4, 8, 10, 9, 15};

                // Act
                var result = new Solution().FindUnsortedSubarray2(nums);

                // Act
                Assert.AreEqual(5, result);
            }

            [Test]
            public void Test51()
            {
                // Arrange
                var nums = new[] {2, 6, 4, 15, 8, 9, 10};

                // Act
                var result = new Solution().FindUnsortedSubarray1(nums);

                // Act
                Assert.AreEqual(6, result);
            }

            [Test]
            public void Test52()
            {
                // Arrange
                var nums = new[] {2, 6, 4, 15, 8, 9, 10};

                // Act
                var result = new Solution().FindUnsortedSubarray2(nums);

                // Act
                Assert.AreEqual(6, result);
            }

            [Test]
            public void Test61()
            {
                // Arrange
                var nums = new[] {2, 5, 9, 3, 1, 15, 8, 6, 9};

                // Act
                var result = new Solution().FindUnsortedSubarray1(nums);

                // Act
                Assert.AreEqual(9, result);
            }

            [Test]
            public void Test62()
            {
                // Arrange
                var nums = new[] {2, 5, 9, 3, 1, 15, 8, 6, 9};

                // Act
                var result = new Solution().FindUnsortedSubarray2(nums);

                // Act
                Assert.AreEqual(9, result);
            }

            [Test]
            public void Test72()
            {
                // Arrange
                var nums = new[] {1, 2, 3, 3, 3};

                // Act
                var result = new Solution().FindUnsortedSubarray2(nums);

                // Act
                Assert.AreEqual(0, result);
            }

            [Test]
            public void Test82()
            {
                // Arrange
                var nums = new[] {1, 3, 2, 3, 3};

                // Act
                var result = new Solution().FindUnsortedSubarray2(nums);

                // Act
                Assert.AreEqual(2, result);
            }

            [Test]
            public void Test92()
            {
                // Arrange
                var nums = new[] {2, 3, 3, 2, 4};

                // Act
                var result = new Solution().FindUnsortedSubarray2(nums);

                // Act
                Assert.AreEqual(3, result);
            }
        }
    }
}