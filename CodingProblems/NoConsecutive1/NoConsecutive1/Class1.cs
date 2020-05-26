#region

using NUnit.Framework;

#endregion

namespace NoConsecutive1
{
    public static class Class1
    {
        public static int Solve(int n)
        {
            if (n < 1)
            {
                return 0;
            }

            var memoizationArray = new int[n, 2];

            // SS: start with 0
            var c1 = SolveRec(n - 1, 0, memoizationArray);

            // SS: start with 1
            var c2 = SolveRec(n - 1, 1, memoizationArray);

            return c1 + c2;
        }

        private static int SolveRec(int n, int lastChar, int[,] memoizationArray)
        {
            // SS: use memoization to reduce runtime from O(2^n) to O(n)

            if (n == 0)
            {
                return 1;
            }

            if (memoizationArray[n, lastChar] > 0)
            {
                return memoizationArray[n, lastChar];
            }

            if (lastChar == 0)
            {
                // SS: 00 is allowed
                var c1 = SolveRec(n - 1, 0, memoizationArray);
                memoizationArray[n, 0] = c1;

                // SS: 01 is allowed
                var c2 = SolveRec(n - 1, 1, memoizationArray);
                memoizationArray[n, 1] = c1;

                return c1 + c2;
            }
            else
            {
                // SS: 11 is not allowed, but 10 is
                var c1 = SolveRec(n - 1, 0, memoizationArray);
                memoizationArray[n, 0] = c1;
                return c1;
            }
        }
    }

    [TestFixture]
    public class TestClass
    {
        [TestCase(1, 2)]
        [TestCase(2, 3)]
        [TestCase(3, 5)]
        [TestCase(4, 8)]
        [TestCase(5, 13)]
        public void Test(int n, int expectedCount)
        {
            // Arrange

            // Act
            var count = Class1.Solve(n);

            // Assert
            Assert.AreEqual(expectedCount, count);
        }
    }
}