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

            List<TreeNode> Solve(HashSet<int> candidates)
            {
                var trees = new List<TreeNode>();

                foreach (var value in candidates)
                {
                    // SS: partition values < value for left subtree
                    var leftCandidates = new HashSet<int>();
                    foreach (var v2 in candidates)
                    {
                        if (v2 < value)
                        {
                            leftCandidates.Add(v2);
                        }
                    }

                    var leftTrees = Solve(leftCandidates);

                    // SS: partition values > value for right subtree
                    var rightCandidates = new HashSet<int>();
                    foreach (var v2 in candidates)
                    {
                        if (v2 > value)
                        {
                            rightCandidates.Add(v2);
                        }
                    }

                    var rightTrees = Solve(rightCandidates);

                    // SS: create new trees
                    var nLeft = leftTrees.Count;
                    var nRight = rightTrees.Count;

                    var nTrees = Math.Max(1, nLeft) * Math.Max(1, nRight);
                    var lIdx = 0;
                    var rIdx = 0;

                    var treeCnt = 0;
                    while (treeCnt < nTrees)
                    {
                        var leftSubTree = lIdx < leftTrees.Count ? leftTrees[lIdx] : null;
                        var rightSubTree = rIdx < rightTrees.Count ? rightTrees[rIdx] : null;
                        var newTree = new TreeNode
                        {
                            val = value
                            , left = leftSubTree
                            , right = rightSubTree
                        };
                        trees.Add(newTree);

                        treeCnt++;
                        lIdx++;
                        if (lIdx >= leftTrees.Count)
                        {
                            lIdx = 0;
                            rIdx++;
                        }
                    }
                }

                return trees;
            }

            var candidates = new HashSet<int>();
            for (var i = 1; i <= n; i++)
            {
                candidates.Add(i);
            }

            return Solve(candidates);
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