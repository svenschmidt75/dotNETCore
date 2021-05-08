#region

using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

#endregion

// Problem: 637. Average of Levels in Binary Tree
// URL: https://leetcode.com/problems/average-of-levels-in-binary-tree/

namespace LeetCode
{
    public class Solution
    {
        public IList<double> AverageOfLevels(TreeNode root)
        {
            // SS: BFS or level-order travsersal
            var sumValues = new List<double>();
            var nNodes = new List<int>();

            var q = new Queue<(TreeNode node, int level)>();
            q.Enqueue((root, 0));

            while (q.Any())
            {
                (var node, var level) = q.Dequeue();

                // SS: add a new level?
                if (sumValues.Count == level)
                {
                    sumValues.Add(0);
                    nNodes.Add(0);
                }

                sumValues[level] += node.val;
                nNodes[level]++;

                if (node.left != null)
                {
                    q.Enqueue((node.left, level + 1));
                }

                if (node.right != null)
                {
                    q.Enqueue((node.right, level + 1));
                }
            }

            // SS: calc. avg
            for (var i = 0; i < sumValues.Count; i++)
            {
                sumValues[i] /= nNodes[i];
            }

            return sumValues;
        }

        public class TreeNode
        {
            public TreeNode left;
            public TreeNode right;
            public int val;

            public TreeNode(int val = 0, TreeNode left = null, TreeNode right = null)
            {
                this.val = val;
                this.left = left;
                this.right = right;
            }
        }

        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1()
            {
                // Arrange

                // Act

                // Assert
            }
        }
    }
}