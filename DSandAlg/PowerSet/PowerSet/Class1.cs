#region

using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

#endregion

namespace PowerSet
{
    public class Solution
    {
        public List<List<int>> Generate(List<int> set)
        {
            var subsets = new List<List<int>>();
            var currentSubset = new List<int>();
            subsets.Add(currentSubset);
            Generate(0, set.Count, set, subsets, currentSubset);
            return subsets;
        }

        private void Generate(int start, int end, List<int> set, List<List<int>> subsets, List<int> currentSubset)
        {
            for (var i = start; i < end; i++)
            {
                var item = set[i];
                var s = new List<int>(currentSubset) {item};
                subsets.Add(s);
                Generate(i + 1, end, set, subsets, s);
            }
        }

        public List<int[]> Generate(int n)
        {
            var subsets = new List<int[]>();
            var currentSubset = new int[n];
            Generate(0, n, subsets, currentSubset);
            return subsets;
        }

        private void Generate(int start, int n, List<int[]> subsets, int[] currentSubset)
        {
            // SS: Here, the subsets are always of length n...
            if (start == n)
            {
                subsets.Add(currentSubset.ToArray());
            }
            else
            {
                // SS: add item
                currentSubset[start] = 1;
                Generate(start + 1, n, subsets, currentSubset);

                // SS: do not add item
                currentSubset[start] = 0;
                Generate(start + 1, n, subsets, currentSubset);
            }
        }

        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1()
            {
                // Arrange
                var set = new List<int> {1, 2, 3, 4, 5};

                // Act
                var subsets = new Solution().Generate(set);

                // Assert
                Assert.AreEqual(Math.Pow(2, set.Count), subsets.Count);
            }

            [Test]
            public void Test2()
            {
                // Arrange

                // Act
                var subsets = new Solution().Generate(5);

                // Assert
                Assert.AreEqual(Math.Pow(2, 5), subsets.Count);
            }
        }
    }
}