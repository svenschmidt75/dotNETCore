#region

using NUnit.Framework;

#endregion

// 521. Longest Uncommon Subsequence I 
// https://leetcode.com/problems/longest-uncommon-subsequence-i/

namespace L521
{
    public class Solution
    {
        public int FindLUSlength(string a, string b)
        {
            // SS: brute-force would compute all subsequences for both strings and check in
            // descending length whether one is a subsequence of the other.
            // Creating all subsequences runtime complexity: O( 2^{s1.Length} + 2^{s2.Length})
            
            // Here, we use a O(1) solution...
            
            if (a.Length == b.Length)
            {
                if (a == b)
                {
                    // SS: if both strings are equal, they have the same subsequences 
                    return -1;
                }

                // SS: the longest uncommon subsequence is one of the strings itself
                return a.Length;
            }

            // SS: strings have different length
            return a.Length > b.Length ? a.Length : b.Length;
        }

        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1()
            {
                // Arrange
                var s1 = "aba";
                var s2 = "cdc";

                // Act
                var result = new Solution().FindLUSlength(s1, s2);

                // Assert
                Assert.AreEqual(3, result);
            }

            [Test]
            public void Test2()
            {
                // Arrange
                var s1 = "aaa";
                var s2 = "bbb";

                // Act
                var result = new Solution().FindLUSlength(s1, s2);

                // Assert
                Assert.AreEqual(3, result);
            }

            [Test]
            public void Test3()
            {
                // Arrange
                var s1 = "aaa";
                var s2 = "aaa";

                // Act
                var result = new Solution().FindLUSlength(s1, s2);

                // Assert
                Assert.AreEqual(0, result);
            }

            [Test]
            public void Test4()
            {
                // Arrange
                var s1 = "abcd";
                var s2 = "abc";

                // Act
                var result = new Solution().FindLUSlength(s1, s2);

                // Assert
                Assert.AreEqual(4, result);
            }
        }
    }
}