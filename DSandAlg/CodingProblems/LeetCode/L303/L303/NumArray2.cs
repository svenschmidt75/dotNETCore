using NUnit.Framework;

namespace LeetCode
{
    public class NumArray2
    {
        private readonly int[] _dp;

        public NumArray2(int[] nums)
        {
            _dp = new int[nums.Length + 1];

            // SS: create prefix sum
            // button-up dynamic programming
            int sum = 0;
            for (var i = 0; i < nums.Length; i++)
            {
                var v = nums[i];
                sum += v;
                _dp[i + 1] = sum;
            }
        }

        public int SumRange(int left, int right)
        {
            var l = _dp[left];
            var r = _dp[right + 1];
            var d = r - l;
            return d;
        }

        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1()
            {
                // Arrange
                var a = new NumArray2(new[] {-2, 0, 3, -5, 2, -1});

                // Act
                // Assert
                Assert.AreEqual(1, a.SumRange(0, 2));
                Assert.AreEqual(-1, a.SumRange(2, 5));
                Assert.AreEqual(-3, a.SumRange(0, 5));
            }
        }
    }
}