#region

using System;
using System.Collections.Generic;
using NUnit.Framework;

#endregion

// Problem: 187. Repeated DNA Sequences
// URL: https://leetcode.com/problems/repeated-dna-sequences/

namespace LeetCode
{
    public class Solution
    {
        public IList<string> FindRepeatedDnaSequences(string s)
        {
            // SS: runtime complexity: O(s)
            // space complexity: O(s), i.e. #of substrings
            
            var result = new List<string>();

            if (s.Length < 10)
            {
                return result;
            }

            var hash = new Dictionary<string, int>();
            var i = 0;
            while (i + 9 < s.Length)
            {
                var ss = s.Substring(i, 10);
                if (hash.TryGetValue(ss, out var freq))
                {
                    if (freq == 1)
                    {
                        result.Add(ss);
                    }

                    hash[ss] += 1;
                }
                else
                {
                    hash[ss] = 1;
                }

                i++;
            }

            return result;
        }

        public IList<string> FindRepeatedDnaSequencesSlow(string s)
        {
            // SS: runtime complexity: O(s^2), due to construction of LCP

            var result = new List<string>();

            if (s.Length < 10)
            {
                return result;
            }

            var sa = CreateSuffixArray(s);
            var lcp = CreateLongestCommonPrefixArray(s, sa);

            var tmpResults = new HashSet<string>();
            for (var i = 0; i < lcp.Length; i++)
            {
                if (lcp[i] >= 10)
                {
                    var ss = s.Substring(sa[i], 10);
                    tmpResults.Add(ss);
                }
            }

            foreach (var tmpResult in tmpResults)
            {
                result.Add(tmpResult);
            }

            return result;
        }

        internal static int[] CreateLongestCommonPrefixArray(string s, int[] sa)
        {
            // SS: O(s^2)
            var lcp = new int[sa.Length];

            for (var i = 1; i < s.Length; i++)
            {
                var s1 = s.Substring(sa[i - 1]);
                var s2 = s.Substring(sa[i]);

                var equ = 0;
                var j = 0;
                while (j < s1.Length && j < s2.Length && s1[j] == s2[j])
                {
                    equ++;
                    j++;
                }

                lcp[i] = equ;
            }

            return lcp;
        }

        internal static int[] CreateSuffixArray(string s)
        {
            // SS: O(s log s)
            var sa = new int[s.Length];
            for (var i = 0; i < s.Length; i++)
            {
                sa[i] = i;
            }

            // SS: sort suffixes
            Array.Sort(sa, (a, b) => s.Substring(a).CompareTo(s.Substring(b)));

            return sa;
        }

        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1()
            {
                // Arrange

                // Act
                var sa = CreateSuffixArray("camel");

                // Assert
                CollectionAssert.AreEqual(new[] {1, 0, 3, 4, 2}, sa);
            }

            [Test]
            public void Test2()
            {
                // Arrange

                // Act
                var sa = CreateSuffixArray("ABABBAB");

                // Assert
                CollectionAssert.AreEqual(new[] {5, 0, 2, 6, 4, 1, 3}, sa);
            }

            [Test]
            public void Test3()
            {
                // Arrange
                var s = "ABABBAB";
                var sa = CreateSuffixArray(s);

                // Act
                var lcp = CreateLongestCommonPrefixArray(s, sa);

                // Assert
                CollectionAssert.AreEqual(new[] {0, 2, 2, 0, 1, 3, 1}, lcp);
            }

            [Test]
            public void Test4()
            {
                // Arrange
                var s = "AAAAACCCCCAAAAACCCCCCAAAAAGGGTTT";

                // Act
                var result = new Solution().FindRepeatedDnaSequences(s);

                // Assert
                CollectionAssert.AreEquivalent(new[] {"AAAAACCCCC", "CCCCCAAAAA"}, result);
            }

            [Test]
            public void Test5()
            {
                // Arrange
                var s = "AAAAAAAAAAAAA";

                // Act
                var result = new Solution().FindRepeatedDnaSequences(s);

                // Assert
                CollectionAssert.AreEqual(new[] {"AAAAAAAAAA"}, result);
            }

            [Test]
            public void Test6()
            {
                // Arrange
                var s = "GAGAGAGAGAGAG";

                // Act
                var result = new Solution().FindRepeatedDnaSequences(s);

                // Assert
                CollectionAssert.AreEquivalent(new[] {"GAGAGAGAGA", "AGAGAGAGAG"}, result);
            }
        }
    }
}