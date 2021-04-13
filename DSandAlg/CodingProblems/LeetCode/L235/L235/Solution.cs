#region

using NUnit.Framework;

#endregion

// Problem: 235. Lowest Common Ancestor of a Binary Search Tree
// URL: https://leetcode.com/problems/lowest-common-ancestor-of-a-binary-search-tree/

namespace LeetCode
{
    public class Solution
    {
        public TreeNode LowestCommonAncestor(TreeNode root, TreeNode p, TreeNode q)
        {
            if (root == p || root == q)
            {
                return root;
            }

            var left = p;
            var right = q;
            if (left.val > right.val)
            {
                left = q;
                right = p;
            }

            if (left.val < root.val && right.val > root.val)
            {
                return root;
            }

            if (right.val < root.val)
            {
                return LowestCommonAncestor(root.left, p, q);
            }

            return LowestCommonAncestor(root.right, p, q);
        }

        public class TreeNode
        {
            public TreeNode left;
            public TreeNode right;
            public int val;

            public TreeNode(int x)
            {
                val = x;
            }
        }

        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1()
            {
                // Arrange
                var root = new TreeNode(6)
                {
                    left = new TreeNode(2)
                    {
                        left = new TreeNode(0)
                        , right = new TreeNode(4)
                        {
                            left = new TreeNode(3)
                            , right = new TreeNode(5)
                        }
                    }
                    , right = new TreeNode(8)
                    {
                        left = new TreeNode(7)
                        , right = new TreeNode(9)
                    }
                };

                var p = root.left;
                var q = root.right;

                // Act
                var lca = new Solution().LowestCommonAncestor(root, p, q);

                // Assert
                Assert.AreEqual(6, lca.val);
            }

            [Test]
            public void Test2()
            {
                // Arrange
                var root = new TreeNode(6)
                {
                    left = new TreeNode(2)
                    {
                        left = new TreeNode(0)
                        , right = new TreeNode(4)
                        {
                            left = new TreeNode(3)
                            , right = new TreeNode(5)
                        }
                    }
                    , right = new TreeNode(8)
                    {
                        left = new TreeNode(7)
                        , right = new TreeNode(9)
                    }
                };

                var p = root.left.right.left;
                var q = root.left.right.right;

                // Act
                var lca = new Solution().LowestCommonAncestor(root, p, q);

                // Assert
                Assert.AreEqual(4, lca.val);
            }

            [Test]
            public void Test3()
            {
                // Arrange
                var root = new TreeNode(6)
                {
                    left = new TreeNode(2)
                    {
                        left = new TreeNode(0)
                        , right = new TreeNode(4)
                        {
                            left = new TreeNode(3)
                            , right = new TreeNode(5)
                        }
                    }
                    , right = new TreeNode(8)
                    {
                        left = new TreeNode(7)
                        , right = new TreeNode(9)
                    }
                };

                var p = root.left.right.right;
                var q = root.left.right.left;

                // Act
                var lca = new Solution().LowestCommonAncestor(root, p, q);

                // Assert
                Assert.AreEqual(4, lca.val);
            }

            [Test]
            public void Test4()
            {
                // Arrange
                var root = new TreeNode(6)
                {
                    left = new TreeNode(2)
                    {
                        left = new TreeNode(0)
                        , right = new TreeNode(4)
                        {
                            left = new TreeNode(3)
                            , right = new TreeNode(5)
                        }
                    }
                    , right = new TreeNode(8)
                    {
                        left = new TreeNode(7)
                        , right = new TreeNode(9)
                    }
                };

                var p = root.left;
                var q = root.left.right.left;

                // Act
                var lca = new Solution().LowestCommonAncestor(root, p, q);

                // Assert
                Assert.AreEqual(2, lca.val);
            }

            [Test]
            public void Test5()
            {
                // Arrange
                var root = new TreeNode(6)
                {
                    left = new TreeNode(2)
                    {
                        left = new TreeNode(0)
                        , right = new TreeNode(4)
                        {
                            left = new TreeNode(3)
                            , right = new TreeNode(5)
                        }
                    }
                    , right = new TreeNode(8)
                    {
                        left = new TreeNode(7)
                        , right = new TreeNode(9)
                    }
                };

                var p = root.left.right.left;
                var q = root.left;

                // Act
                var lca = new Solution().LowestCommonAncestor(root, p, q);

                // Assert
                Assert.AreEqual(2, lca.val);
            }

            [Test]
            public void Test6()
            {
                // Arrange
                var root = new TreeNode(6)
                {
                    left = new TreeNode(2)
                    {
                        left = new TreeNode(0)
                        , right = new TreeNode(4)
                        {
                            left = new TreeNode(3)
                            , right = new TreeNode(5)
                        }
                    }
                    , right = new TreeNode(8)
                    {
                        left = new TreeNode(7)
                        , right = new TreeNode(9)
                    }
                };

                var p = root.left.right.left;
                var q = p;

                // Act
                var lca = new Solution().LowestCommonAncestor(root, p, q);

                // Assert
                Assert.AreEqual(3, lca.val);
            }

            [Test]
            public void Test7()
            {
                // Arrange
                var root = new TreeNode(2) {left = new TreeNode(1)};

                var p = root;
                var q = root.left;

                // Act
                var lca = new Solution().LowestCommonAncestor(root, p, q);

                // Assert
                Assert.AreEqual(2, lca.val);
            }
        }
    }
}