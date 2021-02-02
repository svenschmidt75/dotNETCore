#region

using System.Collections.Generic;
using NUnit.Framework;

#endregion

// Problem: 119. Pascal's Triangle II
// URL: https://leetcode.com/problems/pascals-triangle-ii/

namespace LeetCode
{
    public class Solution
    {
        public IList<int> GetRow(int rowIndex)
        {
            // SS: DP, bottom-up
            // SS: runtime complexity: O(rowIndex^2)
            // The recurrence relation is:
            // f(i, j) = f(i - 1, j - 1) + f(i - 1, j)
            // f(i, 0) = f(i, max) = 1
            
            var row1 = new int[rowIndex + 1];
            row1[0] = 1;

            if (rowIndex == 0)
            {
                return row1;
            }

            row1[1] = 1;

            var row2 = new int[rowIndex + 1];
            row2[0] = 1;

            for (var i = 1; i < rowIndex; i++)
            {
                for (var j = 0; j < i; j++)
                {
                    var v = row1[j] + row1[j + 1];
                    row2[j + 1] = v;
                }

                row2[i + 1] = 1;

                var tmp = row1;
                row1 = row2;
                row2 = tmp;
            }

            return row1;
        }

        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1()
            {
                // Arrange

                // Act
                var row = new Solution().GetRow(3);

                // Assert
                CollectionAssert.AreEqual(new[] {1, 3, 3, 1}, row);
            }

            [Test]
            public void Test2()
            {
                // Arrange

                // Act
                var row = new Solution().GetRow(5);

                // Assert
                CollectionAssert.AreEqual(new[] {1, 5, 10, 10, 5, 1}, row);
            }

            [Test]
            public void Test3()
            {
                // Arrange

                // Act
                var row = new Solution().GetRow(2);

                // Assert
                CollectionAssert.AreEqual(new[] {1, 2, 1}, row);
            }

            [Test]
            public void Test4()
            {
                // Arrange

                // Act
                var row = new Solution().GetRow(4);

                // Assert
                CollectionAssert.AreEqual(new[] {1, 4, 6, 4, 1}, row);
            }

            [Test]
            public void Test7()
            {
                // Arrange

                // Act
                var row = new Solution().GetRow(7);

                // Assert
                CollectionAssert.AreEqual(new[] {1, 7, 21, 35, 35, 21, 7, 1}, row);
            }
        }
    }
}