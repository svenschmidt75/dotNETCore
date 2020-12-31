#region

using System.Collections.Generic;
using NUnit.Framework;

#endregion

// Problem: 113. Path Sum II
// URL: https://leetcode.com/problems/path-sum-ii/

namespace LeetCode
{
    public class Solution
    {
        public IList<IList<int>> PathSum(TreeNode root, int sum)
        {
            // SS: solving using backtracking
            var results = new List<IList<int>>();

            void DFS(TreeNode node, int localSum, List<int> r)
            {
                if (node == null)
                {
                    return;
                }

                r.Add(node.val);

                // SS: is this a leaf node?
                if (node.left == null && node.right == null)
                {
                    if (localSum + node.val == sum)
                    {
                        results.Add(new List<int>(r));
                    }
                }
                else
                {
                    DFS(node.left, localSum + node.val, r);
                    DFS(node.right, localSum + node.val, r);
                }

                // SS: backtrack
                r.RemoveAt(r.Count - 1);
            }

            DFS(root, 0, new List<int>());
            return results;
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
                TreeNode root = new()
                {
                    val = 5
                    , left = new()
                    {
                        val = 4
                        , left = new()
                        {
                            val = 11
                            , left = new()
                            {
                                val = 7
                            }
                            , right = new()
                            {
                                val = 2
                            }
                        }
                    }
                    , right = new()
                    {
                        val = 8
                        , left = new()
                        {
                            val = 13
                        }
                        , right = new()
                        {
                            val = 4
                            , left = new()
                            {
                                val = 5
                            }
                            , right = new()
                            {
                                val = 1
                            }
                        }
                    }
                };

                // Act
                var results = new Solution().PathSum(root, 22);

                // Assert
                CollectionAssert.AreEquivalent(new[] {new[] {5, 4, 11, 2}, new[] {5, 8, 4, 5}}, results);
            }

            [Test]
            public void Test2()
            {
                // Arrange
                TreeNode root = new()
                {
                    val = -2
                    , right = new()
                    {
                        val = -3
                    }
                };

                // Act
                var results = new Solution().PathSum(root, -5);

                // Assert
                CollectionAssert.AreEquivalent(new[] {new[] {-2, -3}}, results);
            }
        }
    }
}