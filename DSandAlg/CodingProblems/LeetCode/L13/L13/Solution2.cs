#region

using System.Collections.Generic;
using NUnit.Framework;

#endregion

// Problem: 13. Roman to Integer
// URL: https://leetcode.com/problems/roman-to-integer/

namespace LeetCode13
{
    public class Solution2
    {
        private readonly IDictionary<char, int> _map = new Dictionary<char, int>
        {
            {'I', 0}
            , {'V', 1}
            , {'X', 2}
            , {'L', 3}
            , {'C', 4}
            , {'D', 5}
            , {'M', 6}
        };

        private readonly int[] _values = {1, 5, 10, 50, 100, 500, 1000};

        public int RomanToInt(string s)
        {
            var result = 0;

            for (var i = 0; i < s.Length; i++)
            {
                var c = s[i];
                var idx = _map[c];
                var v = _values[idx];

                if (c == 'I' || c == 'X' || c == 'C')
                {
                    if (i <= s.Length - 2)
                    {
                        var c2 = s[i + 1];
                        var idx2 = _map[c2];
                        if (idx2 == idx + 1 || idx2 == idx + 2)
                        {
                            v *= -1;
                        }
                    }
                }

                result += v;
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
                var s = "III";

                // Act
                var result = new Solution().RomanToInt(s);

                // Assert
                Assert.AreEqual(3, result);
            }

            [Test]
            public void Test2()
            {
                // Arrange
                var s = "IV";

                // Act
                var result = new Solution().RomanToInt(s);

                // Assert
                Assert.AreEqual(4, result);
            }

            [Test]
            public void Test3()
            {
                // Arrange
                var s = "LVIII";

                // Act
                var result = new Solution().RomanToInt(s);

                // Assert
                Assert.AreEqual(58, result);
            }

            [Test]
            public void Test4()
            {
                // Arrange
                var s = "IX";

                // Act
                var result = new Solution().RomanToInt(s);

                // Assert
                Assert.AreEqual(9, result);
            }

            [Test]
            public void Test5()
            {
                // Arrange
                var s = "MCMXCIV";

                // Act
                var result = new Solution().RomanToInt(s);

                // Assert
                Assert.AreEqual(1994, result);
            }
        }
    }
}