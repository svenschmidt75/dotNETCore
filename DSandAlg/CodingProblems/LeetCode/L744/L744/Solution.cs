#region

using NUnit.Framework;

#endregion

// Problem: 744. Find Smallest Letter Greater Than Target
// URL: https://leetcode.com/problems/find-smallest-letter-greater-than-target/

namespace LeetCode
{
    public class Solution
    {
        public char NextGreatestLetter(char[] letters, char target)
        {
            // SS: This problem is finding the upper bound, so we can
            // use Binary Search because the input is sorted.
            // runtime complexity: O(log n)
            // space complexity: O(1)

            var min = 0;
            var max = letters.Length;

            var targetNumber = target - 'a';

            while (min < max)
            {
                var mid = (min + max) / 2;
                var v = letters[mid] - 'a';

                if (v <= targetNumber)
                {
                    // SS: go to the right
                    min = mid + 1;
                }
                else
                {
                    max = mid;
                }
            }

            if (min == letters.Length)
            {
                // SS: wrap-around
                return letters[0];
            }

            return letters[min];
        }

        [TestFixture]
        public class Tests
        {
            [TestCase(new[] {'c', 'f', 'j'}, 'a', 'c')]
            [TestCase(new[] {'c', 'f', 'j'}, 'c', 'f')]
            [TestCase(new[] {'c', 'f', 'j'}, 'd', 'f')]
            [TestCase(new[] {'c', 'f', 'j'}, 'g', 'j')]
            [TestCase(new[] {'c', 'f', 'j'}, 'j', 'c')]
            [TestCase(new[] {'c', 'f', 'j'}, 'k', 'c')]
            [TestCase(new[] {'a', 'f', 'f', 'z', 'z'}, 'a', 'f')]
            [TestCase(new[] {'a', 'f', 'f', 'f', 'z'}, 'f', 'z')]
            public void Test(char[] letters, char target, char expected)
            {
                // Arrange

                // Act
                var result = new Solution().NextGreatestLetter(letters, target);

                // Assert
                Assert.AreEqual(expected, result);
            }
        }
    }
}