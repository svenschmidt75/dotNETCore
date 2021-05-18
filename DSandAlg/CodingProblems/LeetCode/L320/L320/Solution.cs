using System.Collections.Generic;
using NUnit.Framework;

// Problem: 320.Generalized Abbreviation
// URL: https://leetcode.com/problems/generalized-abbreviation/

namespace LeetCode
{
    public class Solution
    {
        public IList<string> GeneralizedAbbreviation(string word)
        {
            return GeneralizedAbbreviation1(word);
        }

        private IList<string> GeneralizedAbbreviation1(string word)
        {
            var result = new List<string>();

            void Backtracking(int idx, string s)
            {
                // SS: base case
                if (idx == word.Length && string.IsNullOrEmpty(s) == false)
                {
                    result.Add(s);
                    return;
                }

                if (idx > word.Length)
                {
                    return;
                }

                // SS: spacial case
                Backtracking(idx + 1, $"{s}{word[idx]}");
                
                for (int i = idx; i < word.Length; i++)
                {
                    int d = i - idx + 1;
                    Backtracking(i + 1, $"{s}{d}");
                }
            }

            Backtracking(0, "");
            return result;
        }

        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1()
            {
                // Arrange

                // Act
                var result = new Solution().GeneralizedAbbreviation("word");

                // Assert
                CollectionAssert.AreEquivalent(new[]{"word", "1ord", "w1rd", "wo1d", "wor1", "2rd", "w2d", "wo2", "1o1d", "1or1", "w1r1", "1o2", "2r1", "3d", "w3", "4"}, result);
            }
        }
    }
}