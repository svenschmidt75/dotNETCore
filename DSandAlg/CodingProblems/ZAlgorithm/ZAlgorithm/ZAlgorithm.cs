using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace ZAlgorithm
{
    public class ZAlgorithm
    {
        public static int[] FindPattern(string s, string p)
        {
            // SS: see Tushar Ray, https://www.youtube.com/watch?v=CpZh4eF8QBw
            
            if (String.IsNullOrWhiteSpace(s) || String.IsNullOrWhiteSpace(p))
            {
                return new int[0];
            }
            
            // SS: create combined string
            // $ must not appear in either s nor p
            string combined = $"{p}${s}";
            
            // SS: create and fill z array
            int[] z = CreateZ(combined);

            var indices = new List<int>();
            for (int i = 1; i < z.Length; i++)
            {
                if (z[i] == p.Length)
                {
                    int idx = i - 1 - p.Length;
                    indices.Add(idx);
                }
            }

            return indices.ToArray();
        }

        internal static int[] CreateZ(string combined)
        {
            // SS: runtime complexity: O(n)
            
            int[] z = new int[combined.Length];

            int i = 1;
            while (i < combined.Length)
            {
                int j = 0;
                int k = i;
                
                while (k < combined.Length && combined[j] == combined[k])
                {
                    j++;
                    k++;
                }

                int patternLength = j;

                z[i] = patternLength;

                if (patternLength > 1)
                {
                    int idx = 1;
                    int l = i + idx;
                    int r = k;

                    while (l < r)
                    {
                        int v = z[idx];

                        if (l + v < r)
                        {
                            z[l] = v;
                            idx++;
                        }
                        else
                        {
                            // SS: right boundary exceeded, more comparisons needed
                            int length = r - l;
                            int p = r;
                            
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


        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1()
            {
                // Arrange
                string s = "xabcabzabc";
                string p = "abc";

                // Act
                int[] indices = ZAlgorithm.FindPattern(s, p);

                // Assert
                CollectionAssert.AreEqual(new[]{1, 7}, indices);
            }

            [Test]
            public void Test2()
            {
                // Arrange

                // Act
                int[] indices = ZAlgorithm.CreateZ("aabxaabxcaabxaabxay");

                // Assert
                CollectionAssert.AreEqual(new[]{0, 1, 0, 0, 4, 1, 0, 0, 0, 8, 1, 0, 0, 5, 1, 0, 0, 1, 0}, indices);
            }

            [Test]
            public void Test3()
            {
                // Arrange

                // Act
                int[] indices = ZAlgorithm.FindPattern("hello", "ll");

                // Assert
                CollectionAssert.AreEqual(new[]{2}, indices);
            }
            
            [Test]
            public void Test4()
            {
                // Arrange

                // Act
                int[] indices = ZAlgorithm.FindPattern("aaaaa", "baa");

                // Assert
                CollectionAssert.AreEqual(new int[0], indices);
            }

            [Test]
            public void Test5()
            {
                // Arrange

                // Act
                int[] indices = ZAlgorithm.FindPattern("", "");

                // Assert
                CollectionAssert.AreEqual(new int[0], indices);
            }

        }
    }
}