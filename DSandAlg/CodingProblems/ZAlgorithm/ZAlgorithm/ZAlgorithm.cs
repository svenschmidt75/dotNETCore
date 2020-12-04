#region

using System;
using System.Collections.Generic;
using NUnit.Framework;

#endregion

namespace ZAlgorithm
{
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
                
                // SS: j is the pattern match length
                z[i] = j;

                if (j > 1)
                {
                    var offset = 1;
                    var l = i + offset;
                    var r = k;

                    while (l < r)
                    {
                        var v = z[offset];

                        if (l + v < r)
                        {
                            z[l] = v;
                            offset++;
                        }
                        else
                        {
                            // SS: right boundary exceeded, more comparisons needed
                            var length = r - l;

                            while (r < combined.Length && combined[length] == combined[r])
                            {
                                r++;
                                length++;
                            }

                            z[l] = length;
                            offset = 1;
                        }

                        l++;
                    }

                    k = r;
                }

                i = Math.Max(i + 1, k);
            }

            return z;
        }


        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1()
            {
                // Arrange
                var s = "xabcabzabc";
                var p = "abc";

                // Act
                var indices = FindPattern(s, p);

                // Assert
                CollectionAssert.AreEqual(new[] {1, 7}, indices);
            }

            [Test]
            public void Test2()
            {
                // Arrange

                // Act
                var indices = CreateZ("aabxaabxcaabxaabxay");

                // Assert
                CollectionAssert.AreEqual(new[] {0, 1, 0, 0, 4, 1, 0, 0, 0, 8, 1, 0, 0, 5, 1, 0, 0, 1, 0}, indices);
            }

            [Test]
            public void Test3()
            {
                // Arrange

                // Act
                var indices = FindPattern("hello", "ll");

                // Assert
                CollectionAssert.AreEqual(new[] {2}, indices);
            }

            [Test]
            public void Test4()
            {
                // Arrange

                // Act
                var indices = FindPattern("aaaaa", "baa");

                // Assert
                CollectionAssert.AreEqual(new int[0], indices);
            }

            [Test]
            public void Test5()
            {
                // Arrange

                // Act
                var indices = FindPattern("", "");

                // Assert
                CollectionAssert.AreEqual(new int[0], indices);
            }
        }
    }
}