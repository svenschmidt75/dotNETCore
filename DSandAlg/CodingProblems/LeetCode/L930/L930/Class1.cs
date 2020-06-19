#region

using System.Collections.Generic;
using NUnit.Framework;

#endregion

namespace L930
{
    public class Solution
    {
        public int NumSubarraysWithSum(int[] A, int S)
        {
            var N = A.Length;
            var P = new int[N + 1];
            for (var i = 0; i < N; ++i) P[i + 1] = P[i] + A[i];

            IDictionary<int, int> count = new Dictionary<int, int>();
            var ans = 0;
            foreach (var x in P)
            {
                // SS: Check whether this x would make sum S 
                if (count.ContainsKey(x))
                {
                    ans += count[x];
                }

                // SS: insert the x that would make the sub equal to S.
                // Later on, when we find it (line above), we increase
                // by that amount.
                if (count.ContainsKey(x + S))
                {
                    count[x + S]++;
                }
                else
                {
                    count[x + S] = 1;
                }
            }

            return ans;
        }
    }

    [TestFixture]
    public class Tests
    {
        [Test]
        public void Test1()
        {
            // Arrange
            var input = new[] {1, 0, 1, 0, 1};

            // Act
            var n = new Solution().NumSubarraysWithSum(input, 2);

            // Assert
            Assert.AreEqual(4, n);
        }

        [Test]
        public void Test2()
        {
            // Arrange
            var input = new[] {0, 0, 0, 0, 0};

            // Act
            var n = new Solution().NumSubarraysWithSum(input, 0);

            // Assert
            Assert.AreEqual(15, n);
        }
    }
}