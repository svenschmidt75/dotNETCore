#region

using NUnit.Framework;

#endregion

// Problem: 189. Rotate Array
// URL: https://leetcode.com/problems/rotate-array/

namespace LeetCode
{
    public class Solution
    {
        public void Rotate(int[] nums, int k)
        {
            RotateInPlace(nums, k);
        }

        private void RotateInPlace(int[] nums, int k)
        {
            // SS: runtime complexity: O(n)?
            // space complexity: O(1)

            if (nums.Length == 1)
            {
                return;
            }

            var k2 = k % nums.Length;
            if (k2 == 0)
            {
                return;
            }

            // [1, 2, 3, 4, 5, 6, 7], k = 4
            // ^^^^^^^^  ^^^^^^^^^^
            //   tail       front

            var frontIdx = nums.Length - k2;

            var currentTailIdx = 0;
            var currentFrontIdx = frontIdx;

            while (true)
            {
                while (currentTailIdx < frontIdx && currentFrontIdx < nums.Length)
                {
                    Swap(nums, currentFrontIdx, currentTailIdx);
                    currentFrontIdx++;
                    currentTailIdx++;
                }

                if (currentTailIdx == frontIdx && currentFrontIdx == nums.Length)
                {
                    // SS: we are done
                    break;
                }

                // SS: front longer than tail?
                if (currentTailIdx == frontIdx)
                {
                    frontIdx = currentFrontIdx;
                }

                // SS: tail longer than front?
                else if (currentFrontIdx == nums.Length)
                {
                    if (currentTailIdx < frontIdx)
                    {
                        currentFrontIdx = frontIdx;
                    }
                }
            }
        }

        private static void Swap(int[] nums, int i1, int i2)
        {
            var tmp = nums[i1];
            nums[i1] = nums[i2];
            nums[i2] = tmp;
        }

        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1()
            {
                // Arrange
                int[] nums = {1, 2, 3, 4, 5, 6, 7};
                var k = 3;

                // Act
                new Solution().Rotate(nums, k);

                // Assert
                CollectionAssert.AreEqual(new[] {5, 6, 7, 1, 2, 3, 4}, nums);
            }

            [Test]
            public void Test2()
            {
                // Arrange
                int[] nums = {-1, -100, 3, 99};
                var k = 2;

                // Act
                new Solution().Rotate(nums, k);

                // Assert
                CollectionAssert.AreEqual(new[] {3, 99, -1, -100}, nums);
            }

            [Test]
            public void Test3()
            {
                // Arrange
                int[] nums = {-1, -100};
                var k = 1;

                // Act
                new Solution().Rotate(nums, k);

                // Assert
                CollectionAssert.AreEqual(new[] {-100, -1}, nums);
            }

            [Test]
            public void Test4()
            {
                // Arrange
                int[] nums = {1, 2, 3, 4, 5, 6, 7};
                var k = 2;

                // Act
                new Solution().Rotate(nums, k);

                // Assert
                CollectionAssert.AreEqual(new[] {6, 7, 1, 2, 3, 4, 5}, nums);
            }

            [Test]
            public void Test5()
            {
                // Arrange
                int[] nums = {1, 2, 3, 4, 5, 6, 7};
                var k = 4;

                // Act
                new Solution().Rotate(nums, k);

                // Assert
                CollectionAssert.AreEqual(new[] {4, 5, 6, 7, 1, 2, 3}, nums);
            }

            [Test]
            public void Test6()
            {
                // Arrange
                int[] nums = {1, 2, 3, 4, 5, 6, 7};
                var k = 6;

                // Act
                new Solution().Rotate(nums, k);

                // Assert
                CollectionAssert.AreEqual(new[] {2, 3, 4, 5, 6, 7, 1}, nums);
            }
        }
    }
}