#region

using System.Collections.Generic;
using NUnit.Framework;

#endregion

// Problem: 282. Expression Add Operators
// URL: https://leetcode.com/problems/expression-add-operators/

namespace LeetCode
{
    public class Solution
    {
        public IList<string> AddOperators(string num, int target)
        {
            return AddOperators1(num, target);
        }

        private static IList<string> AddOperators1(string num, int target)
        {
            var result = new List<string>();

            void Solve(int sIdx, long value, string str, long prevValue)
            {
                if (sIdx == num.Length)
                {
                    if (value == target)
                    {
                        // SS: skip the 1st operator
                        result.Add(str.Substring(1));
                    }

                    return;
                }

                long b = 0;
                for (var i = sIdx; i < num.Length; i++)
                {
                    var d = num[i];
                    b = b * 10 + (d - '0');

                    // SS: for *, revert previous operation as * has higher precedence
                    if (sIdx > 0)
                    {
                        Solve(i + 1, value - prevValue + prevValue * b, $"{str}*{b}", prevValue * b);
                    }

                    // SS: +
                    Solve(i + 1, value + b, $"{str}+{b}", b);

                    if (sIdx > 0)
                    {
                        // SS: -
                        Solve(i + 1, value - b, $"{str}-{b}", -b);
                    }
                    
                    // SS: do not allow 08
                    if (b == 0)
                    {
                        break;
                    }
                }
            }

            Solve(0, 0, "", 0);
            return result;
        }

        [TestFixture]
        public class Tests
        {
            [TestCase("123", 6, new[] {"1*2*3", "1+2+3"})]
            [TestCase("232", 8, new[] {"2*3+2", "2+3*2"})]
            [TestCase("00", 0, new[] {"0*0", "0+0", "0-0"})]
            [TestCase("3456237490", 9191, new string[0])]
            [TestCase("5", 5, new[] {"5"})]
            [TestCase("12412", -45, new[] {"1+2-4*12"})]
            [TestCase("1241279", -342, new[] {"1+2-4*12*7-9"})]
            [TestCase("00089", 72, new[] {"0*0*0+8*9","0*0+0+8*9","0*0-0+8*9","0+0*0+8*9","0+0+0+8*9","0+0-0+8*9","0-0*0+8*9","0-0+0+8*9","0-0-0+8*9"})]
            [TestCase("2147483647", int.MaxValue, new[] {"2147483647"})]
            public void Test(string num, int target, IList<string> expected)
            {
                // Arrange

                // Act
                var result = new Solution().AddOperators(num, target);

                // Assert
                CollectionAssert.AreEqual(expected, result);
            }
        }
    }
}