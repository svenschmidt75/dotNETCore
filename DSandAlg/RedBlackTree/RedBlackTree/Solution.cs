#region

using System;
using NUnit.Framework;

#endregion

namespace LeetCode
{
    public class RedBlackTree
    {
        private Node _root;

        enum Color
        {
            Black
            , Red
        }
        
        public void Insert(int v)
        {
            // SS: root is special
            if (_root == null)
            {
                _root = new Node {Value = v, Color = Color.Black};
            }
            else
            {
                _root = InsertInternal(_root, v);
            }
        }

        private Node InsertInternal(Node node, int v)
        {
            if (node == null)
            {
                return new Node {Value = v, Color = Color.Red};
            }

            if (node.Value == v)
            {
                // SS: this is a set, not a multiset, so we do not
                // allow duplicate values
                return node;
            }

            if (v < node.Value)
            {
                var leftNode = InsertInternal(node.Left, v);
                
                // SS: if a rotation happened at a lower level, node.Left becomes
                // the parent of this node
                if (node.Parent == leftNode)
                {
                    // SS: A rotation happened at a lower level. Since RB tree rotations
                    // do not cause RR-violations, we are done
                    return leftNode;
                }

                // SS: a new leaf node was created, check for RR-violations
                node.Left = leftNode;
                node.Left.Parent = node;

                var n = FixViolations(node.Left);
                if (node.Parent == n)
                {
                    return n;
                }
            }
            else
            {
                var nodeRight =  InsertInternal(node.Right, v);
                
                // SS: if a rotation happened at a lower level, node.Right becomes
                // the parent of this node
                if (node.Parent == nodeRight)
                {
                    // SS: A rotation happened at a lower level. Since RB tree rotations
                    // do not cause RR-violations, we are done
                    return nodeRight;
                }

                // SS: a new leaf node was created, check for RR-violations
                node.Right = nodeRight;
                node.Right.Parent = node;

                var n = FixViolations(node.Right);
                if (node.Parent == n)
                {
                    return n;
                }
            }

            return node;
        }

        private static bool NodeHasTwoRedChildren(Node node)
        {
            return node.Left != null && node.Right != null && node.Left.Color == Color.Red && node.Right.Color == Color.Red;
        }

        private static Node RotateLeft(Node node, bool flipColors)
        {
            // SS: rotate left around parent 
            var parent = node.Parent;
            var grandParent = parent.Parent;

            parent.Right = node.Left;
            node.Left = parent;

            // SS: parent is root?
            if (grandParent != null)
            {
                if (grandParent.Left == parent)
                {
                    grandParent.Left = node;
                }
                else
                {
                    grandParent.Right = node;
                }
            }

            node.Parent = grandParent;
            parent.Parent = node;

            if (parent.Right != null)
            {
                parent.Right.Parent = parent;
            }

            if (flipColors)
            {
                node.Color = FlipColor(node.Color);
                parent.Color = FlipColor(parent.Color);
            }

            return node;
        }

        private static Node RotateRight(Node node, bool flipColors)
        {
            // SS: rotate left around parent 
            var parent = node.Parent;
            var grandParent = parent.Parent;

            parent.Left = node.Right;
            node.Right = parent;

            // SS: parent is root?
            if (grandParent != null)
            {
                if (grandParent.Left == parent)
                {
                    grandParent.Left = node;
                }
                else
                {
                    grandParent.Right = node;
                }
            }

            node.Parent = grandParent;
            parent.Parent = node;

            if (parent.Left != null)
            {
                parent.Left.Parent = parent;
            }

            if (flipColors)
            {
                node.Color = FlipColor(node.Color);
                parent.Color = FlipColor(parent.Color);
            }

            return node;
        }

        private static Color FlipColor(Color color)
        {
            return color == Color.Red ? Color.Black : Color.Red;
        }

        private static bool IsLLSituation(Node node)
        {
            var parent = node.Parent;
            var grandParent = parent.Parent;
            return grandParent.Left != null && grandParent.Left == parent && parent.Left != null && parent.Left == node;
        }

        private static bool IsLRSituation(Node node)
        {
            var parent = node.Parent;
            var grandParent = parent.Parent;
            return grandParent.Right != null && grandParent.Right == parent && parent.Left != null && parent.Left == node;
        }

        private static bool IsRRSituation(Node node)
        {
            var parent = node.Parent;
            var grandParent = parent.Parent;
            return grandParent.Right != null && grandParent.Right == parent && parent.Right != null && parent.Right == node;
        }

        private static bool IsRLSituation(Node node)
        {
            var parent = node.Parent;
            var grandParent = parent.Parent;
            return grandParent.Left != null && grandParent.Left == parent && parent.Right != null && parent.Right == node;
        }

        private Node FixViolations(Node node)
        {
            var parent = node.Parent;
            var grandParent = parent.Parent;

            if (node.Color != Color.Red || parent.Color == Color.Black)
            {
                // SS: there cannot be a RR-violation, we are done
                return parent;
            }

            // SS: we have a RR-violation 
            if (NodeHasTwoRedChildren(grandParent))
            {
                // SS: recolor

                // SS: flip parent's and parent's sibling's color from red to black
                grandParent.Left.Color = Color.Black;
                grandParent.Right.Color = Color.Black;

                if (grandParent != _root)
                {
                    grandParent.Color = Color.Red;
                }

                return parent;
            }

            // SS: we need to rotate

            if (IsLLSituation(node))
            {
                node = RotateRight(node.Parent, true);
            }
            else if (IsLRSituation(node))
            {
                RotateRight(node, false);
                node = RotateLeft(node, true);
            }
            else if (IsRLSituation(node))
            {
                RotateLeft(node, false);
                node = RotateRight(node, true);
            }
            else if (IsRRSituation(node))
            {
                node = RotateLeft(node.Parent, true);
            }
            else
            {
                throw new InvalidOperationException();
            }

            return node;
        }

        private class Node
        {
            public int Value { get; set; }
            public Node Parent { get; set; }
            public Node Left { get; set; }
            public Node Right { get; set; }
            public Color Color { get; set; }
        }


        [TestFixture]
        public class Tests
        {
            [Test]
            public void TestCreateTree1()
            {
                // Arrange
                var tree = new RedBlackTree();

                // Act
                tree.Insert(10);
                tree.Insert(-10);
                tree.Insert(20);

                // Assert
                Assert.AreEqual(10, tree._root.Value);
                Assert.AreEqual(-10, tree._root.Left.Value);
                Assert.AreEqual(20, tree._root.Right.Value);
            }
            
            [Test]
            public void TestCreateTree2()
            {
                // Arrange
                var tree = new RedBlackTree();

                // Act
                // Assert
                tree.Insert(10);
                Assert.AreEqual(10, tree._root.Value);
                Assert.AreEqual(RedBlackTree.Color.Black, tree._root.Color);
                
                tree.Insert(20);
                Assert.AreEqual(20, tree._root.Right.Value);
                Assert.AreEqual(RedBlackTree.Color.Red, tree._root.Right.Color);

                tree.Insert(-10);
                Assert.AreEqual(-10, tree._root.Left.Value);
                Assert.AreEqual(RedBlackTree.Color.Red, tree._root.Left.Color);

                // SS: insertion causes RR-violation, recolor case                
                tree.Insert(15);
                Assert.AreEqual(15, tree._root.Right.Left.Value);
                Assert.AreEqual(RedBlackTree.Color.Red, tree._root.Right.Left.Color);
                Assert.AreEqual(RedBlackTree.Color.Black, tree._root.Color);
                Assert.AreEqual(RedBlackTree.Color.Black, tree._root.Left.Color);
                Assert.AreEqual(RedBlackTree.Color.Black, tree._root.Right.Color);

                // SS: insertion causes RR-violation, RL type                
                tree.Insert(17);
                Assert.AreEqual(17, tree._root.Right.Value);

                Assert.AreEqual(RedBlackTree.Color.Black, tree._root.Color);
                Assert.AreEqual(RedBlackTree.Color.Black, tree._root.Left.Color);
                Assert.AreEqual(RedBlackTree.Color.Black, tree._root.Right.Color);
                Assert.AreEqual(RedBlackTree.Color.Red, tree._root.Right.Left.Color);
                Assert.AreEqual(RedBlackTree.Color.Red, tree._root.Right.Right.Color);
            }

            [Test]
            public void TestRotateRightAroundRoot()
            {
                // Arrange
                var tree = new RedBlackTree();
                tree.Insert(10);
                tree.Insert(5);

                // Act
                // Assert

                // SS: creates RR-violation, need to rotate right around root
                tree.Insert(3);
                Assert.AreEqual(5, tree._root.Value);
                Assert.AreEqual(RedBlackTree.Color.Black, tree._root.Color);

                Assert.AreEqual(3, tree._root.Left.Value);
                Assert.AreEqual(RedBlackTree.Color.Red, tree._root.Left.Color);

                Assert.AreEqual(10, tree._root.Right.Value);
                Assert.AreEqual(RedBlackTree.Color.Red, tree._root.Right.Color);
            }

            [Test]
            public void Test1()
            {
                // Arrange
                var tree = new RedBlackTree();
                tree.Insert(10);
                tree.Insert(-10);
                tree.Insert(20);
                tree.Insert(-20);
                tree.Insert(6);
                tree.Insert(25);

                // Act
                // Assert
                tree.Insert(4);
                Assert.AreEqual(4, tree._root.Left.Right.Left.Value);
                Assert.AreEqual(RedBlackTree.Color.Red, tree._root.Left.Right.Left.Color);

                Assert.AreEqual(-10, tree._root.Left.Value);
                Assert.AreEqual(RedBlackTree.Color.Red, tree._root.Left.Color);

                Assert.AreEqual(-20, tree._root.Left.Left.Value);
                Assert.AreEqual(RedBlackTree.Color.Black, tree._root.Left.Left.Color);

                Assert.AreEqual(6, tree._root.Left.Right.Value);
                Assert.AreEqual(RedBlackTree.Color.Black, tree._root.Left.Right.Color);

            }
            
        }
    }
}