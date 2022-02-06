using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace L76
{
    // Problem: Smallest Substring of all characters
    // URL: https://www.youtube.com/watch?v=5xuvqBjRkok

    public class SolutionBloomberg
    {
        public string GetShortestUniqueSubstring(char[] arr, string str)
        {
            return GetShortestUniqueSubstring1(arr, str);
        }

        private string GetShortestUniqueSubstring1(char[] arr, string str)
        {
            // SS: sliding window technique
            // runtime complexity: O(max(str, arr))
            // space complexity: O(arr)

            int i = 0;
            int j = 0;

            int min = -1;
            int max = -1;

            int smallestLength = int.MaxValue;

            var map = new Dictionary<char, int>();

            // SS: O(a)
            var set = new HashSet<char>(arr);

            int count = 0;

            while (i < str.Length)
            {
                // SS: case 1: all characters are there
                if (count == arr.Length)
                {
                    if (j - i < smallestLength)
                    {
                        min = i;
                        max = j;
                        smallestLength = j - i;
                    }

                    // SS: shrink window
                    char c = str[i];

                    // SS: only keep track of chars in arr
                    if (set.Contains(c))
                    {
                        map[c]--;
                        if (map[c] == 0)
                        {
                            map.Remove(c);
                            count--;
                        }
                    }

                    i++;
                }
                else if (j == str.Length)
                {
                    // SS: we cannot grow the window any further to the right
                    // as we have exhausted all characters
                    break;
                }
                else
                {
                    // SS: incomplete
                    char c = str[j];

                    // SS: is c in arr?
                    if (set.Contains(c))
                    {
                        if (map.ContainsKey(c))
                        {
                            map[c]++;
                        }
                        else
                        {
                            map[c] = 1;
                            count++;
                        }
                    }

                    j++;
                }
            }

            return min == -1 ? "" : str[min..max];
        }

        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1()
            {
                // Arrange
                var s1 = new[] { 'x', 'y', 'z' };
                var s2 = "xyyzyzyx";

                // Act
                var result = new SolutionBloomberg().GetShortestUniqueSubstring(s1, s2);

                // Act
                Assert.AreEqual("zyx", result);
            }

            [Test]
            public void Test2()
            {
                // Arrange
                var s1 = new[] { 'x', 'y', 'z' };
                var s2 = "xyyzyzypqx";

                // Act
                var result = new SolutionBloomberg().GetShortestUniqueSubstring(s1, s2);

                // Act
                Assert.AreEqual("xyyz", result);
            }

            [Test]
            public void Test3()
            {
                // Arrange
                var s1 = new[] { 'x', 'y', 'z' };
                var s2 = "";

                // Act
                var result = new SolutionBloomberg().GetShortestUniqueSubstring(s1, s2);

                // Act
                Assert.AreEqual("", result);
            }

            [Test]
            public void Test4()
            {
                // Arrange
                var s1 = new[] { 'x', 'y', 'z' };
                var s2 = "xyysyyya";

                // Act
                var result = new SolutionBloomberg().GetShortestUniqueSubstring(s1, s2);

                // Act
                Assert.AreEqual("", result);
            }
        }
    }
}