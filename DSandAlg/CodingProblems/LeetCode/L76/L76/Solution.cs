#region

using NUnit.Framework;

#endregion

namespace L76
{
    public class Solution
    {
        public string Solve(string s, string t)
        {
            var windowChars = new Word(t);

            var min = 0;
            var max = s.Length;

            var i = 0;
            var j = 0;

            var foundWindow = false;

            while (i < s.Length && j < s.Length)
            {
                var c = s[j];

                if (windowChars.IsComplete == false)
                {
                    windowChars.AddChar(c);

                    // SS: grow window
                    j++;
                }
                else
                {
                    foundWindow = true;

                    var i2 = i;
                    while (i <= j && windowChars.IsComplete)
                    {
                        i2 = i;
                        windowChars.RemoveChar(s[i]);
                        i++;
                    }

                    if (max - min > j - i2)
                    {
                        min = i2;
                        max = j;
                    }
                }
            }

            // SS: remainder
            if (windowChars.IsComplete)
            {
                var i2 = i;
                while (i <= j && windowChars.IsComplete)
                {
                    i2 = i;
                    windowChars.RemoveChar(s[i]);
                    i++;
                }

                if (max - min > j - i2)
                {
                    min = i2;
                    max = j;
                }

                foundWindow = true;
            }

            return foundWindow ? s.Substring(min, max - min) : string.Empty;
        }
    }

    [TestFixture]
    public class Tests
    {
        [Test]
        public void Test1()
        {
            // Arrange
            var s1 = "ADOBECODEBANC";
            var s2 = "ABC";

            // Act
            var result = new Solution().Solve(s1, s2);

            // Act
            Assert.AreEqual("BANC", result);
        }

        [Test]
        public void Test2()
        {
            // Arrange
            var s1 = "ab";
            var s2 = "a";

            // Act
            var result = new Solution().Solve(s1, s2);

            // Act
            Assert.AreEqual("a", result);
        }

        [Test]
        public void Test3()
        {
            // Arrange
            var s1 = "ab";
            var s2 = "b";

            // Act
            var result = new Solution().Solve(s1, s2);

            // Act
            Assert.AreEqual("b", result);
        }

        [Test]
        public void Test4()
        {
            // Arrange
            var s1 = "abc";
            var s2 = "b";

            // Act
            var result = new Solution().Solve(s1, s2);

            // Act
            Assert.AreEqual("b", result);
        }

        [Test]
        public void Test5()
        {
            // Arrange
            var s1 = "bba";
            var s2 = "ab";

            // Act
            var result = new Solution().Solve(s1, s2);

            // Act
            Assert.AreEqual("ba", result);
        }

        [Test]
        public void Test6()
        {
            // Arrange
            var s1 = "bbaac";
            var s2 = "aba";

            // Act
            var result = new Solution().Solve(s1, s2);

            // Act
            Assert.AreEqual("baa", result);
        }
    }
}