#region

using System;
using NUnit.Framework;

#endregion

// Problem: 50. Pow(x, n)
// URL: https://leetcode.com/problems/powx-n/

namespace LeetCode50
{
    public class Solution
    {
        public double MyPow(double x, int n)
        {
            // SS: runtime performance: O(log n)
            // space complexity: O(log n)
            // The idea is to decompose for example:
            // 1.01^478 = 1.01^(2^8) * 1.01^(2^7) * 1.01^(2^6) * 1.01^(2^4) * 1.01^(2^3) * 1.01^(2^2) * 1.01^(2^1)

            if (n == 0)
            {
                return 1;
            }

            if (x == 1)
            {
                return x;
            }

            var dp = new double[32 + 1];
            dp[0] = 1;
            dp[1] = n < 0 ? 1 / x : x;

            var dp2 = new long[32 + 1];
            dp2[0] = 1;
            dp2[1] = 2;

            var n2 = n < 0 ? -(long) n : n;
            var exp = (int) (Math.Log(n2) / Math.Log(2));

            var result = n < 0 ? 1 / x : x;
            for (var i = 0; i < exp; i++)
            {
                result *= result;
                dp[i + 2] = result;
                dp2[i + 2] = 2 * dp2[i + 1];
            }

            result = 1;
            var e = n2;
            while (e > 0)
            {
                var remainderExp = (int) (Math.Log(e) / Math.Log(2));
                result *= dp[remainderExp + 1];
                e -= dp2[remainderExp];
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

                // Act
                var result = new Solution().MyPow(2.0, 10);

                // Assert
                Assert.That(result, Is.EqualTo(1024));
            }

            [Test]
            public void Test2()
            {
                // Arrange

                // Act
                var result = new Solution().MyPow(2.0, -2);

                // Assert
                Assert.That(result, Is.EqualTo(1 / 4.0));
            }

            [Test]
            public void Test3()
            {
                // Arrange

                // Act
                var result = new Solution().MyPow(1.0, int.MaxValue);

                // Assert
                Assert.That(result, Is.EqualTo(1));
            }

            [Test]
            public void Test4()
            {
                // Arrange

                // Act
                var result = new Solution().MyPow(2.0, int.MinValue);

                // Assert
                Assert.That(result, Is.EqualTo(0));
            }

            [Test]
            public void Test5()
            {
                // Arrange

                // Act
                var result = new Solution().MyPow(2.0, int.MinValue + 1);

                // Assert
                Assert.That(result, Is.EqualTo(0));
            }

            [Test]
            public void Test6()
            {
                // Arrange

                // Act
                var result = new Solution().MyPow(2.0, 16);

                // Assert
                Assert.That(result, Is.EqualTo(65536));
            }

            [Test]
            public void Test7()
            {
                // Arrange

                // Act
                var result = new Solution().MyPow(2.0, 16 + 1);

                // Assert
                Assert.That(result, Is.EqualTo(2 * 65536));
            }

            [Test]
            public void Test8()
            {
                // Arrange

                // Act
                var result = new Solution().MyPow(2.0, 12);

                // Assert
                Assert.That(result, Is.EqualTo(4096));
            }

            [Test]
            public void Test9()
            {
                // Arrange

                // Act
                var result = new Solution().MyPow(1.01, 478);

                // Assert
                Assert.That(result, Is.EqualTo(116.3098961891139338).Within(1E-6));
            }

            [Test]
            public void Test10()
            {
                // Arrange

                // Act
                var result = new Solution().MyPow(2, -11);

                // Assert
                Assert.That(result, Is.EqualTo(0.00048828125).Within(1E-6));
            }
        }
    }
}