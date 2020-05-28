
// Construct a binary tree from given inorder and preorder traversals.
// Since the output of traversal is not unique for a BT, when given
// both, we can reconstruct the tree uniquely.

#region

using System.Collections.Generic;
using NUnit.Framework;

#endregion

namespace BinaryTreeFromTraversal
{
    public class Node
    {
        public int Value { get; set; }
        public Node Left { get; set; }
        public Node Right { get; set; }
    }

    public static class Class1
    {
        public static Node FromInOrderTraversal(int[] input)
        {
            if (input.Length == 0)
            {
                return null;
            }

            var root = ConstructFromInOrder(input, 0);

            return root;
        }

        private static Node ConstructFromInOrder(int[] input, int index)
        {
            if (index <= input.Length - 2)
            {
                var left = new Node {Value = input[index]};
                var parent = new Node {Value = input[index + 1], Left = left};
                parent.Right = ConstructFromInOrder(input, index + 2);
                return parent;
            }

            var r = new Node {Value = input[index]};
            return r;
        }

        public static Node FromInOrderTraversalBalanced(int[] input)
        {
            if (input.Length == 0)
            {
                return null;
            }

            var root = ConstructFromInOrderBalanced(input, 0, input.Length - 1);

            return root;
        }

        private static Node ConstructFromInOrderBalanced(int[] input, int min, int max)
        {
            if (min > max)
            {
                return null;
            }

            var mid = (min + max) / 2;
            var parent = new Node {Value = input[mid]};
            parent.Left = ConstructFromInOrderBalanced(input, min, mid - 1);
            parent.Right = ConstructFromInOrderBalanced(input, mid + 1, max);
            return parent;
        }

        public static List<int> InOrderTraversal(Node node)
        {
            var nodes = new List<int>();
            InOrderTraversal(node, nodes);
            return nodes;
        }

        public static void InOrderTraversal(Node node, List<int> nodes)
        {
            if (node == null)
            {
                return;
            }

            if (node.Left != null)
            {
                InOrderTraversal(node.Left, nodes);
            }

            nodes.Add(node.Value);

            if (node.Right != null)
            {
                InOrderTraversal(node.Right, nodes);
            }
        }

        public static Node FromPreOrderTraversal(int[] input)
        {
            if (input.Length == 0)
            {
                return null;
            }

            var root = ConstructFromPreOrder(input, 0);

            return root;
        }

        private static Node ConstructFromPreOrder(int[] input, int index)
        {
            var parent = new Node {Value = input[index]};

            if (index == input.Length - 1)
            {
                return parent;
            }

            var left = new Node {Value = input[index + 1]};
            var right = ConstructFromPreOrder(input, index + 2);
            parent.Left = left;
            parent.Right = right;
            return parent;
        }

        public static Node FromPreOrderTraversalBalanced(int[] input)
        {
            if (input.Length == 0)
            {
                return null;
            }

            var root = ConstructFromPreOrderBalanced(input, 0, input.Length - 1);

            return root;
        }

        private static Node ConstructFromPreOrderBalanced(int[] input, int min, int max)
        {
            if (min > max)
            {
                return null;
            }

            var parent = new Node {Value = input[min]};
            var mid = (min + 1 + max) / 2;
            parent.Left = ConstructFromPreOrderBalanced(input, min + 1, mid);
            parent.Right = ConstructFromPreOrderBalanced(input, mid + 1, max);
            return parent;
        }

        public static List<int> PreOrderTraversal(Node node)
        {
            var nodes = new List<int>();
            PreOrderTraversal(node, nodes);
            return nodes;
        }

        public static void PreOrderTraversal(Node node, List<int> nodes)
        {
            if (node == null)
            {
                return;
            }

            nodes.Add(node.Value);

            if (node.Left != null)
            {
                PreOrderTraversal(node.Left, nodes);
            }

            if (node.Right != null)
            {
                PreOrderTraversal(node.Right, nodes);
            }
        }

        public static List<int> PostOrderTraversal(Node node)
        {
            var nodes = new List<int>();
            PostOrderTraversal(node, nodes);
            return nodes;
        }

        public static void PostOrderTraversal(Node node, List<int> nodes)
        {
            if (node == null)
            {
                return;
            }

            if (node.Left != null)
            {
                PostOrderTraversal(node.Left, nodes);
            }

            if (node.Right != null)
            {
                PostOrderTraversal(node.Right, nodes);
            }

            nodes.Add(node.Value);
        }

        public static Node FromPostOrderTraversalBalanced(int[] input)
        {
            if (input.Length == 0)
            {
                return null;
            }

            var root = ConstructFromPostOrderBalanced(input, 0, input.Length - 1);

            return root;
        }

        private static Node ConstructFromPostOrderBalanced(int[] input, int min, int max)
        {
            if (min > max)
            {
                return null;
            }

            var parent = new Node {Value = input[max]};
            if (min <= max - 1)
            {
                var mid = (min + max - 1) / 2;
                parent.Left = ConstructFromPostOrderBalanced(input, min, mid);
                parent.Right = ConstructFromPostOrderBalanced(input, mid + 1, max - 1);
            }

            return parent;
        }

        public static Node ConstructFromPostOrderAndInOrder(int[] postorder, int[] inorder)
        {
            // SS: allows for O(1) access to an item's index
            var itemToIndex = new Dictionary<int, int>();
            for (var i = 0; i < inorder.Length; i++)
            {
                itemToIndex.Add(inorder[i], i);
            }

            var root = ConstructFromPostOrderAndInOrder(postorder, inorder, itemToIndex, 0, inorder.Length - 1, -1
                , inorder.Length - 1);
            return root;
        }

        private static Node ConstructFromPostOrderAndInOrder(int[] postorder, int[] inorder
            , IDictionary<int, int> itemToIndex, int postMin, int postMax, int inMin, int inMax)
        {
            if (postMin > postMax)
            {
                return null;
            }

            // SS: parent node is always the last node in post-order
            var rootValue = postorder[postMax];
            var parent = new Node {Value = rootValue};

            // SS: get left subtree
            var inIndex = itemToIndex[rootValue];
//            if (min < mid && mid <= max)
            {
                var offset = inIndex - inMin - 1;
                if (offset > 0)
                {
                    parent.Left = ConstructFromPostOrderAndInOrder(postorder, inorder, itemToIndex, postMin
                        , postMin + offset - 1, inMin, inIndex);
                }

                if (offset >= 0)
                {
                    parent.Right = ConstructFromPostOrderAndInOrder(postorder, inorder, itemToIndex, postMin + offset
                        , postMax - 1, inIndex, inMax);
                }
            }
            return parent;
        }
    }

    [TestFixture]
    public class Test
    {
        [Test]
        public void FromInOrder()
        {
            // Arrange
            var input = new[] {1, 2, 3, 5, 8, 9, 10};

            // Act
            var root = Class1.FromInOrderTraversal(input);

            // Assert
            CollectionAssert.AreEqual(input, Class1.InOrderTraversal(root));
        }

        [Test]
        public void FromInOrder2()
        {
            // Arrange
            var input = new[] {1, 2, 3, 5, 8, 9, 10};

            // Act
            var root = Class1.FromInOrderTraversalBalanced(input);

            // Assert
            CollectionAssert.AreEqual(input, Class1.InOrderTraversal(root));
        }

        [Test]
        public void FromPostOrder()
        {
            // Arrange
            var input = new[] {1, 3, 2, 8, 10, 9, 5};

            // Act
            var root = Class1.FromPostOrderTraversalBalanced(input);

            // Assert
            CollectionAssert.AreEqual(input, Class1.PostOrderTraversal(root));
        }

        [Test]
        public void FromPostOrderAndInOrder()
        {
            // Arrange
            var postorder = new[] {4, 5, 2, 8, 6, 7, 3, 1};
            var inorder = new[] {4, 2, 5, 1, 6, 8, 3, 7};

            // Act
            var root = Class1.ConstructFromPostOrderAndInOrder(postorder, inorder);

            // Assert
            CollectionAssert.AreEqual(postorder, Class1.PostOrderTraversal(root));
            CollectionAssert.AreEqual(inorder, Class1.InOrderTraversal(root));
        }

        [Test]
        public void FromPreOrder()
        {
            // Arrange
            var input = new[] {1, 2, 3, 5, 8, 9, 10};

            // Act
            var root = Class1.FromPreOrderTraversal(input);

            // Assert
            CollectionAssert.AreEqual(input, Class1.PreOrderTraversal(root));
        }

        [Test]
        public void FromPreOrder2()
        {
            // Arrange
            var input = new[] {1, 2, 3, 5, 8, 9, 10};

            // Act
            var root = Class1.FromPreOrderTraversalBalanced(input);

            // Assert
            CollectionAssert.AreEqual(input, Class1.PreOrderTraversal(root));
        }

        [Test]
        public void FromPreOrder3()
        {
            // Arrange

            // Act
            var node4 = new Node {Value = 4};
            var node5 = new Node {Value = 5};
            var node2 = new Node {Value = 2, Left = node4, Right = node5};

            var node6 = new Node {Value = 6};
            var node7 = new Node {Value = 7};
            var node3 = new Node {Value = 3, Left = node6, Right = node7};

            var node8 = new Node {Value = 8};
            node6.Right = node8;

            var node1 = new Node {Value = 1, Left = node2, Right = node3};

            // Assert
            var post = Class1.PostOrderTraversal(node1);
            var inorder = Class1.InOrderTraversal(node1);
        }
    }
}