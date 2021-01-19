#region

using System.Collections.Generic;
using NUnit.Framework;

#endregion

// Problem: 118. Pascal's Triangle
// URL: https://leetcode.com/problems/pascals-triangle/

namespace LeetCode
{
    public class Solution
    {
        public IList<IList<int>> Generate(int numRows)
        {
            var result = new List<IList<int>>();

            if (numRows == 0)
            {
                return result;
            }

            result.Add(new List<int> {1});

            for (var i = 1; i < numRows; i++)
            {
                var row = new List<int>();

                // SS: 1st always 1
                row.Add(1);

                var prevRow = result[i - 1];
                for (var j = 0; j <= prevRow.Count - 2; j++)
                {
                    var v = prevRow[j] + prevRow[j + 1];
                    row.Add(v);
                }

                // SS: last always 1
                row.Add(1);

                result.Add(row);
            }

            return result;
        }

        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1()
            {
                // Arrange

                // Act
                var result = new Solution().Generate(5);

                // Assert
                CollectionAssert.AreEqual(new[] {new[] {1}, new[] {1, 1}, new[] {1, 2, 1}, new[] {1, 3, 3, 1}, new[] {1, 4, 6, 4, 1}}, result);
            }
        }
    }
}