#region

using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

#endregion

// LeetCode 937. Reorder Data in Log Files
// https://leetcode.com/problems/reorder-data-in-log-files/

namespace SortLogFileLines
{
    public static class SortLogFileLines
    {
        public static string[] ReorderLogFiles(string[] logs)
        {
            var comparer = Comparer<(int type, string[] remainder, string id)>.Create((t1, t2) =>
            {
                var cmp = t1.type.CompareTo(t2.type);
                if (cmp != 0)
                {
                    return cmp;
                }

                if (t1.type == 1 && t2.type == 1)
                {
                    // SS: leave numeric loglines in original order
                    return 0;
                }

                for (var i = 0; i < t1.remainder.Length; i++)
                {
                    if (i >= t2.remainder.Length)
                    {
                        // SS: t2 is bigger than t1
                        return 1;
                    }

                    cmp = t1.remainder[i].CompareTo(t2.remainder[i]);
                    if (cmp != 0)
                    {
                        return cmp;
                    }
                }

                return t1.id.CompareTo(t2.id);
            });

            var sorted = logs.Select(logLine =>
                {
                    var words = logLine.Split(' ');
                    var id = words[0];
                    var remainder = words[1..];
                    var logType = char.IsDigit(remainder[0][0]) ? 1 : 0;
                    return (logType, remainder, id, logLine);
                }).OrderBy(x => (x.logType, x.remainder, x.id), comparer)
                .Select(x => x.logLine)
                .ToArray();
            return sorted;
        }

        [TestFixture]
        public class SortLogFileLinesTest
        {
            [Test]
            public void TestWithoutTies()
            {
                // Arrange
                string[] logFileLines =
                {
                    "ab1 abc def"
                    , "tg2 ghd ytr"
                    , "gf6 123 756 635"
                };

                // Act
                var sortedLines = ReorderLogFiles(logFileLines);

                // Assert
                Assert.AreEqual(logFileLines[0], sortedLines[0]);
                Assert.AreEqual(logFileLines[1], sortedLines[1]);
                Assert.AreEqual(logFileLines[2], sortedLines[2]);
            }

            [Test]
            public void TestWithTies()
            {
                // Arrange
                string[] logFileLines =
                {
                    "ab1 abc def"
                    , "tg2 ghd ytr"
                    , "ta2 ghd ytr"
                    , "gf6 123 756 635"
                };

                // Act
                var sortedLines = ReorderLogFiles(logFileLines);

                // Assert
                Assert.AreEqual(logFileLines[0], sortedLines[0]);
                Assert.AreEqual(logFileLines[2], sortedLines[1]);
                Assert.AreEqual(logFileLines[1], sortedLines[2]);
                Assert.AreEqual(logFileLines[3], sortedLines[3]);
            }

            [Test]
            public void Test3()
            {
                // Arrange
                string[] logFileLines =
                {
                    "dig1 8 1 5 1"
                    , "let1 art can"
                    , "dig2 3 6"
                    , "let2 own kit dig"
                    , "let3 art zero"
                };

                // Act
                var sortedLines = ReorderLogFiles(logFileLines);

                // Assert
                CollectionAssert.AreEqual(new[] {"let1 art can", "let3 art zero", "let2 own kit dig", "dig1 8 1 5 1", "dig2 3 6"}, sortedLines);
            }
        }
    }
}