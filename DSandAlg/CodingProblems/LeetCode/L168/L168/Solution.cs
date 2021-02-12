using System.Linq;
using System.Text;
using NUnit.Framework;

// Problem: 168. Excel Sheet Column Title
// URL: https://leetcode.com/problems/excel-sheet-column-title/

namespace LeetCode
{
    public class Solution
    {
        public string ConvertToTitle(int n)
        {
            // SS: convert n to base26 representation, where the coefficients
            // are represented by A -> 1, ..., Z -> 26.
            // The coefficients are 1-based which makes it a little harder...

            string result = "";

            while (n > 0)
            {
                int v = (n - 1) % 26;
                char c = (char)('A' + v);
                result = c + result;
                n = (n - 1) / 26;
            }

            return result;
        }
        
        [TestFixture]
        public class Tests
        {
            [TestCase(1, "A")]
            [TestCase(28, "AB")]
            [TestCase(701, "ZY")]
            [TestCase(17465, "YUS")]
            public void Test1(int n, string expected)
            {
                // Arrange

                // Act

                // Assert
                Assert.AreEqual(expected, new Solution().ConvertToTitle(n));
            }
        }
    }
}