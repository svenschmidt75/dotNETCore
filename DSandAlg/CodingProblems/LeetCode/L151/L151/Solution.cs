#region

using NUnit.Framework;

#endregion

// Problem: 151. Reverse Words in a String
// URL: https://leetcode.com/problems/reverse-words-in-a-string/

namespace LeetCode
{
    public class Solution
    {
        public string ReverseWords(string s)
        {
            // SS: O(s) runtime complexity
            // in-place doesn't work as string is immutable 
            var result = new char[s.Length];
            var i = s.Length - 1;
            var hasWord = false;
            var k = 0;
            while (true)
            {
                // SS: skip leading white space
                var j = SkipWhiteSpace(s, i);
                if (j == -1)
                {
                    break;
                }

                // SS: find end of word
                var l = j;
                while (l >= 0 && char.IsWhiteSpace(s[l]) == false)
                {
                    l--;
                }

                var p = l;

                l++;

                if (hasWord)
                {
                    result[k++] = ' ';
                }
                hasWord = true;

                while (l <= j)
                {
                    result[k++] = s[l++];
                }

                i = p;
            }

            var r = new string(result, 0, k);
            return r;
        }

        private static int SkipWhiteSpace(string s, int idx)
        {
            var i = idx;
            while (i >= 0 && char.IsWhiteSpace(s[i]))
            {
                i--;
            }

            return i;
        }

        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1()
            {
                // Arrange
                var s = "the sky is blue";

                // Act
                var result = new Solution().ReverseWords(s);

                // Assert
                Assert.AreEqual("blue is sky the", result);
            }

            [Test]
            public void Test2()
            {
                // Arrange
                var s = "  hello world  ";

                // Act
                var result = new Solution().ReverseWords(s);

                // Assert
                Assert.AreEqual("world hello", result);
            }

            [Test]
            public void Test3()
            {
                // Arrange
                var s = "a good   example";

                // Act
                var result = new Solution().ReverseWords(s);

                // Assert
                Assert.AreEqual("example good a", result);
            }

            [Test]
            public void Test4()
            {
                // Arrange
                var s = "  Bob    Loves  Alice   ";

                // Act
                var result = new Solution().ReverseWords(s);

                // Assert
                Assert.AreEqual("Alice Loves Bob", result);
            }

            [Test]
            public void Test5()
            {
                // Arrange
                var s = "Alice does not even like bob";

                // Act
                var result = new Solution().ReverseWords(s);

                // Assert
                Assert.AreEqual("bob like even not does Alice", result);
            }

            [Test]
            public void Test6()
            {
                // Arrange
                var s = " a";

                // Act
                var result = new Solution().ReverseWords(s);

                // Assert
                Assert.AreEqual("a", result);
            }

            [Test]
            public void Test7()
            {
                // Arrange
                var s = "a ";

                // Act
                var result = new Solution().ReverseWords(s);

                // Assert
                Assert.AreEqual("a", result);
            }
            
        }
    }
}