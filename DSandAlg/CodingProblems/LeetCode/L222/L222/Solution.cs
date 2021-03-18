#region

using NUnit.Framework;

#endregion

// Problem: 222. Count Complete Tree Nodes
// URL: https://leetcode.com/problems/count-complete-tree-nodes/

namespace LeetCode
{
    public class Solution
    {
        public int CountNodes(TreeNode root)
        {
            // return CountNodes1(root);
            return CountNodes2(root);
        }

        private int CountNodes2(TreeNode root)
        {
            // SS: runtime complexity: O(log^2 N)
            // space complexity: O(log N)

            if (root == null)
            {
                return 0;
            }

            int Solve(TreeNode node)
            {
                if (node == null)
                {
                    return 0;
                }

                // SS: count path length to left-most node
                var n = node.left;
                var leftCount = 0;
                while (n != null)
                {
                    leftCount++;
                    n = n.left;
                }

                // SS: count path length to left-most node
                n = node.right;
                var rightCount = 0;
                while (n != null)
                {
                    rightCount++;
                    n = n.right;
                }

                if (leftCount == rightCount)
                {
                    // SS: count the current node as well
                    return (1 << (leftCount + 1)) - 1;
                }

                return 1 + Solve(node.left) + Solve(node.right);
            }

            return Solve(root);
        }

        private int CountNodes1(TreeNode root)
        {
            if (root == null)
            {
                return 0;
            }

            (int count, int maxFullLevel, bool done) Solve(TreeNode node, int v, int level, int maxFullLevel)
            {
                var count = 0;

                if (maxFullLevel == -1 || level > maxFullLevel)
                {
                    count += v;
                }

                var done = false;

                if (node.right != null)
                {
                    var v2 = 2 * v;
                    int c;
                    (c, maxFullLevel, done) = Solve(node.right, v2, level + 1, maxFullLevel);
                    count += c;
                }

                if (done)
                {
                    return (count, maxFullLevel, done);
                }

                if (node.left != null && (node.right == null || maxFullLevel != -1))
                {
                    var v2 = 2 * v - 1;
                    int c;
                    int mfl;
                    (c, mfl, done) = Solve(node.left, v2, level + 1, maxFullLevel);
                    count += c;

                    if (maxFullLevel == -1)
                    {
                        done = true;
                    }
                }

                if (done)
                {
                    return (count, maxFullLevel, done);
                }

                if (node.left == null && node.left == null)
                {
                    // SS leaf node
                    if (maxFullLevel == -1)
                    {
                        return (count, level, false);
                    }

                    return (count, maxFullLevel, level > maxFullLevel);
                }

                return (count, maxFullLevel, done);
            }

            var (count, _, _) = Solve(root, 1, 0, -1);
            return count;
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
                var root = new TreeNode(1);

                // Act
                var n = new Solution().CountNodes(root);

                // Assert
                Assert.AreEqual(1, n);
            }

            [Test]
            public void Test2()
            {
                // Arrange
                var root = new TreeNode(1, new TreeNode(2));

                // Act
                var n = new Solution().CountNodes(root);

                // Assert
                Assert.AreEqual(2, n);
            }

            [Test]
            public void Test3()
            {
                // Arrange
                var root = new TreeNode(1, new TreeNode(2), new TreeNode(3));

                // Act
                var n = new Solution().CountNodes(root);

                // Assert
                Assert.AreEqual(3, n);
            }

            [Test]
            public void Test4()
            {
                // Arrange
                var root = new TreeNode(1, new TreeNode(2, new TreeNode(4), new TreeNode(5)), new TreeNode(3, new TreeNode(6)));

                // Act
                var n = new Solution().CountNodes(root);

                // Assert
                Assert.AreEqual(6, n);
            }

            [Test]
            public void Test5()
            {
                // Arrange
                var root = new TreeNode
                {
                    val = 1
                    , left = new TreeNode
                    {
                        val = 2
                        , left = new TreeNode
                        {
                            val = 4
                            , left = new TreeNode(7)
                            , right = new TreeNode(8)
                        }
                        , right = new TreeNode
                        {
                            val = 4
                            , left = new TreeNode(9)
                            , right = new TreeNode(10)
                        }
                    }
                    , right = new TreeNode
                    {
                        val = 3
                        , left = new TreeNode
                        {
                            val = 6
                            , left = new TreeNode(11)
                        }
                    }
                };

                // Act
                var n = new Solution().CountNodes(root);

                // Assert
                Assert.AreEqual(11, n);
            }

            [Test]
            public void Test6()
            {
                // Arrange
                var root = new TreeNode
                {
                    val = 1
                    , left = new TreeNode
                    {
                        val = 2
                        , left = new TreeNode
                        {
                            val = 4
                            , left = new TreeNode(7)
                            , right = new TreeNode(8)
                        }
                        , right = new TreeNode
                        {
                            val = 5
                            , left = new TreeNode(9)
                            , right = new TreeNode(10)
                        }
                    }
                    , right = new TreeNode
                    {
                        val = 3
                        , left = new TreeNode
                        {
                            val = 6
                            , left = new TreeNode(11)
                            , right = new TreeNode(12)
                        }
                        , right = new TreeNode
                        {
                            val = 7
                            , left = new TreeNode(11)
                        }
                    }
                };

                // Act
                var n = new Solution().CountNodes(root);

                // Assert
                Assert.AreEqual(14, n);
            }

            [Test]
            public void Test7()
            {
                // Arrange
                var root = new TreeNode
                {
                    val = 1
                    , left = new TreeNode
                    {
                        val = 2
                        , left = new TreeNode
                        {
                            val = 4
                            , left = new TreeNode(9)
                            , right = new TreeNode(10)
                        }
                        , right = new TreeNode
                        {
                            val = 5
                            , left = new TreeNode(9)
                            , right = new TreeNode(10)
                        }
                    }
                    , right = new TreeNode
                    {
                        val = 3
                        , left = new TreeNode
                        {
                            val = 6
                            , left = new TreeNode(11)
                            , right = new TreeNode(12)
                        }
                        , right = new TreeNode(7)
                    }
                };

                // Act
                var n = new Solution().CountNodes(root);

                // Assert
                Assert.AreEqual(13, n);
            }

            [Test]
            public void Test8()
            {
                // Arrange
                var root = new TreeNode
                {
                    val = 1
                    , left = new TreeNode
                    {
                        val = 2
                        , left = new TreeNode(4)
                    }
                    , right = new TreeNode
                    {
                        val = 3
                    }
                };

                // Act
                var n = new Solution().CountNodes(root);

                // Assert
                Assert.AreEqual(4, n);
            }

            [Test]
            public void Test9()
            {
                // Arrange
                var root = new TreeNode
                {
                    val = 1
                    , left = new TreeNode
                    {
                        val = 2
                        , left = new TreeNode
                        {
                            val = 4
                            , left = new TreeNode(8)
                            , right = new TreeNode(9)
                        }
                        , right = new TreeNode
                        {
                            val = 5
                            , left = new TreeNode(10)
                        }
                    }
                    , right = new TreeNode
                    {
                        val = 3
                        , left = new TreeNode
                        {
                            val = 6
                        }
                        , right = new TreeNode
                        {
                            val = 7
                        }
                    }
                };

                // Act
                var n = new Solution().CountNodes(root);

                // Assert
                Assert.AreEqual(10, n);
            }
        }
    }
}