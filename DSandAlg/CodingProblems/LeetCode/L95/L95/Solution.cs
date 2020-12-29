#region

using System;
using System.Collections.Generic;
using NUnit.Framework;

#endregion

// Problem: 95. Unique Binary Search Trees II
// URL: https://leetcode.com/problems/unique-binary-search-trees-ii/

namespace LeetCode
{
    public class Solution
    {
        public IList<TreeNode> GenerateTrees(int n)
        {
            var result = new List<TreeNode>();
            if (n == 0)
            {
                return result;
            }

            List<TreeNode> Solve(int min, int max)
            {
                var trees = new List<TreeNode>();

                if (min > max)
                {
                    trees.Add(null);
                    return trees;
                }
                
                for (int i = min; i <= max; i++)
                {
                    var leftTrees = Solve(min, i - 1);
                    var rightTrees = Solve(i + 1, max);

                    foreach (var leftSubTree in leftTrees)
                    {
                        foreach (var rightSubTree in rightTrees)
                        {
                            var newTree = new TreeNode
                            {
                                val = i
                                , left = leftSubTree
                                , right = rightSubTree
                            };
                            trees.Add(newTree);
                        }
                    }
                }

                return trees;
            }

            return Solve(1, n);
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
                var trees = new Solution().GenerateTrees(3);

                // Assert
                Assert.AreEqual(5, trees.Count);
            }
        }
    }
}