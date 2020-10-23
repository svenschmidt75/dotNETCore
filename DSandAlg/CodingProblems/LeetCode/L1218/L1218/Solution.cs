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
        public int LongestSubsequenceBruteForce(int[] arr, int difference)
        {
            // SS: runtime complexity: O(N^2)
            var maxCount = 0;
            for (var i = 0; i < arr.Length; i++)
            {
                var count = 1;
                var v = arr[i];
                var vNext = v + difference;
                for (var j = i + 1; j < arr.Length; j++)
                {
                    if (arr[j] == vNext)
                    {
                        vNext += difference;
                        count++;
                    }
                }

                maxCount = Math.Max(maxCount, count);
            }

            return maxCount;
        }

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
            for (var k = 0; k < arr.Length; k++)
            {
                toVisit.Add(k);
                values.Add(arr[k]);
            }

            var maxCount = 1;

            for (var i = 0; i < arr.Length; i++)
            {
                if (toVisit.Contains(i) == false)
                {
                    continue;
                }

                toVisit.Remove(i);

                var count = 1;
                var v = arr[i];
                var vNext = v + difference;

                if (values.Contains(vNext))
                {
                    for (var j = i + 1; j < arr.Length; j++)
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
            for (var k = 0; k < arr.Length; k++)
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

        private int BinarySearchMin((int v, int idx)[] arr, int element, int min, int max)
        {
            // SS: stable BS, stable in the sense that when there is more than one element
            // with the same value, return the one with the lowest index
            if (min == max)
            {
                if (arr[min].v == element)
                {
                    return min;
                }

                return -1;
            }

            var mid = (min + max) / 2;

            var midValue = arr[mid].v;

            if (element <= midValue)
            {
                return BinarySearchMin(arr, element, min, mid);
            }

            return BinarySearchMin(arr, element, mid + 1, max);
        }

        private int BinarySearchMax((int v, int idx)[] arr, int element, int min, int max)
        {
            // SS: stable BS, stable in the sense that when there is more than one element
            // with the same value, return the one with the largest index
            if (min == max)
            {
                if (arr[min].v == element)
                {
                    return min;
                }

                return -1;
            }

            var mid = (min + max) / 2;

            var midValue = arr[mid].v;

            if (midValue == element)
            {
                if (mid < max && arr[mid + 1].v == element)
                {
                    return BinarySearchMax(arr, element, mid + 1, max);
                }

                return mid;
            }

            if (element < midValue)
            {
                return BinarySearchMax(arr, element, min, mid);
            }

            return BinarySearchMax(arr, element, mid + 1, max);
        }

        public int LongestSubsequence5(int[] arr, int difference)
        {
            // SS: runtime complexity: O(N log N), still time limit exceeded
            if (arr.Length == 0)
            {
                return 0;
            }

            // SS: O(N log N)
            // Note: important to use stable sort here!
            var sortedArr = arr.Select((v, idx) => (v, idx)).OrderBy(t => t.v).ToArray();

            var toVisit = new HashSet<int>();
            for (var k = 0; k < arr.Length; k++)
            {
                toVisit.Add(k);
            }

            var maxCount = 0;

            for (var i = 0; i < arr.Length; i++)
            {
                if (toVisit.Contains(i) == false)
                {
                    continue;
                }

                toVisit.Remove(i);

                var v = arr[i];
                var vNext = v + difference;

                var count = 1;

                var j = i;
                while (true)
                {
                    // SS: O(log N)
                    var idx = BinarySearchMin(sortedArr, vNext, 0, sortedArr.Length - 1);
                    if (idx < 0)
                    {
                        maxCount = Math.Max(maxCount, count);
                        break;
                    }

                    // SS: is the index > j?
                    while (idx < sortedArr.Length && sortedArr[idx].v == vNext && sortedArr[idx].idx <= j)
                    {
                        idx++;
                    }

                    if (idx == sortedArr.Length || sortedArr[idx].v != vNext)
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

        public int LongestSubsequence6(int[] arr, int difference)
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
            for (var k = 0; k < arr.Length; k++)
            {
                toVisit.Add(k);
            }

            var maxCount = 0;

            // SS: each element is only touched once, so
            // O(N log N) total runtime complexity
            for (var i = arr.Length - 1; i >= 0; i--)
            {
                if (toVisit.Contains(i) == false)
                {
                    continue;
                }

                toVisit.Remove(i);

                var v = arr[i];
                var vNext = v - difference;

                var count = 1;

                var j = i;
                while (true)
                {
                    // SS: O(log N)
                    var idx = BinarySearchMax(sortedArr, vNext, 0, sortedArr.Length - 1);
                    if (idx < 0)
                    {
                        maxCount = Math.Max(maxCount, count);
                        break;
                    }

                    // SS: is the index > j?
                    while (idx >= 0 && sortedArr[idx].v == vNext && sortedArr[idx].idx >= j)
                    {
                        idx--;
                    }

                    if (idx < 0 || sortedArr[idx].v != vNext)
                    {
                        // SS: index too small
                        maxCount = Math.Max(maxCount, count);
                        break;
                    }

                    j = sortedArr[idx].idx;
                    toVisit.Remove(j);

                    count++;

                    vNext -= difference;
                }
            }

            return maxCount;
        }

        public int LongestSubsequence7(int[] arr, int difference)
        {
            // SS: 
            if (arr.Length == 0)
            {
                return 0;
            }

            var hashMap = new Dictionary<int, List<int>>();
            var toVisit = new HashSet<int>();
            for (var k = 0; k < arr.Length; k++)
            {
                toVisit.Add(k);

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

            var maxCount = 0;

            for (var i = 0; i < arr.Length; i++)
            {
                if (toVisit.Contains(i) == false)
                {
                    continue;
                }

                toVisit.Remove(i);

                var v = arr[i];
                var vNext = v + difference;

                var count = 1;

                var j = i;
                while (true)
                {
                    if (hashMap.TryGetValue(vNext, out var items) == false)
                    {
                        maxCount = Math.Max(maxCount, count);
                        break;
                    }

                    // SS: value exists, but is the index to our right
                    var k = items.BinarySearch(j + 1);
                    if (k < 0)
                    {
                        k = ~k;
                    }

                    if (k == items.Count)
                    {
                        maxCount = Math.Max(maxCount, count);
                        break;
                    }

                    j = items[k];
                    toVisit.Remove(j);
                    count++;
                    vNext += difference;
                }
            }

            return maxCount;
        }

        public int LongestSubsequence(int[] arr, int difference)
        {
            if (arr.Length == 0)
            {
                return 0;
            }

            var hashMap = new Dictionary<int, int>();

            var maxCount = 0;

            for (var i = 0; i < arr.Length; i++)
            {
                var v = arr[i];

                if (hashMap.ContainsKey(v) == false)
                {
                    hashMap[v] = 0;
                }

                var count = hashMap[v];
                hashMap[v + difference] = count + 1;
                maxCount = Math.Max(maxCount, count + 1);
            }

            return maxCount;
        }
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

        [Test]
        public void Test5()
        {
            // Arrange
            int[] arr = {4, 12, 10, 0, -2, 7, -8, 9, -9, -12, -12, 8, 8};
            var difference = 0;

            // Act
            var result = new Solution().LongestSubsequence(arr, difference);

            // Assert
            Assert.AreEqual(2, result);
        }

        [Test]
        public void Test6()
        {
            // Arrange
            int[] arr =
            {
                24, 55, -149, 25, 134, -63, -57, 95, -124, 141, -65, 85, 111, -119, 179, 180, -24, 186, -126, -91, -69, -27, -77, -190, 59, 140, -103, -156, 138, 158, 103, -10, -113, -67, 8, 80, -165
                , 95, 129, -148, 17, 106, 23, -104, 110, -42, 33, 166, 94, 171, 23, 35, 60, -93, 71, 83, 193, -65, 153, 181, -144, -168, 178, 44, 48, 139, -3, -61, 172, 32, -122, -150, 175, 97, 183
                , 200, 187, -132, 88, 8, -109, -183, -150, -123, -151, -24, 177, 197, -108, -168, -170, -43, -143, 108, -96, -135, 137, -53, 126, -20, 140, -14, -200, -192, -25, 177, -65, 41, 140, 107
                , -76, 187, -118, -15, -127, -170, -99, 81, -96, 125, 157, 177, -56, -118, -179, 176, 125, 157, 140, -92, -87, -14, 158, -145, 130, -41, -27, -6, -126, 83, -137, 144, 85, 134, -84, 147
                , -168, -153, -179, 163, -40, -122, -80, 200, 173, -27, 21, 199, 105, 64, 97, -4, 60, 87, -152, -181, -21, -45, -196, 26, -141, -3, 4, -196, -100, 171, 77, 92, -119, -174, 35, -58
                , -198, -123, -87, -109, 112, 141, -128, 79, -11, 61, 200, -155, -117, -129, 25, -110, -176, -25
            };
            var difference = -8;

            // Act
            var result = new Solution().LongestSubsequence(arr, difference);

            // Assert
            Assert.AreEqual(5, result);
        }
    }
}