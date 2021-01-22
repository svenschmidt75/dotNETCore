#region

using NUnit.Framework;

#endregion

// Problem: 129. Sum Root to Leaf Numbers
// URL: https://leetcode.com/problems/sum-root-to-leaf-numbers/

namespace LeetCode
{
    public class Solution
    {
        public int SumNumbers(TreeNode root)
        {
            if (root == null)
            {
                return 0;
            }

            var sum = 0;

            void DFS(TreeNode node, int number)
            {
                // SS: we do preorder traversal

                var nodeValue = node.val;

                // SS: leaf node?
                if (node.left == null && node.right == null)
                {
                    number += nodeValue;
                    sum += number;
                    return;
                }

                if (node.left != null)
                {
                    DFS(node.left, (number + nodeValue) * 10);
                }

                if (node.right != null)
                {
                    DFS(node.right, (number + nodeValue) * 10);
                }
            }

            DFS(root, 0);

            return sum;
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
                var root = new TreeNode(1, new TreeNode(2), new TreeNode(3));

                // Act
                var sum = new Solution().SumNumbers(root);

                // Assert
                Assert.AreEqual(25, sum);
            }

            [Test]
            public void Test2()
            {
                // Arrange
                var root = new TreeNode(4, new TreeNode(9, new TreeNode(5), new TreeNode(1)), new TreeNode());

                // Act
                var sum = new Solution().SumNumbers(root);

                // Assert
                Assert.AreEqual(1026, sum);
            }
        }
    }
}