#region

using System;
using NUnit.Framework;

#endregion

namespace L7
{
    public class Solution
    {
        public int Reverse(int x)
        {
            if (x == 0)
            {
                return 0;
            }

            var sign = x < 0 ? -1 : 1;

            var v = x < 0 ? -x : x;

            var exp1 = (int) Math.Pow(10, (int) Math.Log10(v));
            var exp2 = 1;

            var result = 0;

            while (v > 0)
            {
                var digit = v / exp1;
                v -= digit * exp1;
                exp1 /= 10;

                // SS: alternatively, we could put this into a checked statement and
                // catch OverflowException...
                var tmp = result + digit * (long) exp2;
                if (tmp > int.MaxValue)
                {
                    return 0;
                }

                result = (int) tmp;
                exp2 *= 10;
            }

            return sign * result;
        }

        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1()
            {
                // Arrange
                var x = 123;

                // Act
                var result = new Solution().Reverse(x);

                // Assert
                Assert.AreEqual(321, result);
            }

            [Test]
            public void Test2()
            {
                // Arrange
                var x = -123;

                // Act
                var result = new Solution().Reverse(x);

                // Assert
                Assert.AreEqual(-321, result);
            }

            [Test]
            public void Test3()
            {
                // Arrange
                var x = 120;

                // Act
                var result = new Solution().Reverse(x);

                // Assert
                Assert.AreEqual(21, result);
            }

            [Test]
            public void Test4()
            {
                // Arrange
                var x = 0;

                // Act
                var result = new Solution().Reverse(x);

                // Assert
                Assert.AreEqual(0, result);
            }

            [Test]
            public void Test5()
            {
                // Arrange
                var x = 1534236469;

                // Act
                var result = new Solution().Reverse(x);

                // Assert
                Assert.AreEqual(0, result);
            }
        }
    }
}