#region

using NUnit.Framework;

#endregion

namespace Leetcode292
{
    public class Solution
    {
        public bool CanWinNim(int n)
        {
            var memoizationArray = new int[2, n + 1];
            return CanWinNimRecursive(n, 0, memoizationArray) == 1;
        }

        public int CanWinNimRecursive(int stones, int depth, int[,] memoizationArray)
        {
            if (stones <= 3)
            {
                memoizationArray[depth % 2, stones] = depth % 2 + 1;
                return depth % 2 + 1;
            }

            if (memoizationArray[depth % 2, stones] > 0)
            {
                return memoizationArray[depth % 2, stones];
            }

            var stones1 = CanWinNimRecursive(stones - 1, depth + 1, memoizationArray);
            if (stones1 == depth % 2 + 1)
            {
                memoizationArray[depth % 2, stones] = depth % 2 + 1;
                return depth % 2 + 1;
            }

            var stones2 = CanWinNimRecursive(stones - 2, depth + 1, memoizationArray);
            if (stones2 == depth % 2 + 1)
            {
                memoizationArray[depth % 2, stones] = depth % 2 + 1;
                return depth % 2 + 1;
            }

            var stones3 = CanWinNimRecursive(stones - 3, depth + 1, memoizationArray);
            if (stones3 == depth % 2 + 1)
            {
                memoizationArray[depth % 2, stones] = depth % 2 + 1;
                return depth % 2 + 1;
            }

            // SS: we did not win
            memoizationArray[depth % 2, stones] = (depth + 1) % 2 + 1;
            return (depth + 1) % 2 + 1;
        }
    }

    [TestFixture]
    public class SolutionTest
    {
        [TestCase(4)]
        [TestCase(9)]
        [TestCase(19)]
        [TestCase(975)]
        [TestCase(659)]
        [TestCase(95)]
        [TestCase(91)]
        public void Test1(int n)
        {
            // Arrange

            // Act
            var result = new Solution().CanWinNim(n);

            // Assert
            Assert.AreEqual(n % 4 > 0, result);
        }
    }
}