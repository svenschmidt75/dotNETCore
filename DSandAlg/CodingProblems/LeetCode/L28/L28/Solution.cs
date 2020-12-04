#region

using System;
using System.Collections.Generic;
using NUnit.Framework;

#endregion

// Problem: 28. Implement strStr()
// URL: https://leetcode.com/problems/implement-strstr/

namespace LeetCode28
{
    public class Solution
    {
        public int StrStr(string haystack, string needle)
        {
            if (string.IsNullOrWhiteSpace(needle))
            {
                return 0;
            }

            var indices = ZAlgorithm.FindPattern(haystack, needle);
            return indices.Length == 0 ? -1 : indices[0];
        }

        public class ZAlgorithm
        {
            public static int[] FindPattern(string s, string p)
            {
                // SS: see Tushar Ray, https://www.youtube.com/watch?v=CpZh4eF8QBw

                if (string.IsNullOrWhiteSpace(s) || string.IsNullOrWhiteSpace(p))
                {
                    return new int[0];
                }

                // SS: create combined string
                // $ must not appear in either s nor p
                var combined = $"{p}${s}";

                // SS: create and fill z array
                var z = CreateZ(combined);

                var indices = new List<int>();
                for (var i = 1; i < z.Length; i++)
                {
                    if (z[i] == p.Length)
                    {
                        var idx = i - 1 - p.Length;
                        indices.Add(idx);
                    }
                }

                return indices.ToArray();
            }

            internal static int[] CreateZ(string combined)
            {
                // SS: runtime complexity: O(n)

                var z = new int[combined.Length];

                var i = 1;
                while (i < combined.Length)
                {
                    var j = 0;
                    var k = i;

                    while (k < combined.Length && combined[j] == combined[k])
                    {
                        j++;
                        k++;
                    }

                    var patternLength = j;

                    z[i] = patternLength;

                    if (patternLength > 1)
                    {
                        var idx = 1;
                        var l = i + idx;
                        var r = k;

                        while (l < r)
                        {
                            var v = z[idx];

                            if (l + v < r)
                            {
                                z[l] = v;
                                idx++;
                            }
                            else
                            {
                                // SS: right boundary exceeded, more comparisons needed
                                var length = r - l;
                                var p = r;

                                while (p < combined.Length && combined[length] == combined[p])
                                {
                                    p++;
                                    length++;
                                }

                                z[l] = length;
                                r = p;
                                idx = 1;
                            }

                            l++;
                        }

                        k = r;
                    }

                    i = Math.Max(i + 1, k);
                }

                return z;
            }
        }

        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1()
            {
                // Arrange

                // Act
                var idx = new Solution().StrStr("hello", "ll");

                // Assert
                Assert.AreEqual(2, idx);
            }

            [Test]
            public void Test2()
            {
                // Arrange

                // Act
                var idx = new Solution().StrStr("aaaaa", "baa");

                // Assert
                Assert.AreEqual(-1, idx);
            }

            [Test]
            public void Test3()
            {
                // Arrange

                // Act
                var idx = new Solution().StrStr("", "");

                // Assert
                Assert.AreEqual(0, idx);
            }

            [Test]
            public void Test4()
            {
                // Arrange

                // Act
                var idx = new Solution().StrStr("", "a");

                // Assert
                Assert.AreEqual(-1, idx);
            }
        }
    }
}