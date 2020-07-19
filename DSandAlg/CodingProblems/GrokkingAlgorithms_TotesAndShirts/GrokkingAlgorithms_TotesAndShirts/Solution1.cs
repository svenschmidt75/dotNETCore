#region

using NUnit.Framework;

#endregion

namespace GrokkingAlgorithms_TotesAndShirts
{
    public class Solution1
    {
        // For example, suppose your company makes two products, shirts and totes.
        // Shirts need 1 meter of fabric and 5 buttons. Totes need 2 meters of
        // fabric and 2 buttons. You have 11 meters of fabric and 20 buttons.
        // You make $2 per shirt and $3 per tote. How many shirts and totes
        // should you make to maximize your profit?
        public (int nShirts, int nTotes) Solve(int nFabric, int nButtons)
        {
            // SS: Divide and Conquer, O(2^N) solution
            // N = nFabric, M = nButtons
            // We can make at most r1 = min(N / 1, M / 5) shirts and r2 = min(N / 2, M / 2) totes
            // The runtime is bounded by O(2^{r1 + r2})...
            var (nShirts, nTotes) = Solve(nFabric, nButtons, 0, 0);
            return (nShirts, nTotes);
        }

        public (int nShirts, int nTotes) Solve(int nFabric, int nButtons, int nShirts, int nTotes)
        {
//            Console.WriteLine($"{nFabric} / {nButtons}");

            var (nShirts1, nTotes1) = (nShirts, nTotes);
            var (nShirts2, nTotes2) = (nShirts, nTotes);

            // SS: can we make a shirt?
            if (nFabric >= 1 && nButtons >= 5)
            {
                (nShirts1, nTotes1) = Solve(nFabric - 1, nButtons - 5, nShirts + 1, nTotes);
            }

            // SS: can we make a tote?
            if (nFabric >= 2 && nButtons >= 2)
            {
                (nShirts2, nTotes2) = Solve(nFabric - 2, nButtons - 2, nShirts, nTotes + 1);
            }

            var profit1 = nShirts1 * 2 + nTotes1 * 3;
            var profit2 = nShirts2 * 2 + nTotes2 * 3;
            if (profit1 > profit2)
            {
                return (nShirts1, nTotes1);
            }

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
                var (nShirts, nTotes) = new Solution1().Solve(nFabric, nButtons);

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
                var (nShirts, nTotes) = new Solution1().Solve(nFabric, nButtons);

                // Assert
                Assert.AreEqual(1, nShirts);
                Assert.AreEqual(14, nTotes);
            }
        }
    }
}