#region

using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

#endregion

// Problem: 100. Same Tree
// URL: https://leetcode.com/problems/same-tree/

namespace LeetCode
{
    public class Solution
    {
        public bool IsSameTree(TreeNode p, TreeNode q)
        {
            // SS: runtime complexity: O(min(p, q))
            // space complexity: O(min(p, q))

            var p1 = IsSameTreeRecursive(p, q);
            var p2 = IsSameTreeIterative(p, q);
            return p1 == p2 && p1;
        }

        public bool IsSameTreeIterative(TreeNode p, TreeNode q)
        {
            // SS: runtime complexity: O(min(p, q))
            // space complexity: O(min(p, q))
            
            var stack = new Stack<(TreeNode left, TreeNode right)>();
            stack.Push((p, q));

            while (stack.Any())
            {
                (var left, var right) = stack.Pop();

                if (left == null && right != null)
                {
                    return false;
                }

                if (left != null && right == null)
                {
                    return false;
                }

                if (left != null)
                {
                    if (left.val != right.val)
                    {
                        return false;
                    }

                    stack.Push((left.left, right.left));
                    stack.Push((left.right, right.right));
                }
            }

            return true;
        }


        public bool IsSameTreeRecursive(TreeNode p, TreeNode q)
        {
            if (p == null)
            {
                return q == null;
            }

            if (q == null)
            {
                return false;
            }

            // SS: inorder traversal
            return q.val == p.val && IsSameTreeRecursive(q.left, p.left) && IsSameTreeRecursive(q.right, p.right);
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
                var left = new TreeNode {val = 1, left = new TreeNode {val = 2}, right = new TreeNode {val = 3}};
                var right = new TreeNode {val = 1, left = new TreeNode {val = 2}, right = new TreeNode {val = 3}};

                // Act
                var same = new Solution().IsSameTree(left, right);

                // Assert
                Assert.True(same);
            }

            [Test]
            public void Test2()
            {
                // Arrange
                var left = new TreeNode {val = 1, left = new TreeNode {val = 2}};
                var right = new TreeNode {val = 1, right = new TreeNode {val = 2}};

                // Act
                var same = new Solution().IsSameTree(left, right);

                // Assert
                Assert.False(same);
            }

            [Test]
            public void Test3()
            {
                // Arrange
                var left = new TreeNode {val = 1, left = new TreeNode {val = 2}, right = new TreeNode {val = 1}};
                var right = new TreeNode {val = 1, left = new TreeNode {val = 1}, right = new TreeNode {val = 2}};

                // Act
                var same = new Solution().IsSameTree(left, right);

                // Assert
                Assert.False(same);
            }

            [Test]
            public void Test4()
            {
                // Arrange
                var left = new TreeNode {val = 1, left = new TreeNode {val = 2}};
                var right = new TreeNode {val = 1, right = new TreeNode {val = 2}};

                // Act
                var same = new Solution().IsSameTree(left, right);

                // Assert
                Assert.False(same);
            }
        }
    }
}