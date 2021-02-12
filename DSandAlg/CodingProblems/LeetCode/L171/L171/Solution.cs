using NUnit.Framework;

// Problem: 171. Excel Sheet Column Number
// URL: https://leetcode.com/problems/excel-sheet-column-number/

namespace LeetCode
{
    public class Solution
    {
        public int TitleToNumber(string s)
        {
            // SS: runtime complexity: O(s)
            // space complexity: O(1)
            
            int exp = 1;
            int result = 0;

            for (int i = s.Length - 1; i >= 0; i--)
            {
                char c = s[i];
                int coefficient = c - 'A' + 1;
                result += exp * coefficient;
                exp *= 26;
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
                string s = "A";

                // Act
                int value = new Solution().TitleToNumber(s);

                // Assert
                Assert.AreEqual(1, value);
            }
            
            [Test]
            public void Test2()
            {
                // Arrange
                string s = "AB";

                // Act
                int value = new Solution().TitleToNumber(s);

                // Assert
                Assert.AreEqual(28, value);
            }
            
            [Test]
            public void Test3()
            {
                // Arrange
                string s = "ZY";

                // Act
                int value = new Solution().TitleToNumber(s);

                // Assert
                Assert.AreEqual(701, value);
            }
            
        }
    }
}