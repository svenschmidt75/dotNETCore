#region

using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

#endregion

// Problem: 68. Text Justification
// URL: https://leetcode.com/problems/text-justification/

namespace LeetCode
{
    public class Solution
    {
        public IList<string> FullJustify(string[] words, int maxWidth)
        {
            // SS: runtime complexity: O(words.Length * maxWidth)
            
            var results = new List<string>();

            var i = 0;
            var j = 0;
            var lineLength = 0;
            var nSpaces = 0;

            while (j < words.Length)
            {
                var word = words[j];

                // SS: can we add another word?
                if (lineLength + nSpaces + word.Length <= maxWidth)
                {
                    lineLength += nSpaces + word.Length;
                    nSpaces = 1;
                    j++;
                }
                else
                {
                    // SS: create new line
                    var line = CreateJustifiedLine(i, j, words, maxWidth);
                    results.Add(line);

                    i = j;
                    lineLength = 0;
                    nSpaces = 0;
                }
            }

            // SS: create remaining line
            var line2 = CreateUnJustifiedLine(i, words, maxWidth);
            results.Add(line2);

            return results;
        }

        private static string CreateUnJustifiedLine(int i, string[] words, int maxWidth)
        {
            var builder = new StringBuilder();
            var lineLength = 0;
            for (var j = i; j < words.Length; j++)
            {
                var w = words[j];
                lineLength += w.Length;
                builder.Append(w);

                if (j <= words.Length - 2)
                {
                    builder.Append(' ');
                    lineLength++;
                }
            }

            // SS: pad line with spaces
            builder.Append(new string(' ', maxWidth - lineLength));

            var formattedLine = builder.ToString();
            return formattedLine;
        }

        private static string CreateJustifiedLine(int i, int j, string[] words, int maxWidth)
        {
            // SS: format words[i..j] on a line with maxWidth width
            var nWords = j - i;
            var spaces = new int[nWords];

            var lineWidth = 0;

            for (var k = 0; k < nWords; k++)
            {
                var w = words[i + k].Length;
                lineWidth += w;
            }

            // SS: evenly distribute spaces
            var remainder = maxWidth - lineWidth;
            if (nWords > 1)
            {
                var p = 0;
                while (remainder > 0)
                {
                    if (p == nWords - 1)
                    {
                        p = 0;
                    }

                    spaces[p++]++;
                    remainder--;
                }
            }
            else
            {
                // SS: only one word fits on line, pad with spaces
                spaces[0] = remainder;
            }

            var builder = new StringBuilder();
            var q = 0;
            for (var k = 0; k < nWords; k++)
            {
                var w = words[i + k];
                builder.Append(w);

                if (q < spaces.Length)
                {
                    builder.Append(new string(' ', spaces[q]));
                    q++;
                }
            }

            var formattedLine = builder.ToString();
            return formattedLine;
        }

        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1()
            {
                // Arrange
                string[] words = {"This", "is", "an", "example", "of", "text", "justification."};

                // Act
                var results = new Solution().FullJustify(words, 16);

                // Assert
                CollectionAssert.AreEqual(new[]
                {
                    "This    is    an", "example  of text", "justification.  "
                }, results);
            }

            [Test]
            public void Test2()
            {
                // Arrange
                string[] words = {"What", "must", "be", "acknowledgment", "shall", "be"};

                // Act
                var results = new Solution().FullJustify(words, 16);

                // Assert
                CollectionAssert.AreEqual(new[]
                {
                    "What   must   be", "acknowledgment  ", "shall be        "
                }, results);
            }

            [Test]
            public void Test3()
            {
                // Arrange
                string[] words = {"Science", "is", "what", "we", "understand", "well", "enough", "to", "explain", "to", "a", "computer.", "Art", "is", "everything", "else", "we", "do"};

                // Act
                var results = new Solution().FullJustify(words, 20);

                // Assert
                CollectionAssert.AreEqual(new[]
                {
                    "Science  is  what we", "understand      well", "enough to explain to", "a  computer.  Art is", "everything  else  we", "do                  "
                }, results);
            }

            [Test]
            public void Test4()
            {
                // Arrange
                string[] words = {"Science", "is", "what", "we", "understand", "well", "enough", "to", "explain", "to", "a", "computer.", "Art", "is", "everything", "else", "we", "do"};

                // Act
                var results = new Solution().FullJustify(words, 21);

                // Assert
                CollectionAssert.AreEqual(new[]
                {
                    "Science  is  what  we", "understand       well", "enough  to explain to", "a  computer.  Art  is", "everything else we do"
                }, results);
            }

            [Test]
            public void Test5()
            {
                // Arrange
                string[] words = {"Science"};

                // Act
                var results = new Solution().FullJustify(words, 20);

                // Assert
                CollectionAssert.AreEqual(new[]
                {
                    "Science             "
                }, results);
            }
        }
    }
}