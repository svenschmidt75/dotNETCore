#region

using NUnit.Framework;

#endregion

namespace JumpGame
{
    public class Solution
    {
        public bool SolveCC(int[] arr, int maxStep)
        {
            if (maxStep == 0 || arr.Length == 0)
            {
                return false;
            }

            // SS: runtime complexity is O(maxStep^N)
            // space complexity: O(N) (call stack)
            return SolveCC(arr, 0, maxStep);
        }

        private bool SolveCC(int[] arr, int position, int maxStep)
        {
            if (position == arr.Length - 1)
            {
                return true;
            }

            if (position >= arr.Length)
            {
                return false;
            }

            if (arr[position] == 1)
            {
                // SS: we hit an obstacle
                return false;
            }

            // SS: try all steps
            for (var step = 1; step <= maxStep; step++)
            {
                if (SolveCC(arr, position + step, maxStep))
                {
                    return true;
                }
            }

            return false;
        }

        public bool SolveBottomUp(int[] arr, int maxStep)
        {
            // SS: Bottom-Up Dynamic Programming approach
            // runtime complexity: O(N * maxStep)
            // space complexity: O(N)
            // can be optimized to O(maxStep)
            if (maxStep == 0 || arr.Length == 0)
            {
                return false;
            }

            var memArray = new int[arr.Length];
            memArray[^1] = 1;

            for (var i = memArray.Length - 1; i >= 0; i--)
            {
                if (memArray[i] == 0)
                {
                    // SS: obstacle
                    continue;
                }

                for (var j = 0; j <= maxStep; j++)
                {
                    if (i - j < 0)
                    {
                        break;
                    }

                    memArray[i - j] = arr[i - j] == 1 ? 0 : 1;
                }
            }

            return memArray[0] == 1;
        }

        [TestFixture]
        public class Tests
        {
            [TestCase(1, false)]
            [TestCase(2, false)]
            [TestCase(3, true)]
            public void Test11(int maxStep, bool expectedResult)
            {
                // Arrange
                var arr = new[] {0, 1, 0, 1, 0, 0, 1, 1, 0, 1, 0, 0, 0};

                // Act
                var result = new Solution().SolveCC(arr, maxStep);

                // Assert
                Assert.AreEqual(expectedResult, result);
            }

            [TestCase(1, false)]
            [TestCase(2, false)]
            [TestCase(3, true)]
            public void Test12(int maxStep, bool expectedResult)
            {
                // Arrange
                var arr = new[] {0, 1, 0, 1, 0, 0, 1, 1, 0, 1, 0, 0, 0};

                // Act
                var result = new Solution().SolveBottomUp(arr, maxStep);

                // Assert
                Assert.AreEqual(expectedResult, result);
            }
        }
    }
}