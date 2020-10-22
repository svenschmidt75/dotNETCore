#region

using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

#endregion

// 1218. Longest Arithmetic Subsequence of Given Difference
// https://leetcode.com/problems/longest-arithmetic-subsequence-of-given-difference/

namespace L1218
{
    public class Solution
    {
        public int LongestSubsequence(int[] arr, int difference)
        {
            if (arr.Length == 0)
            {
                return 0;
            }

            var hashMap = new Dictionary<int, List<int>>();
            for (var k = 0; k < arr.Length; k++)
            {
                var value = arr[k];

                if (hashMap.TryGetValue(value, out var items))
                {
                    items.Add(k);
                }
                else
                {
                    hashMap[value] = new List<int> {k};
                }
            }

            var i = 0;
            var j = 0;
            var maxCount = 1;

            while (arr.Length - i > maxCount)
            {
                j = i;
                var count = 1;

                var v = arr[j];
                var items = hashMap[v];
                items.Remove(j);
                if (items.Any() == false)
                {
                    hashMap.Remove(v);
                }

                // SS: copy hash map
                var workingHashMap = new Dictionary<int, List<int>>();
                foreach (var pair in hashMap)
                {
                    workingHashMap[pair.Key] = new List<int>(pair.Value);
                }

                while (j < arr.Length)
                {
                    var nextValue = v + difference;
                    if (workingHashMap.ContainsKey(nextValue) == false)
                    {
                        maxCount = Math.Max(maxCount, count);
                        i++;
                        break;
                    }

                    // SS: check whether the index is to the right of where we are
                    items = workingHashMap[nextValue];

                    var valid = false;

                    for (var k = 0; k < items.Count; k++)
                    {
                        var idx = items[k];
                        if (idx > j)
                        {
                            valid = true;
                            j = idx;
                            break;
                        }
                    }

                    if (valid == false)
                    {
                        maxCount = Math.Max(maxCount, count);
                        i++;
                        break;
                    }

                    count++;
                    items.Remove(j);
                    if (items.Any() == false)
                    {
                        workingHashMap.Remove(nextValue);
                    }

                    v = nextValue;
                }
            }

            return maxCount;
        }

        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1()
            {
                // Arrange
                int[] arr = {1, 2, 3, 4};
                var difference = 1;

                // Act
                var result = new Solution().LongestSubsequence(arr, difference);

                // Assert
                Assert.AreEqual(4, result);
            }

            [Test]
            public void Test2()
            {
                // Arrange
                int[] arr = {1, 3, 5, 7};
                var difference = 1;

                // Act
                var result = new Solution().LongestSubsequence(arr, difference);

                // Assert
                Assert.AreEqual(1, result);
            }

            [Test]
            public void Test3()
            {
                // Arrange
                int[] arr = {1, 5, 7, 8, 5, 3, 4, 2, 1};
                var difference = -2;

                // Act
                var result = new Solution().LongestSubsequence(arr, difference);

                // Assert
                Assert.AreEqual(4, result);
            }

            [Test]
            public void Test4()
            {
                // Arrange
                int[] arr = {10, -11, 8, -1, -14, -5, 7, 15, 7, -2, 14, 5, -3, -9, 12, -9};
                var difference = -2;

                // Act
                var result = new Solution().LongestSubsequence(arr, difference);

                // Assert
                Assert.AreEqual(2, result);
            }
        }
    }
}