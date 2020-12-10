#region

using NUnit.Framework;

#endregion

// Problem: 65. Valid Number
// URL: https://leetcode.com/problems/valid-number/

namespace LeetCode
{
    public class Solution
    {
        public bool IsNumber(string s)
        {
            // SS: we use a state-machine to parse the input
            // runtime complexity: O(N), N = s.Length
            
            var state = 0;

            var i = 0;

            while (true)
            {
                switch (state)
                {
                    // SS: initial state
                    case 0:
                        // SS: skip leading white-space
                        while (i < s.Length && char.IsWhiteSpace(s[i]))
                        {
                            i++;
                        }

                        if (i == s.Length)
                        {
                            return false;
                        }

                        if (s[i] == '+' || s[i] == '-')
                        {
                            i++;
                            state = 1;
                        }
                        else if (char.IsDigit(s[i]))
                        {
                            i++;
                            state = 2;
                        }
                        else if (s[i] == '.')
                        {
                            i++;
                            state = 3;
                        }
                        else
                        {
                            // SS: invalid state
                            return false;
                        }

                        break;

                    case 1:
                        if (i == s.Length)
                        {
                            return false;
                        }

                        if (char.IsDigit(s[i]))
                        {
                            i++;
                            state = 2;
                        }
                        else if (s[i] == '.')
                        {
                            i++;
                            state = 3;
                        }
                        else
                        {
                            // SS: invalid state
                            return false;
                        }

                        break;

                    case 2:
                        i = IsTrailingNumber(s, i);

                        if (i == s.Length)
                        {
                            return true;
                        }

                        if (i == -1)
                        {
                            return false;
                        }

                        if (s[i] == '.')
                        {
                            i++;
                            state = 4;
                        }
                        else if (s[i] == 'e')
                        {
                            i++;
                            state = 5;
                        }
                        else
                        {
                            // SS: invalid state
                            return false;
                        }

                        break;

                    case 3:
                        if (i == s.Length)
                        {
                            return false;
                        }

                        if (char.IsDigit(s[i]))
                        {
                            i++;
                            state = 4;
                        }
                        else
                        {
                            // SS: invalid state
                            return false;
                        }

                        break;

                    case 4:
                        i = IsTrailingNumber(s, i);

                        if (i == s.Length)
                        {
                            return true;
                        }

                        if (i == -1)
                        {
                            return false;
                        }

                        if (s[i] == 'e')
                        {
                            i++;
                            state = 5;
                        }
                        else
                        {
                            // SS: invalid state
                            return false;
                        }

                        break;

                    case 5:
                        if (i == s.Length)
                        {
                            return false;
                        }

                        if (s[i] == '+' || s[i] == '-')
                        {
                            i++;
                            state = 6;
                        }
                        else if (char.IsDigit(s[i]))
                        {
                            i++;
                            state = 7;
                        }
                        else
                        {
                            // SS: invalid state
                            return false;
                        }

                        break;

                    case 6:
                        if (i == s.Length)
                        {
                            return false;
                        }

                        if (char.IsDigit(s[i]))
                        {
                            i++;
                            state = 7;
                        }
                        else
                        {
                            // SS: invalid state
                            return false;
                        }

                        break;

                    case 7:
                        i = IsTrailingNumber(s, i);
                        return i == s.Length;
                }
            }
        }

        private static int IsTrailingNumber(string s, int idx)
        {
            // SS: check whether s[idx..] is a number. We allow for
            // trailing white-space

            var i = idx;
            while (i < s.Length && char.IsDigit(s[i]))
            {
                i++;
            }

            var k = i;
            while (k < s.Length && char.IsWhiteSpace(s[k]))
            {
                k++;
            }

            return k == s.Length || i == k ? k : -1;
        }

        [TestFixture]
        public class Tests
        {
            [TestCase(".  1", false)]
            [TestCase("1 23", false)]
            [TestCase("", false)]
            [TestCase("+.", false)]
            [TestCase("0", true)]
            [TestCase(" 0.1 ", true)]
            [TestCase(".6", true)]
            [TestCase("13.e10", true)]
            [TestCase("13.", true)]
            [TestCase("-.79", true)]
            [TestCase(".e2", false)]
            [TestCase("9.e4", true)]
            [TestCase(".", false)]
            [TestCase("e", false)]
            [TestCase("+", false)]
            [TestCase("9+", false)]
            [TestCase("e.", false)]
            [TestCase(".e", false)]
            [TestCase("abc", false)]
            [TestCase("1 a", false)]
            [TestCase("2e10", true)]
            [TestCase(" -90e3  ", true)]
            [TestCase(" 1e", false)]
            [TestCase(" e3 ", false)]
            [TestCase(" 6e-1  ", true)]
            [TestCase(" 99.e2.5  ", false)]
            [TestCase("53.5e93", true)]
            [TestCase("1 2.e23", false)]
            [TestCase("12 .e23", false)]
            [TestCase("12. e23", false)]
            [TestCase(" --6 ", false)]
            [TestCase("-+3 ", false)]
            [TestCase("95a54e53", false)]
            public void Test1(string s, bool expected)
            {
                // Arrange

                // Act
                var isNumber = new Solution().IsNumber(s);

                // Assert
                Assert.AreEqual(expected, isNumber);
            }
        }
    }
}