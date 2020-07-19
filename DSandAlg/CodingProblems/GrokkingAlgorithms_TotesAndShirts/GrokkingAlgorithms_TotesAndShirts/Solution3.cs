#region

using NUnit.Framework;

#endregion

namespace GrokkingAlgorithms_TotesAndShirts
{
    public class Solution3
    {
        public (int nShirts, int nTotes) Solve(int nFabric, int nButtons)
        {
            // SS: bottom-up dynamic programming, O(N * M) solution
            // N = nFabric, M = nButtons
            // space complexity: O(N)
            int[][] row = {new int[(nFabric + 1) * 2], new int[(nFabric + 1) * 2]};

            var i1 = 0;
            var i2 = 1;

            var buttonIdx = 2;

            while (buttonIdx <= nButtons)
            {
                var fabricIdx = 2;

                while (fabricIdx <= nFabric)
                {
                    var nShirtsPrev1 = row[i1][fabricIdx * 2];
                    var nTotesPrev1 = row[i1][fabricIdx * 2 + 1];
                    var profitPrev1 = 2 * nShirtsPrev1 + 3 * nTotesPrev1;

                    var nShirtsPrev2 = row[i2][(fabricIdx - 1) * 2];
                    var nTotesPrev2 = row[i2][(fabricIdx - 1) * 2 + 1];
                    var profitPrev2 = 2 * nShirtsPrev2 + 3 * nTotesPrev2;

                    var nShirts = profitPrev1 > profitPrev2 ? nShirtsPrev1 : nShirtsPrev2;
                    var nTotes = profitPrev1 > profitPrev2 ? nTotesPrev1 : nTotesPrev2;

                    var nFabricRemaining = fabricIdx - nShirts * 1 - nTotes * 2;
                    var nButtonsRemaining = buttonIdx - nShirts * 5 - nTotes * 2;

                    // SS: check for higher-value item first...
                    if (nFabricRemaining >= 2 && nButtonsRemaining >= 2)
                    {
                        // SS: we can add a tote
                        nTotes++;
                    }
                    else if (nFabricRemaining >= 1 && nButtonsRemaining >= 5)
                    {
                        // SS: we can add a shirt
                        nShirts++;
                    }

                    row[i2][fabricIdx * 2] = nShirts;
                    row[i2][fabricIdx * 2 + 1] = nTotes;

                    fabricIdx++;
                }

                // SS: swap rows
                var tmp = i1;
                i1 = i2;
                i2 = tmp;

                buttonIdx++;
            }

            var maxShirts = row[i1][nFabric * 2];
            var maxTotes = row[i1][nFabric * 2 + 1];

            return (maxShirts, maxTotes);
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
                var (nShirts, nTotes) = new Solution3().Solve(nFabric, nButtons);

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
                var (nShirts, nTotes) = new Solution3().Solve(nFabric, nButtons);

                // Assert
                Assert.AreEqual(1, nShirts);
                Assert.AreEqual(14, nTotes);
            }
        }
    }
}