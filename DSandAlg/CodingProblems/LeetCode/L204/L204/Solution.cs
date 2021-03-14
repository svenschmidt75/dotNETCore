#region

using System.Collections.Generic;
using NUnit.Framework;

#endregion

// Problem: 204. Count Primes
// URL: https://leetcode.com/problems/count-primes/

namespace LeetCode
{
    public class Solution
    {
        public int CountPrimes(int n)
        {
            // SS: Sieve of Eratosthenes
            if (n < 2)
            {
                // SS: 0 and 1 are not primes
                return 0;
            }

            var sieve = new int[n + 2];

            // SS: 2 is prime
            for (var i = 2; i * i < n; i++)
            {
                if (sieve[i] == 1)
                {
                    continue;
                }

                for (var j = i * i; j < n; j += i)
                {
                    sieve[j] = 1;
                }
            }

            var nPrimes = 0;

            for (var i = 2; i < n; i++)
            {
                if (sieve[i] == 0)
                {
                    nPrimes++;
                }
            }

            return nPrimes;
        }

        public int CountPrimes2(int n)
        {
            // SS: runtime complexity: O(n)
            // SS: space complexity: O(n)

            if (n < 2)
            {
                // SS: 0 and 1 are not primes
                return 0;
            }

            var notPrime = new HashSet<int>();

            var nPrimes = 0;

            for (var i = 2; i < n; i++)
            {
                if (notPrime.Contains(i))
                {
                    continue;
                }

                nPrimes++;

                var j = 2;
                while (j * i < n)
                {
                    notPrime.Add(i * j);
                    j++;
                }
            }

            return nPrimes;
        }

        [TestFixture]
        public class Tests
        {
            [TestCase(0, 0)]
            [TestCase(1, 0)]
            [TestCase(2, 0)]
            [TestCase(3, 1)]
            [TestCase(4, 2)]
            [TestCase(5, 2)]
            [TestCase(6, 3)]
            [TestCase(10, 4)]
            [TestCase(119875, 11290)]
            public void Test1(int n, int expected)
            {
                // Arrange

                // Act
                var nPrimes = new Solution().CountPrimes(n);

                // Assert
                Assert.AreEqual(expected, nPrimes);
            }
        }
    }
}