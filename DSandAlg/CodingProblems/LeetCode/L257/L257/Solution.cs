#region

using System.Collections.Generic;
using NUnit.Framework;

#endregion

// Problem: 257. Binary Tree Paths
// URL: https://leetcode.com/problems/binary-tree-paths/

namespace LeetCode
{
    public class Solution
    {
        public IList<string> BinaryTreePaths(TreeNode root)
        {
            // SS: DF traversal, preorder traversal
            // runtime complexity: O(V), because we visit every node
            var result = new List<string>();

            void DFS(TreeNode node, string str)
            {
                if (node.left == null && node.right == null)
                {
                    result.Add(str + node.val);
                }
                else
                {
                    if (node.left != null)
                    {
                        DFS(node.left, str + node.val + "->");
                    }

                    if (node.right != null)
                    {
                        DFS(node.right, str + node.val + "->");
                    }
                }
            }

            DFS(root, "");

            return result;
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