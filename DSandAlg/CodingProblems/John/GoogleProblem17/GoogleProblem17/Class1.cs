#region

using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

#endregion

// LeetCode 269. Alien Dictionary
// https://leetcode.com/problems/alien-dictionary/

namespace GoogleProblem17
{
    public class Solution
    {
        public string Solve(string[] words)
        {
            var chars = new HashSet<char>();
            var orderInfo = new List<(char c1, char c2)>();

            for (var i = 0; i <= words.Length - 2; i++)
            {
                var w1 = words[i];
                var w2 = words[i + 1];

                var length = Math.Min(w1.Length, w2.Length);
                int j;
                for (j = 0; j < length; j++)
                {
                    var c1 = w1[j];
                    chars.Add(c1);

                    var c2 = w2[j];
                    chars.Add(c2);

                    if (c1 != c2)
                    {
                        orderInfo.Add((c1, c2));
                        Console.WriteLine($"{c1} < {c2}");
                        break;
                    }
                }

                // simply add remaining characters, since we have no
                // order information about them
                while (j < w1.Length)
                {
                    var c = w1[j];
                    chars.Add(c);
                    j++;
                }

                while (j < w2.Length)
                {
                    var c = w2[j];
                    chars.Add(c);
                    j++;
                }
            }

            // generate unordered alphabet
            var alphabet = chars.ToList();

            // order alphabet, O(n^2)
            var reordered = true;
            var cnt = 0;
            while (reordered && cnt < alphabet.Count * alphabet.Count)
            {
                reordered = false;
                cnt++;

                for (var i = 0; i < orderInfo.Count; i++)
                {
                    var (c1, c2) = orderInfo[i];

                    // O(n)
                    var p1 = alphabet.IndexOf(c1);
                    var p2 = alphabet.IndexOf(c2);

                    if (p1 > p2)
                    {
                        // reorder
                        alphabet.RemoveAt(p2);
                        alphabet.Insert(p1, c2);
                        reordered = true;
                    }
                }
            }

            return cnt == alphabet.Count * alphabet.Count ? "" : new string(alphabet.ToArray());
        }
    }

    [TestFixture]
    public class Tests
    {
        [Test]
        public void Test1()
        {
            // Arrange
            var words = new[] {"wrt", "wrf", "er", "ett", "rftt"};

            // Act
            var alphabet = new Solution().Solve(words);

            // Assert
            Assert.AreEqual("wertf", alphabet);
        }

        [Test]
        public void Test2()
        {
            // Arrange
            var words = new[] {"z", "x"};

            // Act
            var alphabet = new Solution().Solve(words);

            // Assert
            Assert.AreEqual("zx", alphabet);
        }

        [Test]
        public void Test3()
        {
            // Arrange
            var words = new[] {"z", "x", "z"};

            // Act
            var alphabet = new Solution().Solve(words);

            // Assert
            Assert.AreEqual("", alphabet);
        }
    }
}