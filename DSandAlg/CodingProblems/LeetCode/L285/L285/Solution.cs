using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

// Problem: Inorder Successor in a BST
// URL: https://leetcode.com/problems/inorder-successor-in-bst/

namespace LeetCode
{
    public class Solution
    {
        public class TreeNode
        {
            public int Key { get; set; }
            public TreeNode Left { get; set; }
            public TreeNode Right { get; set; }
            public TreeNode Parent { get; set; }
        }

        public TreeNode Solve(TreeNode root, TreeNode node)
        {
            // return Solve1(root, node);
            // return Solve2(root, node);
            // return Solve3(root, node);
            // return Solve4(root, node);
            // return Solve5(root, node);
            return Solve6(root, node);
        }

        private TreeNode Solve6(TreeNode root, TreeNode node)
        {
            // SS: In this solution, we have access to the parent of a node and
            // we only use the node, NOT the root, i.e. the node is already
            // given...
            // runtime complexity: O(log n) - since we have access to the parent
            // space complexity: O(log n)

            TreeNode successor = null;
            TreeNode n;

            // SS: case 1: node has a right subtree
            if (node.Right != null)
            {
                // SS: the inorder successor to node is the left-most
                // node in it's right subtree
                n = node.Right;
                while (n != null)
                {
                    successor = n;
                    n = n.Left;
                }

                return successor;
            }

            // SS: case 2: node does not have a right subtree,
            // so the inorder successor is the first parent
            // of the node that is larger than node.
            n = node.Parent;
            while (n != null)
            {
                if (n.Key > node.Key)
                {
                    successor = n;
                    break;
                }

                n = n.Parent;
            }

            return successor;
        }

        private TreeNode Solve5(TreeNode root, TreeNode node)
        {
            // SS: In this solution, we have access to the parent of a node.
            // runtime complexity: O(log n) - since we have access to the parent
            // space complexity: O(log n)

            TreeNode successor = null;
            TreeNode n;

            // SS: case 1: node has a right subtree
            if (node.Right != null)
            {
                // SS: the inorder successor to node is the left-most
                // node in it's right subtree
                n = node.Right;
                while (n != null)
                {
                    successor = n;
                    n = n.Left;
                }

                return successor;
            }

            // SS: case 2: node does not have a right subtree,
            // so the inorder successor is a parent of node.
            // 1. find node
            // 2. first item off the stack is answer (if stack has items left)

            // SS: iterative inorder traversal
            var stack = new Stack<TreeNode>();
            n = root;

            while (true)
            {
                if (n != null)
                {
                    stack.Push(n);
                    n = n.Left;
                }
                else
                {
                    n = stack.Pop();

                    // SS: we are done exploring n's left subtree
                    if (n == node)
                    {
                        // SS: we found the node
                        break;
                    }

                    // SS: explore n's right subtree
                    n = n.Right;
                }
            }

            // SS: we have found the node
            if (stack.Any())
            {
                // SS: this is the inorder successor
                // look at how iterative inorder traversal works to understand
                // why that is...
                successor = stack.Pop();
            }

            return successor;
        }

        private TreeNode Solve4(TreeNode root, TreeNode node)
        {
            // SS: runtime complexity: DFS - O(n), searching for the node: O(log n)
            // space complexity: O(1) due to iterative inorder traversal

            TreeNode successor = null;
            bool foundNode = false;

            // SS: iterative inorder traversal
            var stack = new Stack<TreeNode>();
            TreeNode n = root;

            while (true)
            {
                if (n != null)
                {
                    stack.Push(n);
                    n = n.Left;
                }
                else
                {
                    if (stack.Any() == false)
                    {
                        // SS: we are done
                        break;
                    }

                    n = stack.Pop();

                    // SS: we are done exploring n's left subtree
                    if (n == node)
                    {
                        // SS: we found the node
                        foundNode = true;
                    }

                    if (foundNode)
                    {
                        if (n.Key > node.Key)
                        {
                            successor = n;
                            break;
                        }
                    }

                    // SS: explore n's right subtree
                    n = n.Right;
                }
            }

            return successor;
        }

        private TreeNode Solve3(TreeNode root, TreeNode node)
        {
            // SS: runtime complexity: DFS - O(n), searching for the node: O(log n)
            // space complexity: O(log n) avg., O(n) worst-case
            TreeNode successor = null;
            bool foundNode = false;

            // SS: DFS inorder traversal
            void DFS(TreeNode n)
            {
                // SS: base case
                if (n == null)
                {
                    return;
                }

                // SS: inorder traversal
                DFS(n.Left);

                if (n == node)
                {
                    // SS: we found the node
                    foundNode = true;
                }

                if (foundNode)
                {
                    if (n.Key > node.Key)
                    {
                        if (successor != null && n.Key < successor.Key)
                        {
                            successor = n;

                            // SS: do not explore the right subtree
                            return;
                        }

                        if (successor == null)
                        {
                            successor = n;

                            // SS: do not explore the right subtree
                            return;
                        }
                    }
                }

                // SS: If we already have an inorder successor, we do not
                // have to traverse the right subtree as keys are always
                // bigger than the found successor's...
                DFS(n.Right);
            }

            DFS(root);
            return successor;
        }

        private TreeNode Solve2(TreeNode root, TreeNode node)
        {
            // SS: runtime complexity: DFS - O(n), searching for the node: O(log n)
            // space complexity: O(log n) avg., O(n) worst-case
            TreeNode successor = null;
            bool foundNode = false;

            // SS: DFS inorder traversal
            void DFS(TreeNode n)
            {
                // SS: base case
                if (n == null)
                {
                    return;
                }

                // SS: inorder traversal
                DFS(n.Left);

                if (n == node)
                {
                    // SS: we found the node
                    foundNode = true;
                }

                if (foundNode)
                {
                    if (n.Key > node.Key)
                    {
                        if (successor != null && n.Key < successor.Key)
                        {
                            successor = n;

                            // SS: do not explore the right subtree
                            return;
                        }

                        if (successor == null)
                        {
                            successor = n;

                            // SS: do not explore the right subtree
                            return;
                        }
                    }
                }

                DFS(n.Right);
            }

            DFS(root);
            return successor;
        }

        private TreeNode Solve1(TreeNode root, TreeNode node)
        {
            // SS: runtime complexity: DFS: O(n), n = #nodes
            // search: O(n)
            // total runtime complexity: O(n)
            // space complexity: O(n)
            var nodes = new List<TreeNode>();

            // SS: DFS inorder traversal
            void DFS(TreeNode n)
            {
                // SS: base case
                if (n == null)
                {
                    return;
                }

                // SS: inorder
                DFS(n.Left);
                nodes.Add(n);
                DFS(n.Right);
            }

            DFS(root);

            // SS: find key
            int idx = nodes.IndexOf(node);
            return idx + 1 == nodes.Count ? null : nodes[idx + 1];
        }

        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1()
            {
                // Arrange
                var n11 = new TreeNode { Key = 11 };
                var n14 = new TreeNode { Key = 14 };
                var n5 = new TreeNode { Key = 5 };
                var n12 = new TreeNode { Key = 12 };
                var n9 = new TreeNode { Key = 9 };
                var n25 = new TreeNode { Key = 25 };
                var n20 = new TreeNode { Key = 20 };

                n20.Left = n9;
                n9.Parent = n20;
                n20.Right = n25;
                n25.Parent = n20;

                n9.Left = n5;
                n5.Parent = n9;
                n9.Right = n12;
                n12.Parent = n9;

                n12.Left = n11;
                n11.Parent = n12;
                n12.Right = n14;
                n14.Parent = n12;

                // Act
                TreeNode result = new Solution().Solve(n20, n9);

                // Assert
                Assert.AreEqual(11, result.Key);
            }

            [Test]
            public void Test2()
            {
                // Arrange
                var n11 = new TreeNode { Key = 11 };
                var n14 = new TreeNode { Key = 14 };
                var n5 = new TreeNode { Key = 5 };
                var n12 = new TreeNode { Key = 12 };
                var n9 = new TreeNode { Key = 9 };
                var n25 = new TreeNode { Key = 25 };
                var n20 = new TreeNode { Key = 20 };

                n20.Left = n9;
                n9.Parent = n20;
                n20.Right = n25;
                n25.Parent = n20;

                n9.Left = n5;
                n5.Parent = n9;
                n9.Right = n12;
                n12.Parent = n9;

                n12.Left = n11;
                n11.Parent = n12;
                n12.Right = n14;
                n14.Parent = n12;

                // Act
                TreeNode result = new Solution().Solve(n20, n14);

                // Assert
                Assert.AreEqual(20, result.Key);
            }

            [Test]
            public void Test3()
            {
                // Arrange
                var n11 = new TreeNode { Key = 11 };
                var n14 = new TreeNode { Key = 14 };
                var n5 = new TreeNode { Key = 5 };
                var n12 = new TreeNode { Key = 12 };
                var n9 = new TreeNode { Key = 9 };
                var n25 = new TreeNode { Key = 25 };
                var n20 = new TreeNode { Key = 20 };

                n20.Left = n9;
                n9.Parent = n20;
                n20.Right = n25;
                n25.Parent = n20;

                n9.Left = n5;
                n5.Parent = n9;
                n9.Right = n12;
                n12.Parent = n9;

                n12.Left = n11;
                n11.Parent = n12;
                n12.Right = n14;
                n14.Parent = n12;

                // Act
                TreeNode result = new Solution().Solve(n20, n25);

                // Assert
                Assert.IsNull(result);
            }

            [Test]
            public void Test4()
            {
                // Arrange
                var n11 = new TreeNode { Key = 11 };
                var n14 = new TreeNode { Key = 14 };
                var n5 = new TreeNode { Key = 5 };
                var n12 = new TreeNode { Key = 12 };
                var n9 = new TreeNode { Key = 9 };
                var n25 = new TreeNode { Key = 25 };
                var n20 = new TreeNode { Key = 20 };

                n20.Left = n9;
                n9.Parent = n20;
                n20.Right = n25;
                n25.Parent = n20;

                n9.Left = n5;
                n5.Parent = n9;
                n9.Right = n12;
                n12.Parent = n9;

                n12.Left = n11;
                n11.Parent = n12;
                n12.Right = n14;
                n14.Parent = n12;

                // Act
                TreeNode result = new Solution().Solve(n20, n11);

                // Assert
                Assert.AreEqual(12, result.Key);
            }

            [Test]
            public void Test5()
            {
                // Arrange
                var n11 = new TreeNode { Key = 11 };
                var n14 = new TreeNode { Key = 14 };
                var n5 = new TreeNode { Key = 5 };
                var n12 = new TreeNode { Key = 12 };
                var n9 = new TreeNode { Key = 9 };
                var n25 = new TreeNode { Key = 25 };
                var n20 = new TreeNode { Key = 20 };

                n20.Left = n9;
                n9.Parent = n20;
                n20.Right = n25;
                n25.Parent = n20;

                n9.Left = n5;
                n5.Parent = n9;
                n9.Right = n12;
                n12.Parent = n9;

                n12.Left = n11;
                n11.Parent = n12;
                n12.Right = n14;
                n14.Parent = n12;

                // Act
                TreeNode result = new Solution().Solve(n20, n5);

                // Assert
                Assert.AreEqual(9, result.Key);
            }

            [Test]
            public void Test6()
            {
                // Arrange
                var n19 = new TreeNode { Key = 19 };
                var n12 = new TreeNode { Key = 12 };
                var n11 = new TreeNode { Key = 11 };
                var n14 = new TreeNode { Key = 14 };
                var n16 = new TreeNode { Key = 16 };
                var n23 = new TreeNode { Key = 23 };

                n19.Left = n12;
                n12.Parent = n19;
                n19.Right = n23;
                n23.Parent = n19;

                n23.Left = n16;
                n16.Parent = n23;

                n12.Left = n11;
                n11.Parent = n12;
                n12.Right = n14;
                n14.Parent = n12;

                // Act
                TreeNode result = new Solution().Solve(n19, n14);

                // Assert
                Assert.AreEqual(19, result.Key);
            }
        }
    }
}