#region

using System.Text;
using NUnit.Framework;

#endregion

// Problem: 38. Count and Say
// URL: https://leetcode.com/problems/count-and-say/

namespace LeetCode38
{
    public class Solution
    {
        public string CountAndSay(int n)
        {
            // SS: runtime complexity: not sure, quadratic?
            // iterative solution
            if (n == 1)
            {
                return "1";
            }

            var r = new StringBuilder("1");
            var nr = new StringBuilder();
            for (var k = 2; k <= n; k++)
            {
                var i = 0;
                var j = 0;

                while (j < r.Length)
                {
                    while (j < r.Length && r[i] == r[j])
                    {
                        j++;
                    }

                    var cnt = j - i;
                    nr.Append($"{cnt}{r[i]}");

                    i = j;
                }

                r = nr;
                nr = new StringBuilder();
            }

            return r.ToString();
        }

        public string CountAndSay2(int n)
        {
            // SS: runtime complexity: not sure, quadratic?
            if (n == 1)
            {
                return "1";
            }

            var r = CountAndSay2(n - 1);

            var sb = new StringBuilder();

            var i = 0;
            var j = 0;

            while (j < r.Length)
            {
                while (j < r.Length && r[i] == r[j])
                {
                    j++;
                }

                var cnt = j - i;
                sb.Append($"{cnt}{r[i]}");

                i = j;
            }

            var result = sb.ToString();
            return result;
        }

        [TestFixture]
        public class Tests
        {
            [TestCase(1, "1")]
            [TestCase(2, "11")]
            [TestCase(3, "21")]
            [TestCase(4, "1211")]
            [TestCase(7, "13112221")]
            [TestCase(8, "1113213211")]
            [TestCase(9, "31131211131221")]
            public void Test1(int n, string expected)
            {
                // Arrange

                // Act
                var result = new Solution().CountAndSay(n);

                // Assert
                Assert.AreEqual(expected, result);
            }
        }
    }
}