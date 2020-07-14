#region

using System.Collections.Generic;
using System.Diagnostics;
using NUnit.Framework;

#endregion

// 992. Subarrays with K Different Integers
// https://leetcode.com/problems/subarrays-with-k-different-integers/ 

namespace L992
{
    public class Solution
    {
        private static void AddElement(int value, IDictionary<int, int> map)
        {
            if (map.ContainsKey(value))
            {
                map[value]++;
            }
            else
            {
                map[value] = 1;
            }
        }

        private static void RemoveElement(int value, IDictionary<int, int> map)
        {
            if (map.TryGetValue(value, out var freq))
            {
                if (freq == 1)
                {
                    map.Remove(value);
                }
                else
                {
                    map[value]--;
                }
            }
        }

        public int SubarraysWithKDistinct(int[] A, int K)
        {
            if (A.Length < K || K == 0)
            {
                return 0;
            }

            // SS: use two sliding windows to achieve O(N) runtime complexity
            // 1. the outer is the largest subarray with K elements
            // 2. the inner one is the smallest subarray with K elements, and "slides"
            // within the outer one
            var ijkMap = new Dictionary<int, int>();
            var jkMap = new Dictionary<int, int>();

            var i = 0;
            var j = 0;
            var k = 0;

            var count = 0;

            while (true)
            {
                // SS: Step 1: find the largest subarray containing K elements 
                while (ijkMap.Count <= K && k < A.Length)
                {
                    var number = A[k];

                    // SS: can we add `number` without increasing the count?
                    if (ijkMap.Count < K || ijkMap.ContainsKey(number))
                    {
                        AddElement(number, ijkMap);
                    }
                    else
                    {
                        break;
                    }

                    k++;
                }

                if (ijkMap.Count < K)
                {
                    break;
                }

                // SS: Step 2: within the largest subarray of K elements, find the smallest subarrays containing K elements 
                while (k - i >= K)
                {
                    if (jkMap.Count < K)
                    {
                        if (j >= k)
                        {
                            break;
                        }

                        var number = A[j];
                        AddElement(number, jkMap);
                        j++;
                    }
                    else
                    {
                        // SS: count the number of subarrays of size K 
                        var delta = k - j + 1;
                        count += delta;

                        // shrink
                        var number = A[i];
                        RemoveElement(number, jkMap);
                        RemoveElement(number, ijkMap);
                        i++;
                    }
                }
            }

            return count;
        }

    }

    [TestFixture]
    public class Tests
    {
        [Test]
        public void Test1()
        {
            // Arrange
            int[] A = {2, 1, 2, 1, 3};
            var k = 2;

            // Act
            var result = new Solution().SubarraysWithKDistinct(A, k);

            // Assert
            Assert.AreEqual(7, result);
        }

        [Test]
        public void Test2()
        {
            // Arrange
            int[] A = {2, 1, 1, 2, 3};
            var k = 2;

            // Act
            var result = new Solution().SubarraysWithKDistinct(A, k);

            // Assert
            Assert.AreEqual(6, result);
        }

        [Test]
        public void Test3()
        {
            // Arrange
            int[] A = {2, 1, 1, 2, 3};
            var k = 3;

            // Act
            var result = new Solution().SubarraysWithKDistinct(A, k);

            // Assert
            Assert.AreEqual(3, result);
        }

        [Test]
        public void Test4()
        {
            // Arrange
            int[] A = {1, 2, 1, 3, 4};
            var k = 3;

            // Act
            var result = new Solution().SubarraysWithKDistinct(A, k);

            // Assert
            Assert.AreEqual(3, result);
        }

        [Test]
        public void Test5()
        {
            // Arrange
            int[] A = {2, 2, 1, 2, 2, 2, 1, 1};
            var k = 2;

            // Act
            var result = new Solution().SubarraysWithKDistinct(A, k);

            // Assert
            Assert.AreEqual(23, result);
        }

        [Test]
        public void Test6()
        {
            // Arrange
            int[] A =
            {
                27, 27, 43, 28, 11, 20, 1, 4, 49, 18, 37, 31, 31, 7, 3, 31, 50, 6, 50, 46, 4, 13, 31, 49, 15, 52, 25, 31
                , 35, 4, 11, 50, 40, 1, 49, 14, 46, 16, 11, 16, 39, 26, 13, 4, 37, 39, 46, 27, 49, 39, 49, 50, 37, 9, 30
                , 45, 51, 47, 18, 49, 24, 24, 46, 47, 18, 46, 52, 47, 50, 4, 39, 22, 50, 40, 3, 52, 24, 50, 38, 30, 14
                , 12, 1, 5, 52, 44, 3, 49, 45, 37, 40, 35, 50, 50, 23, 32, 1, 2
            };
            var k = 20;

            // Act
            var result = new Solution().SubarraysWithKDistinct(A, k);

            // Assert
            Assert.AreEqual(149, result);
        }
    }
}