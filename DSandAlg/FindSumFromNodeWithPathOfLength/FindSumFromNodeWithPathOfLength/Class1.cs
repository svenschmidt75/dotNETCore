#region

using System.Collections.Generic;
using NUnit.Framework;

#endregion

namespace FindSumFromNodeWithPathOfLength
{
    public class Node
    {
        public int Value { get; set; }
        public Node Left { get; set; }
        public Node Right { get; set; }
    }

    public static class FindSum
    {
        /// <summary>
        /// Runtime is at most two traversals, i.e. O(n) 
        /// </summary>
        public static (bool found, int currentK) Find(Node node, int n, int k, int currentK
            , bool found
            , List<List<int>> paths, List<int> currentPath)
        {
            if (node == null)
            {
                return (found, currentK);
            }

            if (node.Value == n && found == false)
            {
                // SS: need to check both subtrees, and then go up
                Find(node.Left, n, k, 1, true, paths, new List<int> {n});
                Find(node.Right, n, k, 1, true, paths, new List<int> {n});

                // SS: go back up the call stack
                currentPath.Add(n);
                return (true, 1);
            }

            if (found)
            {
                if (currentK == k)
                {
                    // SS: reached end of path
                    var newCurrentPath = new List<int>(currentPath);
                    currentPath.Add(node.Value);
                    paths.Add(currentPath);

                    // SS: go back up the call stack
                    return (true, currentK - 1);
                }

                // SS: end of path not yet reached
                var newCurrentPath2 = new List<int>(currentPath);
                newCurrentPath2.Add(node.Value);

                Find(node.Left, n, k, currentK + 1, true, paths, new List<int>(newCurrentPath2));
                Find(node.Right, n, k, currentK + 1, true, paths, new List<int>(newCurrentPath2));

                return (true, currentK - 1);
            }
            // SS: we have not yet found the node with value n

            // SS: check left subtree
            var (f, ck) = Find(node.Left, n, k, currentK, false, paths, currentPath);
            if (f)
            {
                // SS: we found the node in the left subtree
                if (ck == k)
                {
                    // SS: reached end of path
                    currentPath.Add(node.Value);
                    paths.Add(currentPath);

                    // SS: go back up the call stack
                    return (false, ck);
                }

                // SS: end of path not yet reached
                currentPath.Add(node.Value);

                Find(node.Right, n, k, ck + 1, true, paths, new List<int>(currentPath));

                return (true, ck + 1);
            }

            // SS: check right subtree
            (f, ck) = Find(node.Right, n, k, currentK, false, paths, currentPath);
            if (f)
            {
                // SS: we found the node in the right subtree
                if (ck == k)
                {
                    // SS: reached end of path
                    currentPath.Add(node.Value);
                    paths.Add(currentPath);

                    // SS: go back up the call stack
                    return (false, ck);
                }

                // SS: end of path not yet reached
                currentPath.Add(node.Value);

                Find(node.Left, n, k, ck + 1, true, paths, new List<int>(currentPath));

                return (true, ck + 1);
            }

            return (false, ck + 1);
        }
    }

    [TestFixture]
    public class TestFindSum
    {
        private Node CreateBinaryTree()
        {
            var node10 = new Node {Value = 10};
            var node11 = new Node {Value = 11};
            var node8 = new Node {Value = 8, Left = node10, Right = node11};

            var node12 = new Node {Value = 12};
            var node13 = new Node {Value = 13};
            var node9 = new Node {Value = 9, Left = node12, Right = node13};

            var node6 = new Node {Value = 6, Left = node8, Right = node9};

            var node7 = new Node {Value = 7};
            var node3 = new Node {Value = 3, Left = node6, Right = node7};

            var node4 = new Node {Value = 4};
            var node5 = new Node {Value = 5};
            var node2 = new Node {Value = 2, Left = node4, Right = node5};

            var node1 = new Node {Value = 1, Left = node2, Right = node3};

            return node1;
        }

        [Test]
        public void TestNode3Length2()
        {
            // Arrange
            var root = CreateBinaryTree();

            // Act
            var paths = new List<List<int>>();
            FindSum.Find(root, 3, 2, 0, false, paths, new List<int>());

            // Assert
            Assert.AreEqual(3, paths.Count);
        }

        [Test]
        public void TestNode6Length2()
        {
            // Arrange
            var root = CreateBinaryTree();

            // Act
            var paths = new List<List<int>>();
            FindSum.Find(root, 6, 2, 0, false, paths, new List<int>());

            // Assert
            Assert.AreEqual(6, paths.Count);
        }

        [Test]
        public void TestNode9Length3()
        {
            // Arrange
            var root = CreateBinaryTree();

            // Act
            var paths = new List<List<int>>();
            FindSum.Find(root, 9, 3, 0, false, paths, new List<int>());

            // Assert
            Assert.AreEqual(4, paths.Count);
        }
    }
}