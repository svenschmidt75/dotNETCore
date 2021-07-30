using System.Collections.Generic;
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
        }

        public TreeNode Solve(TreeNode root, TreeNode node)
        {
//            return Solve1(root, node);
            return Solve2(root, node);
        }

        private TreeNode Solve2(TreeNode root, TreeNode node)
        {
            // SS: runtime complexity: DFS - O(n)
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
                        }
                        else if (successor == null)
                        {
                            successor = n;
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
            return idx == -1 ? null : nodes[idx + 1];
        }

        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1()
            {
                // Arrange
                var n6 = new TreeNode { Key = 11 };
                var n7 = new TreeNode { Key = 14 };

                var n4 = new TreeNode { Key = 5 };
                var n5 = new TreeNode { Key = 12, Left = n6, Right = n7};

                var n2 = new TreeNode { Key = 9, Left = n4, Right = n5};
                
                var n3 = new TreeNode { Key = 25 };
                var n1 = new TreeNode { Key = 20, Left = n2, Right = n3};
                
                // Act
                TreeNode result = new Solution().Solve(n1, n2);

                // Assert
                Assert.AreEqual(11, result.Key);
            }
            
            [Test]
            public void Test2()
            {
                // Arrange
                var n6 = new TreeNode { Key = 11 };
                var n7 = new TreeNode { Key = 14 };

                var n4 = new TreeNode { Key = 5 };
                var n5 = new TreeNode { Key = 12, Left = n6, Right = n7};

                var n2 = new TreeNode { Key = 9, Left = n4, Right = n5};
                
                var n3 = new TreeNode { Key = 25 };
                var n1 = new TreeNode { Key = 20, Left = n2, Right = n3};
                
                // Act
                TreeNode result = new Solution().Solve(n1, n7);

                // Assert
                Assert.AreEqual(20, result.Key);
            }
            
            [Test]
            public void Test3()
            {
                // Arrange
                var n6 = new TreeNode { Key = 11 };
                var n7 = new TreeNode { Key = 14 };

                var n4 = new TreeNode { Key = 5 };
                var n5 = new TreeNode { Key = 12, Left = n6, Right = n7};

                var n2 = new TreeNode { Key = 9, Left = n4, Right = n5};
                
                var n3 = new TreeNode { Key = 25 };
                var n1 = new TreeNode { Key = 20, Left = n2, Right = n3};
                
                // Act
                TreeNode result = new Solution().Solve(n3, n1);

                // Assert
                Assert.IsNull(result);
            }

        }
    }
}