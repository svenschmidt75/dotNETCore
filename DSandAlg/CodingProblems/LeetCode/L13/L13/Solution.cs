#region

using NUnit.Framework;

#endregion

// Problem: 13. Roman to Integer
// URL: https://leetcode.com/problems/roman-to-integer/

namespace LeetCode13
{
    public class Solution
    {
        public int RomanToInt(string s)
        {
            var result = 0;

            for (var i = 0; i < s.Length; i++)
            {
                var c = s[i];

                var v = 0;

                if (c == 'I')
                {
                    v = 1;

                    if (i <= s.Length - 2)
                    {
                        var c2 = s[i + 1];
                        if (c2 == 'V' || c2 == 'X')
                        {
                            v = -1;
                        }
                    }
                }
                else if (c == 'X')
                {
                    v = 10;

                    if (i <= s.Length - 2)
                    {
                        var c2 = s[i + 1];
                        if (c2 == 'L' || c2 == 'C')
                        {
                            v = -10;
                        }
                    }
                }
                else if (c == 'C')
                {
                    v = 100;

                    if (i <= s.Length - 2)
                    {
                        var c2 = s[i + 1];
                        if (c2 == 'D' || c2 == 'M')
                        {
                            v = -100;
                        }
                    }
                }
                else if (c == 'V')
                {
                    v = 5;
                }
                else if (c == 'X')
                {
                    v = 10;
                }
                else if (c == 'L')
                {
                    v = 50;
                }
                else if (c == 'C')
                {
                    v = 100;
                }
                else if (c == 'D')
                {
                    v = 500;
                }
                else if (c == 'M')
                {
                    v = 1000;
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