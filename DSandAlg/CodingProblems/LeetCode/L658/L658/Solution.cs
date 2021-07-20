#region

using System;
using System.Collections.Generic;
using NUnit.Framework;

#endregion

// Problem: 658. Find K Closest Elements
// URL: https://leetcode.com/problems/find-k-closest-elements/

namespace LeetCode
{
    public class Solution
    {
        public IList<int> FindClosestElements(int[] arr, int k, int x)
        {
            // return FindClosestElements1(arr, k, x);
            return FindClosestElements2(arr, k, x);
        }

        private IList<int> FindClosestElements1(int[] arr, int k, int x)
        {
            // SS: runtime complexity: O(log n) for BS, O(k log k) to sort,
            // for a total of: O(log n + k log k)
            // space complexity: O(1)

            // SS: find x in arr, or lower bound
            int min = 0;
            int max = arr.Length;
            while (min < max)
            {
                int mid = min + (max - min) / 2;

                int v2 = arr[mid];
                if (v2 == x)
                {
                    min = mid;
                    break;
                }

                if (x < v2)
                {
                    max = mid;
                }
                else
                {
                    min = mid + 1;
                }
            }

            int[] result = new int[k];

            // SS: take first k values around x according to the
            // constraints
            int i1 = min;
            int i2 = min;

            if (min < arr.Length && arr[i1] == x)
            {
                // SS: x is actually in the array
                i2++;
            }
            else
            {
                // SS: x is in between two values, so min points to the upper bound
                i1--;
            }

            int kCount = 0;
            while (kCount < k)
            {
                if (i1 >= 0 && i2 < arr.Length)
                {
                    int v1 = arr[i1];
                    int v2 = arr[i2];

                    int w1 = Math.Abs(v1 - x);
                    int w2 = Math.Abs(v2 - x);

                    if (w1 < w2)
                    {
                        // SS: i1 is closer to x than i2 is
                        result[kCount++] = arr[i1];
                        i1--;
                    }
                    else if (w1 > w2)
                    {
                        // SS: i2 is closer to x than i1 is
                        result[kCount++] = arr[i2];
                        i2++;
                    }
                    else
                    {
                        // SS: both are equally distant from x
                        result[kCount++] = v1;
                        i1--;
                    }
                }
                else if (i1 >= 0)
                {
                    result[kCount++] = arr[i1];
                    i1--;
                }
                else
                {
                    result[kCount++] = arr[i2];
                    i2++;
                }
            }

            // SS: sort
            Array.Sort(result);

            return result;
        }

        private IList<int> FindClosestElements2(int[] arr, int k, int x)
        {
            // SS: runtime complexity: O(log n) for BS
            // space complexity: O(1)

            // SS: find x in arr, or lower bound
            int min = 0;
            int max = arr.Length;
            while (min < max)
            {
                int mid = min + (max - min) / 2;

                int v2 = arr[mid];
                if (v2 == x)
                {
                    min = mid;
                    break;
                }

                if (x < v2)
                {
                    max = mid;
                }
                else
                {
                    min = mid + 1;
                }
            }

            int[] result = new int[k];

            // SS: take first k values around x according to the
            // constraints
            int i1 = min;
            int i2 = min;

            if (min < arr.Length && arr[i1] == x)
            {
                // SS: x is actually in the array
                i2++;
            }
            else
            {
                // SS: x is in between two values, so min points to the upper bound
                i1--;
            }

            int kCount = 0;
            while (kCount < k)
            {
                if (i1 >= 0 && i2 < arr.Length)
                {
                    int v1 = arr[i1];
                    int v2 = arr[i2];

                    int w1 = Math.Abs(v1 - x);
                    int w2 = Math.Abs(v2 - x);

                    if (w1 < w2)
                    {
                        // SS: i1 is closer to x than i2 is
                        kCount++;
                        i1--;
                    }
                    else if (w1 > w2)
                    {
                        // SS: i2 is closer to x than i1 is
                        kCount++;
                        i2++;
                    }
                    else
                    {
                        // SS: both are equally distant from x
                        kCount++;
                        i1--;
                    }
                }
                else if (i1 >= 0)
                {
                    kCount++;
                    i1--;
                }
                else
                {
                    kCount++;
                    i2++;
                }
            }

            // SS: copy values
            for (int i = 0; i < k; i++)
            {
                result[i] = arr[i1 + 1 + i];
            }

            return result;
        }

        [TestFixture]
        public class Tests
        {
            [TestCase(new[] {1, 2, 3, 4, 5}, 4, 3, new[] {1, 2, 3, 4})]
            [TestCase(new[] {1, 2, 3, 4, 5}, 4, -1, new[] {1, 2, 3, 4})]
            [TestCase(new[] {1, 2, 3, 4, 5}, 4, 4, new[] {2, 3, 4, 5})]
            [TestCase(new[] {1, 5, 9, 16, 21}, 3, 17, new[] {9, 16, 21})]
            [TestCase(new[] {1, 5, 9, 14, 15, 16, 21}, 3, 17, new[] {14, 15, 16})]
            [TestCase(new[] {1, 5, 9, 14, 15, 16, 21}, 3, 2, new[] {1, 5, 9})]
            [TestCase(new[] {1, 5, 7, 9, 14, 15, 16, 21}, 3, 4, new[] {1, 5, 7})]
            [TestCase(new[] {1, 5, 7, 9, 14, 15, 16, 21}, 3, 22, new[] {15, 16, 21})]
            [TestCase(new[] {1}, 1, 22, new[] {1})]
            [TestCase(new[] {1}, 1, 0, new[] {1})]
            [TestCase(new[] {1, 2, 3, 4, 4, 4, 4, 5, 5}, 3, 3, new[] {2, 3, 4})]
            public void Test(int[] arr, int k, int x, int[] expected)
            {
                // Arrange

                // Act
                IList<int> result = new Solution().FindClosestElements(arr, k, x);

                // Assert
                CollectionAssert.AreEqual(expected, result);
            }
        }
    }
}