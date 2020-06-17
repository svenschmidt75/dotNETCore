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
        public static List<string> Sort(List<string> lines)
        {
            // SS: partition in numbers only and strings
            var numberLines = new List<string>();
            var stringLines = new List<(int index, string id, string rhs, int hashCode)>();

            for (var i = 0; i < lines.Count; i++)
            {
                var logFile = lines[i];

                if (IsStringLine(logFile))
                {
                    // SS: get id
                    var lineId = GetId(logFile);
                    var rhs = GetRhs(logFile);
                    var hashCode = rhs.GetHashCode();
                    stringLines.Add((i, lineId, rhs, hashCode));
                }
                else
                {
                    numberLines.Add(logFile);
                }
            }

            // SS: sort string rhs alphanumerically
            var sortedLines = stringLines.OrderBy(t => t.rhs, new AlphanumComparator.AlphanumComparator()).ToList();

            // SS: resolve ties using hash code
            for (var i = 0; i < sortedLines.Count; i++)
            {
                var j = i + 1;
                while (j <= sortedLines.Count - 1 && sortedLines[i].hashCode == sortedLines[j].hashCode &&
                       sortedLines[i].rhs == sortedLines[j].rhs)
                {
                    j++;
                }

                if (j > i + 1)
                {
                    // SS: sort range [i, j)
                    var res = new List<(int index, string id, string rhs, int hashCode)>();
                    for (var k = i; k < j; k++)
                    {
                        var l = sortedLines[k];
                        res.Add(l);
                    }

                    var sortedRes = res.OrderBy(t => t.id, new AlphanumComparator.AlphanumComparator()).ToList();
                    for (var k = i; k < j; k++)
                    {
                        var l = sortedRes[k - i];
                        sortedLines[k] = l;
                    }

                    i = j - 1;
                }
            }

            // SS: merge
            var result = new List<string>();
            for (var i = 0; i < sortedLines.Count; i++)
            {
                var logLineIndex = sortedLines[i].index;
                var logLine = lines[logLineIndex];
                result.Add(logLine);
            }

            result.AddRange(numberLines);

            return result;
        }

        private static string GetRhs(string logFile)
        {
            // SS: make faster
            var tokens = logFile.Split(' ');
            var rhs = logFile.Substring(tokens[0].Length + 1);
            return rhs;
        }

        private static bool IsStringLine(string logFile)
        {
            // SS: make faster
            var tokens = logFile.Split(' ');
            return int.TryParse(tokens[1], out var id) == false;
        }

        private static string GetId(string logFile)
        {
            // SS: make faster
            var tokens = logFile.Split(' ');
            return tokens[0];
        }
    }


    [TestFixture]
    public class SortLogFileLinesTest
    {
        [Test]
        public void TestWithoutTies()
        {
            // Arrange
            var logFileLines = new List<string>
            {
                "ab1 abc def"
                , "tg2 ghd ytr"
                , "gf6 123 756 635"
            };

            // Act
            var sortedLines = SortLogFileLines.Sort(logFileLines);

            // Assert
            Assert.AreEqual(logFileLines[0], sortedLines[0]);
            Assert.AreEqual(logFileLines[1], sortedLines[1]);
            Assert.AreEqual(logFileLines[2], sortedLines[2]);
        }

        [Test]
        public void TestWithTies()
        {
            // Arrange
            var logFileLines = new List<string>
            {
                "ab1 abc def"
                , "tg2 ghd ytr"
                , "ta2 ghd ytr"
                , "gf6 123 756 635"
            };

            // Act
            var sortedLines = SortLogFileLines.Sort(logFileLines);

            // Assert
            Assert.AreEqual(logFileLines[0], sortedLines[0]);
            Assert.AreEqual(logFileLines[2], sortedLines[1]);
            Assert.AreEqual(logFileLines[1], sortedLines[2]);
            Assert.AreEqual(logFileLines[3], sortedLines[3]);
        }
    }
}
