#region

using System;
using NUnit.Framework;

#endregion

// Problem: 31. Next Permutation
// URL: https://leetcode.com/problems/next-permutation/

namespace LeetCode31
{
    public class Solution
    {
        public void NextPermutation(int[] nums)
        {
            NextPermutation3(nums);
        }

        public void NextPermutation3(int[] nums)
        {
            // SS: runtime complexity: O(N)
            if (nums.Length == 1)
            {
                return;
            }

            // SS: from the right, find index of first number that is greater
            var i = nums.Length - 2;
            while (i >= 0 && nums[i] >= nums[i + 1])
            {
                i--;
            }

            if (i == -1)
            {
                // SS: is already max permutation, i.e. the array is sorted in
                // decreasing order. Reverse to sort in increasing order.
                Reverse(nums, 0);
            }
            else
            {
                // SS: The array from nums[index + 1..] is sorted in decreasing order!

                // from the right, we need to find the first number that is larger than
                // nums[index]
                var j = nums.Length - 1;
                while (j > i && nums[j] <= nums[i])
                {
                    j--;
                }

                // swap the two numbers
                var tmp = nums[j];
                nums[j] = nums[i];
                nums[i] = tmp;

                // remember, nums is still sorted in decreasing order, even after the swap
                Reverse(nums, i + 1);
            }
        }

        private void Reverse(int[] nums, int startIndex)
        {
            var i = startIndex;
            var j = nums.Length - 1;
            while (i < j)
            {
                var tmp = nums[i];
                nums[i] = nums[j];
                nums[j] = tmp;

                i++;
                j--;
            }
        }

        public void NextPermutation2(int[] nums)
        {
            // SS: runtime complexity: O(N log N)

            if (nums.Length == 1)
            {
                return;
            }

            // SS: from the right, find index of first number that is greater 
            var i = nums.Length - 2;
            while (i >= 0 && nums[i] >= nums[i + 1])
            {
                i--;
            }

            if (i == -1)
            {
                // SS: is already max permutation, sort array in increasing order
                var j = 0;
                var k = nums.Length - 1;
                while (j < k)
                {
                    var v1 = nums[j];
                    var v2 = nums[k];

                    nums[j] = v2;
                    nums[k] = v1;

                    j++;
                    k--;
                }
            }
            else
            {
                var vFound = int.MaxValue;
                var vFoundIdx = -1;

                var v = nums[i];

                // SS: find next bigger number
                for (var j = i + 1; j < nums.Length; j++)
                {
                    var v2 = nums[j];
                    if (v2 > v && v2 < vFound)
                    {
                        vFound = v2;
                        vFoundIdx = j;
                    }
                }

                nums[vFoundIdx] = v;
                nums[i] = vFound;

                // sort, O(N log N)
                Array.Sort(nums, i + 1, nums.Length - i - 1);
            }
        }

        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test10()
            {
                // Arrange
                int[] nums = {7, 4, 3, 9, 7, 2, 1};

                // Act
                new Solution().NextPermutation(nums);

                // Assert
                CollectionAssert.AreEqual(new[] {7, 4, 7, 1, 2, 3, 9}, nums);
            }

            [Test]
            public void Test9()
            {
                // Arrange
                int[] nums = {3, 2, 1};

                // Act
                new Solution().NextPermutation(nums);

                // Assert
                CollectionAssert.AreEqual(new[] {1, 2, 3}, nums);
            }

            [Test]
            public void Test8()
            {
                // Arrange
                int[] nums = {1, 1, 5};

                // Act
                new Solution().NextPermutation(nums);

                // Assert
                CollectionAssert.AreEqual(new[] {1, 5, 1}, nums);
            }

            [Test]
            public void Test7()
            {
                // Arrange
                int[] nums = {1};

                // Act
                new Solution().NextPermutation(nums);

                // Assert
                CollectionAssert.AreEqual(new[] {1}, nums);
            }

            [Test]
            public void Test6()
            {
                // Arrange
                int[] nums = {6, 4, 9, 1, 5, 4, 2, 8, 2, 4};

                // Act
                new Solution().NextPermutation(nums);

                // Assert
                CollectionAssert.AreEqual(new[] {6, 4, 9, 1, 5, 4, 2, 8, 4, 2}, nums);
            }

            [Test]
            public void Test5()
            {
                // Arrange
                int[] nums = {7, 4, 3, 9, 1};

                // Act
                new Solution().NextPermutation(nums);

                // Assert
                CollectionAssert.AreEqual(new[] {7, 4, 9, 1, 3}, nums);
            }

            [Test]
            public void Test4()
            {
                // Arrange
                int[] nums = {1, 2, 3};

                // Act
                new Solution().NextPermutation(nums);

                // Assert
                CollectionAssert.AreEqual(new[] {1, 3, 2}, nums);
            }

            [Test]
            public void Test3()
            {
                // Arrange
                int[] nums = {1, 3, 2};

                // Act
                new Solution().NextPermutation(nums);

                // Assert
                CollectionAssert.AreEqual(new[] {2, 1, 3}, nums);
            }

            [Test]
            public void Test2()
            {
                // Arrange
                int[] nums = {1, 1};

                // Act
                new Solution().NextPermutation(nums);

                // Assert
                CollectionAssert.AreEqual(new[] {1, 1}, nums);
            }

            [Test]
            public void Test1()
            {
                // Arrange
                int[] nums = {2, 4, 3, 2};

                // Act
                new Solution().NextPermutation(nums);

                // Assert
                CollectionAssert.AreEqual(new[] {3, 2, 2, 4}, nums);
            }
        }
    }
}