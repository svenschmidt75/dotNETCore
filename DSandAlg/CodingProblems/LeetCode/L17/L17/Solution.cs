#region

using System;
using System.Collections.Generic;
using NUnit.Framework;

#endregion

// Problem: 17. Letter Combinations of a Phone Number
// URL: https://leetcode.com/problems/letter-combinations-of-a-phone-number/

namespace LeetCode17
{
    public class Solution
    {
        private readonly string[] _letters =
        {
            "" // 0
            , "" // 1
            , "abc" // 2
            , "def" // 3
            , "ghi" // 4
            , "jkl" // 5
            , "mno" // 6
            , "pqrs" // 7
            , "tuv" // 8
            , "wxyz" // 9
        };

        public IList<string> LetterCombinations(string digits)
        {
            // SS: runtime complexity: O(3^(N+1))
            // Explanation: The outer loop is executed N times, where N = digits.Length.
            // Let j be the iteration count of the inner loop.
            // j = 0: results.Count = 0, so we create 3 strings for the digit digits[0]
            // j = 1: results.Count = 3, so we create 3 * 3=3^2 strings for the digit digits[1]
            // j = 2: results.Count = 3^2, so we create 3^2 * 3=3^3 strings for the digit digits[2]
            // etc.
            // The inner loop generates strings:
            // 3 + 3^2 + ... + 3^N strings. 
            // 3 + 3^2 + ... + 3^N = 3^N [ 1 + 1/3 + 1/3^2 + ... + 1/3^(N-1) ]
            // Let x = 1 + 1/3 + 1/3^2 + ... + 1/3^(N-1) + ...
            // Then x - 1 = 1/3 + 1/3^2 + ... + 1/3^(N-1) + ...
            // 3(x - 1) = 1 + 1/3 + 1/3^2 + ... + 1/3^(N-1) + ...
            // => 3(x - 1) = x for N -> inf
            // Solving for x gives: x = 3/2
            // In total, we find 3^N [ 1 + 1/3 + 1/3^2 + ... + 1/3^(N-1) ] = 3/2 * 3^N,
            // or 1/2 3^(N + 1) => thus runtime complexity: O(3^(N+1))
            // Notice: some digits map to 4 chars, not just 3, so the runtime complexity
            // is between O(3^(N+1)) and O(4^(N+1)) I think.
            
            if (digits.Length == 0)
            {
                return new List<string>();
            }

            var est = (int) Math.Pow(4, digits.Length);
            var result = new List<string>(est) {""};

            for (var i = 0; i < digits.Length; i++)
            {
                var d = digits[i] - '0';
                var letters = _letters[d];

                var tmpResult = new List<string>(result.Count * 4);

                for (var j = 0; j < result.Count; j++)
                {
                    var s = result[j];

                    for (var k = 0; k < letters.Length; k++)
                    {
                        var n = s + letters[k];
                        tmpResult.Add(n);
                    }
                }

                result = tmpResult;
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
                var digits = "23";

                // Act
                var result = new Solution().LetterCombinations(digits);

                // Assert
                CollectionAssert.AreEquivalent(new[] {"ad", "ae", "af", "bd", "be", "bf", "cd", "ce", "cf"}, result);
            }

            [Test]
            public void Test2()
            {
                // Arrange
                var digits = "";

                // Act
                var result = new Solution().LetterCombinations(digits);

                // Assert
                Assert.IsEmpty(result);
            }

            [Test]
            public void Test3()
            {
                // Arrange
                var digits = "2";

                // Act
                var result = new Solution().LetterCombinations(digits);

                // Assert
                CollectionAssert.AreEquivalent(new[] {"a", "b", "c"}, result);
            }
        }
    }
}