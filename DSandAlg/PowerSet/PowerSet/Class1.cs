#region

using System;
using System.Collections.Generic;
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
        }
    }
}