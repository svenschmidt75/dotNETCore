#region

using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

#endregion

/*
 * Leetcode 937
 * https://leetcode.com/problems/reorder-data-in-log-files/
 * 
 * You have an array of logs.  Each log is a space delimited string of words.
 * For each log, the first word in each log is an alphanumeric identifier.  Then, either:

 *  Each word after the identifier will consist only of lowercase letters, or;
 *  Each word after the identifier will consist only of digits.

 * We will call these two varieties of logs letter-logs and digit-logs.
 * It is guaranteed that each log has at least one word after its identifier.
 * Reorder the logs so that all of the letter-logs come before any digit-log.
 * The letter-logs are ordered lexicographically ignoring identifier, with the identifier used in case of ties.
 * The digit-logs should be put in their original order.
 * Return the final order of the logs.
 */

namespace SortLogFileLines
{
    public class Comparer : IComparer<string>
    {
        public int Compare(string log1, string log2)
        {
            var log1StartRhs = log1.IndexOf(' ') + 1;
            var log2StartRhs = log2.IndexOf(' ') + 1;

            var log1DigitLog = char.IsDigit(log1[log1StartRhs]);
            var log2DigitLog = char.IsDigit(log2[log2StartRhs]);
            if (log1DigitLog == false && log2DigitLog == false)
            {
                // SS: reorder if needed
                var log1Rhs = log1.Substring(log1StartRhs);
                var log2Rhs = log2.Substring(log2StartRhs);
                var comp = log1Rhs.CompareTo(log2Rhs);
                if (comp == 0)
                {
                    // SS: resolve tie
                    var log1Id = log1.Substring(0, log1StartRhs);
                    var log2Id = log2.Substring(0, log2StartRhs);
                    comp = log1Id.CompareTo(log2Id);
                }

                return comp;
            }

            return log1DigitLog ? log2DigitLog ? 0 : 1 : -1;
        }
    }

    public class SortLogFileLines2
    {
        public static string[] Sort(string[] logs)
        {
            // SS: O(n log n)

            // SS: Notice, Array.Sort cannot be used as it uses an unstable sorting algorithm.
            // Since we want the order of the digit-logs to be preserved, we have to use a
            // stable sorting algorithm. OrderBy uses a stable sorting algorithm, which is why
            // we use it here!
            return logs.OrderBy(x => x, new Comparer()).ToArray();
        }
    }

    [TestFixture]
    public class SortLogFileLines2Test
    {
        [Test]
        public void TestLeetcode1()
        {
            // Arrange
            var logFileLines = new[]
            {
                "dig1 8 1 5 1"
                , "let1 art can"
                , "dig2 3 6"
                , "let2 own kit dig"
                , "let3 art zero"
            };

            // Act
            var sortedLines = SortLogFileLines2.Sort(logFileLines);

            // Assert
            CollectionAssert.AreEqual(
                new[]
                {
                    "let1 art can"
                    , "let3 art zero"
                    , "let2 own kit dig"
                    , "dig1 8 1 5 1"
                    , "dig2 3 6"
                }, sortedLines);
        }

        [Test]
        public void TestLeetcode2()
        {
            // Arrange
            var logFileLines = new[]
            {
                "l5sh 6 3869 08 1295"
                , "16o 94884717383724 9"
                , "43 490972281212 3 51"
                , "9 ehyjki ngcoobi mi"
                , "2epy 85881033085988"
                , "7z fqkbxxqfks f y dg"
                , "9h4p 5 791738 954209"
                , "p i hz uubk id s m l"
                , "wd lfqgmu pvklkdp u"
                , "m4jl 225084707500464"
                , "6np2 bqrrqt q vtap h"
                , "e mpgfn bfkylg zewmg"
                , "ttzoz 035658365825 9"
                , "k5pkn 88312912782538"
                , "ry9 8231674347096 00"
                , "w 831 74626 07 353 9"
                , "bxao armngjllmvqwn q"
                , "0uoj 9 8896814034171"
                , "0 81650258784962331"
                , "t3df gjjn nxbrryos b"
            };

            // Act
            var sortedLines = SortLogFileLines2.Sort(logFileLines);

            // Assert
            CollectionAssert.AreEqual(
                new[]
                {
                    "bxao armngjllmvqwn q"
                    , "6np2 bqrrqt q vtap h"
                    , "9 ehyjki ngcoobi mi"
                    , "7z fqkbxxqfks f y dg"
                    , "t3df gjjn nxbrryos b"
                    , "p i hz uubk id s m l"
                    , "wd lfqgmu pvklkdp u"
                    , "e mpgfn bfkylg zewmg"
                    , "l5sh 6 3869 08 1295"
                    , "16o 94884717383724 9"
                    , "43 490972281212 3 51"
                    , "2epy 85881033085988"
                    , "9h4p 5 791738 954209"
                    , "m4jl 225084707500464"
                    , "ttzoz 035658365825 9"
                    , "k5pkn 88312912782538"
                    , "ry9 8231674347096 00"
                    , "w 831 74626 07 353 9"
                    , "0uoj 9 8896814034171"
                    , "0 81650258784962331"
                }, sortedLines);
        }

        [Test]
        public void TestWithoutTies()
        {
            // Arrange
            var logFileLines = new[]
            {
                "ab1 abc def"
                , "tg2 ghd ytr"
                , "gf6 123 756 635"
            };

            // Act
            var sortedLines = SortLogFileLines2.Sort(logFileLines);

            // Assert
            Assert.AreEqual(logFileLines[0], sortedLines[0]);
            Assert.AreEqual(logFileLines[1], sortedLines[1]);
            Assert.AreEqual(logFileLines[2], sortedLines[2]);
        }

        [Test]
        public void TestWithTies()
        {
            // Arrange
            var logFileLines = new[]
            {
                "ab1 abc def"
                , "tg2 ghd ytr"
                , "ta2 ghd ytr"
                , "gf6 123 756 635"
            };

            // Act
            var sortedLines = SortLogFileLines2.Sort(logFileLines);

            // Assert
            CollectionAssert.AreEqual(new[] {"ab1 abc def", "ta2 ghd ytr", "tg2 ghd ytr", "gf6 123 756 635"}
                , sortedLines);
        }
    }
}