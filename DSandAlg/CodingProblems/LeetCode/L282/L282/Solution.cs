using System;
using System.Collections.Generic;
using NUnit.Framework;

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

            void Solve(int sIdx, Func<int, int> eval)
            {
                if (sIdx == num.Length)
                {
                    if (eval == target)
                    {
                        // SS: skip the 1st operator
                        result.Add(v.Substring(1));
                    }
                    return;
                }

                int b1 = 0;
                int b2 = int.Parse(num[sIdx..]); 
                for (int i = sIdx; i < num.Length; i++)
                {
                    char d = num[i];
                    b1 = b1 * 10 + (d - '0');
                    b2 -= (d - '0') * (int)Math.Pow(10, num.Length - 1);
                    
                    Solve(i + 1, arg => eval(b1 * arg));
                }
            }
            
            Solve(0, 0, "");

            return result;
        }

        [TestFixture]
        public class Tests
        {
            [TestCase("123", 6, new[]{"1*2*3", "1+2+3"})]
            [TestCase("232", 8, new[]{"2*3+2", "2+3*2"})]
            [TestCase("00", 0, new[]{"0*0", "0+0", "0-0"})]
            [TestCase("3456237490", 9191, new string[0])]
            [TestCase("5", 5, new []{"5"})]
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