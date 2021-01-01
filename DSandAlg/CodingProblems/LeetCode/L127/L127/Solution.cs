#region

using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

#endregion

// Problem: 127. Word Ladder
// URL: https://leetcode.com/problems/word-ladder/

namespace LeetCode
{
    public class Solution
    {
        public int LadderLength(string beginWord, string endWord, IList<string> wordList)
        {
            return LadderLengthBacktracking(beginWord, endWord, wordList);
        }

        private int LadderLengthBacktracking(string beginWord, string endWord, IList<string> wordList)
        {
            // SS: check for valid input

            int Solve(string currentWord, IList<string> wordsLeft, IList<string> wordsSoFar)
            {
                // SS: termination condition
                if (currentWord == endWord)
                {
                    return wordsSoFar.Count;
                }

                if (wordsLeft.Any() == false)
                {
                    // SS: no more words, so no path
                    return int.MaxValue;
                }

                var minCount = int.MaxValue;

                for (var i = 0; i < wordsLeft.Count; i++)
                {
                    var w = wordsLeft[i];

                    // SS: transition possible?
                    if (Difference(currentWord, w))
                    {
                        wordsSoFar.Add(w);
                        wordsLeft.Remove(w);
                        var c = Solve(w, wordsLeft, wordsSoFar);
                        minCount = Math.Min(minCount, c);

                        // SS: backtrack
                        wordsLeft.Add(w);
                        wordsSoFar.RemoveAt(wordsSoFar.Count - 1);
                    }
                }

                return minCount;
            }

            // SS: could check if end word is on wordList...

            var n = Solve(beginWord, wordList, new List<string> {beginWord});
            return n == int.MaxValue ? 0 : n;
        }

        private bool Difference(string w1, string w2)
        {
            var isDifferent = false;

            for (var i = 0; i < w1.Length; i++)
            {
                if (w1[i] != w2[i])
                {
                    if (isDifferent)
                    {
                        return false;
                    }

                    isDifferent = true;
                }
            }

            return true;
        }

        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1()
            {
                // Arrange
                var wordList = new List<string> {"hot", "dot", "dog", "lot", "log", "cog"};

                // Act
                var n = new Solution().LadderLength("hit", "cog", wordList);

                // Assert
                Assert.AreEqual(5, n);
            }

            [Test]
            public void Test2()
            {
                // Arrange
                var wordList = new List<string> {"hot", "dot", "dog", "lot", "log"};

                // Act
                var n = new Solution().LadderLength("hit", "cog", wordList);

                // Assert
                Assert.AreEqual(0, n);
            }
        }
    }
}