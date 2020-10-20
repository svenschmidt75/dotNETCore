#region

using System.Collections.Generic;
using NUnit.Framework;

#endregion

// 30. Substring with Concatenation of All Words
// https://leetcode.com/problems/substring-with-concatenation-of-all-words/

namespace L30
{
    public class Solution
    {
        public IList<int> FindSubstring(string s, string[] words)
        {
            // SS: runtime complexity is O(#s * #words)
            var result = new List<int>();

            if (s.Length == 0 || words.Length == 0)
            {
                return result;
            }

            var wordLength = 0;

            // SS: insert words with their multiplicity into hash map
            var wordHash = new Dictionary<string, int>();
            for (var k = 0; k < words.Length; k++)
            {
                var word = words[k];
                wordLength = word.Length;

                if (wordHash.ContainsKey(word) == false)
                {
                    wordHash[word] = 1;
                }
                else
                {
                    wordHash[word]++;
                }
            }

            var wordsSeen = new Dictionary<string, int>();
            var nWordsSeen = 0;

            var maxPos = words.Length * wordLength;
            for (var i = 0; i <= s.Length - maxPos; i++)
            {
                var nextWord = s.Substring(i, wordLength);

                // SS: valid word?
                if (wordHash.ContainsKey(nextWord))
                {
                    wordsSeen.Clear();
                    wordsSeen[nextWord] = 1;
                    nWordsSeen = 1;

                    var j = i + wordLength;
                    while (j <= s.Length - wordLength)
                    {
                        nextWord = s.Substring(j, wordLength);

                        // SS: valid word?
                        if (wordHash.TryGetValue(nextWord, out var frequency) == false)
                        {
                            break;
                        }

                        // SS: valid word but multiplicity too high?
                        if (wordsSeen.ContainsKey(nextWord) && wordsSeen[nextWord] == frequency)
                        {
                            break;
                        }

                        if (wordsSeen.ContainsKey(nextWord) == false)
                        {
                            wordsSeen[nextWord] = 1;
                        }
                        else
                        {
                            wordsSeen[nextWord]++;
                        }

                        nWordsSeen++;
                        j += wordLength;

                        if (nWordsSeen == words.Length)
                        {
                            result.Add(i);
                            wordsSeen.Clear();
                            nWordsSeen = 0;
                            break;
                        }
                    }

                    if (nWordsSeen == words.Length)
                    {
                        result.Add(i);
                    }
                }
            }

            return result;
        }

        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1()
            {
                // Arrange
                var s = "barfoothefoobarman";
                var words = new[] {"foo", "bar"};

                // Act
                var result = new Solution().FindSubstring(s, words);

                // Assert
                CollectionAssert.AreEqual(new[] {0, 9}, result);
            }

            [Test]
            public void Test2()
            {
                // Arrange
                var s = "wordgoodgoodgoodbestword";
                var words = new[] {"word", "good", "best", "word"};

                // Act
                var result = new Solution().FindSubstring(s, words);

                // Assert
                CollectionAssert.AreEqual(new List<int>(), result);
            }

            [Test]
            public void Test3()
            {
                // Arrange
                var s = "barfoofoobarthefoobarman";
                var words = new[] {"bar", "foo", "the"};

                // Act
                var result = new Solution().FindSubstring(s, words);

                // Assert
                CollectionAssert.AreEqual(new[] {6, 9, 12}, result);
            }

            [Test]
            public void Test4()
            {
                // Arrange
                var s = "wordgoodgoodgoodbestword";
                var words = new[] {"word", "good", "best", "good"};

                // Act
                var result = new Solution().FindSubstring(s, words);

                // Assert
                CollectionAssert.AreEqual(new[] {8}, result);
            }

            [Test]
            public void Test5()
            {
                // Arrange
                var s = "lingmindraboofooowingdingbarrwingmonkeypoundcake";
                var words = new[] {"fooo", "barr", "wing", "ding", "wing"};

                // Act
                var result = new Solution().FindSubstring(s, words);

                // Assert
                CollectionAssert.AreEqual(new[] {13}, result);
            }

            [Test]
            public void Test6()
            {
                // Arrange
                var s = "aaaaaaaaaaaaaa";
                var words = new[] {"aa", "aa"};

                // Act
                var result = new Solution().FindSubstring(s, words);

                // Assert
                CollectionAssert.AreEqual(new[] {0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10}, result);
            }

            [Test]
            public void Test7()
            {
                // Arrange
                var s = "a";
                var words = new[] {"a"};

                // Act
                var result = new Solution().FindSubstring(s, words);

                // Assert
                CollectionAssert.AreEqual(new[] {0}, result);
            }
        }
    }
}