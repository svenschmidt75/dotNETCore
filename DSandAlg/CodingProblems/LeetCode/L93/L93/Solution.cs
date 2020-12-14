#region

using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

#endregion

// Problem: 93. Restore IP Addresses
// URL: https://leetcode.com/problems/restore-ip-addresses/

namespace Leetcode
{
    public class Solution
    {
        public IList<string> RestoreIpAddresses(string s)
        {
            // SS: solve using backtracking
            if (s.Length < 4 || s.Length > 4 * 3)
            {
                return new List<string>();
            }

            var results = new List<string>();

            void Solve(int index, int groupNr, List<char> result)
            {
                if (groupNr == 4 && index == s.Length)
                {
                    var ip = new string(result.ToArray());
                    results.Add(ip);
                    return;
                }

                if (index == s.Length)
                {
                    return;
                }

                var r = new List<char>(result);

                if (groupNr > 0)
                {
                    r.Add('.');
                }

                var c = s[index];
                r.Add(c);
                Solve(index + 1, groupNr + 1, r);

                if (c == '0')
                {
                    // SS: no leading 0s allowed
                    return;
                }

                if (index <= s.Length - 2)
                {
                    var c2 = s[index + 1];
                    r.Add(c2);
                    Solve(index + 2, groupNr + 1, r);

                    if (index <= s.Length - 3)
                    {
                        var c3 = s[index + 2];

                        var num = (c - '0') * 100 + (c2 - '0') * 10 + (c3 - '0');
                        if (num <= 255)
                        {
                            r.Add(c3);
                            Solve(index + 3, groupNr + 1, r);
                        }
                    }
                }
            }

            Solve(0, 0, new List<char>());

            return results;
        }

        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1()
            {
                // Arrange
                var s = "0000";

                // Act
                var results = new Solution().RestoreIpAddresses(s);

                // Assert
                CollectionAssert.AreEquivalent(new[] {"0.0.0.0"}, results);
            }

            [Test]
            public void Test2()
            {
                // Arrange
                var s = "123";

                // Act
                var results = new Solution().RestoreIpAddresses(s);

                // Assert
                Assert.False(results.Any());
            }

            [Test]
            public void Test3()
            {
                // Arrange
                var s = "25525511135";

                // Act
                var results = new Solution().RestoreIpAddresses(s);

                // Assert
                CollectionAssert.AreEquivalent(new[] {"255.255.11.135", "255.255.111.35"}, results);
            }

            [Test]
            public void Test4()
            {
                // Arrange
                var s = "1111";

                // Act
                var results = new Solution().RestoreIpAddresses(s);

                // Assert
                CollectionAssert.AreEquivalent(new[] {"1.1.1.1"}, results);
            }

            [Test]
            public void Test5()
            {
                // Arrange
                var s = "010010";

                // Act
                var results = new Solution().RestoreIpAddresses(s);

                // Assert
                CollectionAssert.AreEquivalent(new[] {"0.10.0.10", "0.100.1.0"}, results);
            }

            [Test]
            public void Test6()
            {
                // Arrange
                var s = "101023";

                // Act
                var results = new Solution().RestoreIpAddresses(s);

                // Assert
                CollectionAssert.AreEquivalent(new[] {"1.0.10.23", "1.0.102.3", "10.1.0.23", "10.10.2.3", "101.0.2.3"}, results);
            }
            
        }
    }
}