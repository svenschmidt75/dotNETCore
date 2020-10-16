#region

using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

#endregion

// 1405. Longest Happy String
// https://leetcode.com/problems/longest-happy-string/

namespace L1405
{
    public class Solution
    {
        public string LongestDiverseString(int a, int b, int c)
        {
            // SS: runtime complexity: O(N)
            // space complexity: O(1)
            (int count, char c) na = (a, 'a');
            (int count, char c) nb = (b, 'b');
            (int count, char c) nc = (c, 'c');

            var result = new List<char>(a + b + c);

            // SS: sort, O(1) as only 3 elements!
            var sorted = new[] {na, nb, nc};
            sorted = sorted.OrderByDescending(t => t.count).ToArray();

            // SS: We start with the char that is most frequent.
            // Determine number of groups of 2 chars.
            var nGroups = (sorted[0].count + 1) / 2 - 1;

            // SS: current group
            var group = 0;

            while (sorted[0].count > 0)
            {
                if (sorted[1].count < sorted[2].count)
                {
                    var tmp = sorted[1];
                    sorted[1] = sorted[2];
                    sorted[2] = tmp;
                }

                if (result.Count > 0 && result[^1] == sorted[0].c)
                {
                    break;
                }

                result.Add(sorted[0].c);
                sorted[0].count--;

                if (sorted[0].count > 0)
                {
                    result.Add(sorted[0].c);
                    sorted[0].count--;
                }

                var nFiller = sorted[1].count + sorted[2].count;

                // SS: add filler
                if (sorted[1].count > 0)
                {
                    result.Add(sorted[1].c);
                    sorted[1].count--;
                    nFiller--;
                }

                // SS: do we have enough chars to add a second one?
                if (nFiller > 0 && sorted[1].count >= nGroups - group)
                {
                    result.Add(sorted[1].c);
                    sorted[1].count--;
                    nFiller--;
                }

                if (nFiller >= nGroups - group)
                {
                    if (sorted[2].count > 0)
                    {
                        result.Add(sorted[2].c);
                        sorted[2].count--;
                    }

                    // SS: do we have enough chars to add a second one?
                    if (sorted[2].count > nGroups - group)
                    {
                        result.Add(sorted[2].c);
                        sorted[2].count--;
                    }
                }

                group++;
            }

            var result2 = new string(result.ToArray());
            return result2;
        }

        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1()
            {
                // Arrange

                // Act
                var result = new Solution().LongestDiverseString(1, 1, 7);

                // Assert
                Assert.AreEqual("ccaccbcc", result);
            }

            [Test]
            public void Test2()
            {
                // Arrange

                // Act
                var result = new Solution().LongestDiverseString(2, 2, 1);

                // Assert
                Assert.AreEqual("aabbc", result);
            }

            [Test]
            public void Test3()
            {
                // Arrange

                // Act
                var result = new Solution().LongestDiverseString(7, 1, 0);

                // Assert
                Assert.AreEqual("aabaa", result);
            }

            [Test]
            public void Test4()
            {
                // Arrange

                // Act
                var result = new Solution().LongestDiverseString(2, 4, 1);

                // Assert
                Assert.AreEqual("bbaacbb", result);
            }

            [Test]
            public void Test5()
            {
                // Arrange

                // Act
                var result = new Solution().LongestDiverseString(6, 1, 7);

                // Assert
                Assert.AreEqual("ccaabccaaccaac", result);
            }

            [Test]
            public void Test6()
            {
                // Arrange

                // Act
                var result = new Solution().LongestDiverseString(0, 8, 11);

                // Assert
                Assert.AreEqual("ccbbccbbccbbccbccbc", result);
            }
        }
    }
}