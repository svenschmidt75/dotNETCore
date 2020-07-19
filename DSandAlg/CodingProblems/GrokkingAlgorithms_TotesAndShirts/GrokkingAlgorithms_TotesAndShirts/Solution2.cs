#region

using System;
using NUnit.Framework;

#endregion

namespace GrokkingAlgorithms_TotesAndShirts
{
    public class Solution2
    {
        public (int nShirts, int nTotes) Solve(int nFabric, int nButtons)
        {
            // SS: top-down dynamic programming, O(N * M) solution
            // N = nFabric, M = nButtons
            // space complexity: O(M * N)
            var memoizationArray = new int[nButtons + 1][];
            for (var i = 0; i <= nButtons; i++)
            {
                memoizationArray[i] = new int[(nFabric + 1) * 2];
            }

            var (nShirts, nTotes) = Solve(nFabric, nButtons, 0, 0, memoizationArray);
            return (nShirts, nTotes);
        }

        public (int nShirts, int nTotes) Solve(int nFabric, int nButtons, int nShirts, int nTotes
            , int[][] memoizationArray)
        {
            var nS = memoizationArray[nButtons][nFabric * 2];
            var nT = memoizationArray[nButtons][nFabric * 2 + 1];
            if (nS > 0 || nT > 0)
            {
                return (nS, nT);
            }

            Console.WriteLine($"{nFabric} / {nButtons}");

            var (nShirts1, nTotes1) = (nShirts, nTotes);
            var (nShirts2, nTotes2) = (nShirts, nTotes);

            // SS: can we make a shirt?
            if (nFabric >= 1 && nButtons >= 5)
            {
                (nShirts1, nTotes1) = Solve(nFabric - 1, nButtons - 5, nShirts + 1, nTotes, memoizationArray);
            }

            // SS: can we make a tote?
            if (nFabric >= 2 && nButtons >= 2)
            {
                (nShirts2, nTotes2) = Solve(nFabric - 2, nButtons - 2, nShirts, nTotes + 1, memoizationArray);
            }

            var profit1 = nShirts1 * 2 + nTotes1 * 3;
            var profit2 = nShirts2 * 2 + nTotes2 * 3;
            if (profit1 > profit2)
            {
                memoizationArray[nButtons][nFabric * 2] = nShirts1;
                memoizationArray[nButtons][nFabric * 2 + 1] = nTotes1;
                return (nShirts1, nTotes1);
            }

            memoizationArray[nButtons][nFabric * 2] = nShirts2;
            memoizationArray[nButtons][nFabric * 2 + 1] = nTotes2;
            return (nShirts2, nTotes2);
        }

        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1()
            {
                // Arrange
                var nFabric = 11;
                var nButtons = 20;

                // Act
                var (nShirts, nTotes) = new Solution2().Solve(nFabric, nButtons);

                // Assert
                Assert.AreEqual(1, nShirts);
                Assert.AreEqual(5, nTotes);
            }

            [Test]
            public void Test2()
            {
                // Arrange
                var nFabric = 29;
                var nButtons = 33;

                // Act
                var (nShirts, nTotes) = new Solution2().Solve(nFabric, nButtons);

                // Assert
                Assert.AreEqual(1, nShirts);
                Assert.AreEqual(14, nTotes);
            }
        }
    }
}