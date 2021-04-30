#region

using System.Linq;
using NUnit.Framework;

#endregion

// Problem: Number of larger numbers to the right
// URL:

namespace LeetCode
{
    public class Solution
    {
        public int[] Larger(int[] nums)
        {
            // SS: runtime complexity: O(n log n)

            // SS: sort
            var sorted = nums.Select((n, idx) => (idx, n)).OrderByDescending(t => t.n).ToArray();

            var result = new int[nums.Length];

            var fenwickTree = new int[nums.Length + 1];

            for (var i = 0; i < sorted.Length; i++)
            {
                var (idx, _) = sorted[i];
                var fIdx = sorted.Length - idx - 1;

                // SS: O(log n)
                var count = QueryFenwick(fenwickTree, fIdx);

                // SS: O(log n)
                AddFenwick(fenwickTree, fIdx);

                result[idx] = count;
            }

            return result;
        }

        private static void AddFenwick(int[] fenwickTree, int idx)
        {
            // SS: increase by 1
            var i = idx + 1;
            while (i < fenwickTree.Length)
            {
                fenwickTree[i]++;
                var lsb = i & -i;

                // SS: move to next cell that is influenced by cell i
                i += lsb;
            }
        }

        private static int QueryFenwick(int[] fenwickTree, int idx)
        {
            var count = 0;

            var i = idx + 1;
            while (i > 0)
            {
                count += fenwickTree[i];
                var lsb = i & -i;
                i -= lsb;
            }

            return count;
        }


        [TestFixture]
        public class Tests
        {
            [TestCase(new[] {5, 3, 7, 9, 4, 2, 12}, new[] {3, 4, 2, 1, 1, 1, 0})]
            public void Test(int[] nums, int[] expected)
            {
                // Arrange

                // Act
                var larger = new Solution().Larger(nums);

                // Assert
                CollectionAssert.AreEqual(expected, larger);
            }
        }
    }
}