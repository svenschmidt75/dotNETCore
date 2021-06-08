#region

using System;
using System.Collections.Generic;
using NUnit.Framework;

#endregion

// Problem: 378. Kth Smallest Element in a Sorted Matrix
// URL: https://leetcode.com/problems/kth-smallest-element-in-a-sorted-matrix/

namespace L378
{
    public class Solution
    {
        public int KthSmallest(int[][] matrix, int k)
        {
//            return KthSmallest1(matrix, k);
            // return KthSmallest2(matrix, k);
            return KthSmallest3(matrix, k);
        }

        private int KthSmallest3(int[][] matrix, int k)
        {
            // SS: exploit the fact that the rows and cols are sorted...
            // runtime complexity: O(k log k)
            // space complexity: O(k)
            
            var visited = new HashSet<(int row, int col)>();

            var n = matrix.Length;

            var minHeap = BinaryHeap<(int priority, (int row, int col))>.CreateHeap((i1, i2) => i1.priority > i2.priority);

            // SS: push the smallest element
            minHeap.Push((matrix[0][0], (0, 0)));
            visited.Add((0, 0));

            int c = 1;
            while (c < k)
            {
                c++;
                
                (int cellValue, (int row, int col)) = minHeap.Pop();

                // SS: right neighbor
                int nr = row;
                int nc = col + 1;
                if (nc < n)
                {
                    if (visited.Contains((nr, nc)) == false)
                    {
                        int p = matrix[nr][nc];
                        minHeap.Push((p, (nr, nc)));
                        visited.Add((nr, nc));
                    }
                }

                // SS: below neighbor
                nr = row + 1;
                nc = col;
                if (nr < n)
                {
                    if (visited.Contains((nr, nc)) == false)
                    {
                        int p = matrix[nr][nc];
                        minHeap.Push((p, (nr, nc)));
                        visited.Add((nr, nc));
                    }
                }
            }

            int result = minHeap.Pop().priority;
            return result;
        }

        private int KthSmallest2(int[][] matrix, int k)
        {
            // SS: runtime complexity: O(n^2 log n^2)
            // space complexity: O(k)
            
            var maxHeap = BinaryHeap<int>.CreateMaxHeap();
            
            var n = matrix.Length;
            for (var i = 0; i < n; i++)
            {
                for (var j = 0; j < n; j++)
                {
                    int v = matrix[i][j];
                    if (maxHeap.Length < k)
                    {
                        maxHeap.Push(v);
                    }
                    else
                    {
                        if (v < maxHeap.Peek())
                        {
                            maxHeap.Pop();
                            maxHeap.Push(v);
                        }
                    }
                }
            }

            var result = maxHeap.Pop();
            return result;
        }

        private int KthSmallest1(int[][] matrix, int k)
        {
            // SS: Sort
            // runtime complexity: O(n^2 log n^2)
            // space complexity: O(n^2)

            var n = matrix.Length;
            var arr = new int[n * n];
            var cnt = 0;
            for (var i = 0; i < n; i++)
            {
                for (var j = 0; j < n; j++)
                {
                    arr[cnt++] = matrix[i][j];
                }
            }

            Array.Sort(arr);

            return arr[k - 1];
        }

        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1()
            {
                // Arrange
                int[][] matrix = { new[] { 1, 5, 9 }, new[] { 10, 11, 13 }, new[] { 12, 13, 15 } };

                // Act
                var result = new Solution().KthSmallest(matrix, 8);

                // Assert
                Assert.AreEqual(13, result);
            }

            [Test]
            public void Test2()
            {
                // Arrange
                int[][] matrix = { new[] { -5 } };

                // Act
                var result = new Solution().KthSmallest(matrix, 1);

                // Assert
                Assert.AreEqual(-5, result);
            }
        }
    }
}