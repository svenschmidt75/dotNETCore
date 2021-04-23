#region

using System.Collections.Generic;
using NUnit.Framework;

#endregion

// Problem: 299. Bulls and Cows
// URL: https://leetcode.com/problems/bulls-and-cows/

namespace LeetCode
{
    public class Solution
    {
        public string GetHint(string secret, string guess)
        {
            // SS: runtime complexity: O(n)
            // SS: space complexity: O(n)

            // SS: unmatched secret, potential cow
            var sDict = new Dictionary<char, int>();

            // SS: unmatched guess, potential cow
            var gDict = new Dictionary<char, int>();

            var nCows = 0;
            var nBulls = 0;

            for (var i = 0; i < secret.Length; i++)
            {
                var sc = secret[i];
                var gc = guess[i];

                if (sc == gc)
                {
                    nBulls++;
                }
                else
                {
                    // SS: insert as potential cow
                    if (sDict.ContainsKey(sc) == false)
                    {
                        sDict[sc] = 1;
                    }
                    else
                    {
                        sDict[sc]++;
                    }

                    // SS: insert as potential cow
                    if (gDict.ContainsKey(gc) == false)
                    {
                        gDict[gc] = 1;
                    }
                    else
                    {
                        gDict[gc]++;
                    }

                    // SS: is the guess in the secret at a prior position? 
                    if (sDict.TryGetValue(gc, out var count))
                    {
                        // SS: yes, cow
                        nCows++;

                        sDict[gc]--;
                        if (sDict[gc] == 0)
                        {
                            sDict.Remove(gc);
                        }

                        gDict[gc]--;
                        if (gDict[gc] == 0)
                        {
                            gDict.Remove(gc);
                        }
                    }

                    // SS: is the secret in the guess at a prior position? 
                    if (gDict.TryGetValue(sc, out count))
                    {
                        // SS: yes, cow
                        nCows++;

                        gDict[sc]--;
                        if (gDict[sc] == 0)
                        {
                            gDict.Remove(sc);
                        }

                        sDict[sc]--;
                        if (sDict[sc] == 0)
                        {
                            sDict.Remove(sc);
                        }
                    }
                }
            }

            return $"{nBulls}A{nCows}B";
        }

        [TestFixture]
        public class Tests
        {
            [TestCase("1807", "7810", "1A3B")]
            [TestCase("1123", "0111", "1A1B")]
            [TestCase("1", "0", "0A0B")]
            [TestCase("1", "1", "1A0B")]
            [TestCase("2962", "7236", "0A2B")]
            public void Test(string secret, string guess, string expectedHint)
            {
                // Arrange

                // Act
                var hint = new Solution().GetHint(secret, guess);

                // Assert
                Assert.AreEqual(expectedHint, hint);
            }
        }
    }
}