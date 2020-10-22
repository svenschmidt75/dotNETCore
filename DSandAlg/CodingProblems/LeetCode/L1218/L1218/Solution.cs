#region

using System;
using System.Collections;
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
        public int LongestSubsequence1(int[] arr, int difference)
        {
            // SS: runtime complexity: O(N^2)
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

        public int LongestSubsequence2(int[] arr, int difference)
        {
            // SS: runtime complexity: O(N^2)
            if (arr.Length == 0)
            {
                return 0;
            }

            var toVisit = new HashSet<int>();
            var values = new HashSet<int>();
            for (int k = 0; k < arr.Length; k++)
            {
                toVisit.Add(k);
                values.Add(arr[k]);
            }

            int maxCount = 1;
            
            for (int i = 0; i < arr.Length; i++)
            {
                if (toVisit.Contains(i) == false)
                {
                    continue;
                }

                toVisit.Remove(i);

                int count = 1;
                int v = arr[i];
                int vNext = v + difference;

                if (values.Contains(vNext))
                {
                    for (int j = i + 1; j < arr.Length; j++)
                    {
                    
                        var value = arr[j];
                        if (value == vNext)
                        {
                            count++;
                            vNext += difference;
                            toVisit.Remove(j);

                            if (values.Contains(vNext) == false)
                            {
                                break;
                            }
                        }
                    }
                }

                maxCount = Math.Max(maxCount, count);
            }

            return maxCount;
        }

        public int LongestSubsequence3(int[] arr, int difference)
        {
            // SS: O(N^2)
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

            var toVisit = new HashSet<int>();
            for (int k = 0; k < arr.Length; k++)
            {
                toVisit.Add(k);
            }
            
            
            var i = 0;
            var j = 0;
            var maxCount = 1;

            while (arr.Length - i > maxCount)
            {
                if (toVisit.Contains(i) == false)
                {
                    i++;
                    continue;
                }

                toVisit.Remove(i);
                
                j = i;
                var count = 1;

                var v = arr[j];
                var items = hashMap[v];
                items.Remove(j);
                if (items.Any() == false)
                {
                    hashMap.Remove(v);
                }

                while (j < arr.Length)
                {
                    var nextValue = v + difference;
                    if (hashMap.ContainsKey(nextValue) == false)
                    {
                        maxCount = Math.Max(maxCount, count);
                        i++;
                        break;
                    }

                    // SS: check whether the index is to the right of where we are
                    items = hashMap[nextValue];
                    
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
                    v = nextValue;
                    toVisit.Remove(j);
                }
            }

            return maxCount;
        }

        class Cmp : IComparer
        {
            public int Compare(object lhs, object rhs)
            {
                var x = ((int v, int idx)) lhs;
                var y = ((int v, int idx)) rhs;
                return x.v.CompareTo(y.v);
            }
        }

        public int LongestSubsequence(int[] arr, int difference)
        {
            // SS: still time limit exceeded
            if (arr.Length == 0)
            {
                return 0;
            }

            // SS: O(N log N)
            // Note: important to use stable sort here!
            var sortedArr = arr.Select((v, idx) => (v, idx)).OrderBy(t => t.v).ToArray();
            
            var toVisit = new HashSet<int>();
            for (int k = 0; k < arr.Length; k++)
            {
                toVisit.Add(k);
            }

            int maxCount = 0;
            
            for (int i = 0; i < arr.Length; i++)
            {
                if (toVisit.Contains(i) == false)
                {
                    continue;    
                }

                toVisit.Remove(i);

                var v = arr[i];
                var vNext = v + difference;

                int count = 1;
                
                int j = i + 1;
                while (true)
                {
                    // SS: O(log N)
                    int idx = Array.BinarySearch(sortedArr, (vNext, 0), new Cmp());
                    if (idx < 0)
                    {
                        maxCount = Math.Max(maxCount, count);
                        break;
                    }

                    // SS: is the index > j?
                    while (idx < sortedArr.Length && sortedArr[idx].v == vNext && sortedArr[idx].idx < j)
                    {
                        idx++;
                    }

                    if (idx >= sortedArr.Length || sortedArr[idx].v != vNext)
                    {
                        // SS: index too small
                        maxCount = Math.Max(maxCount, count);
                        break;
                    }

                    j = sortedArr[idx].idx;
                    toVisit.Remove(j);

                    count++;

                    vNext += difference;
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