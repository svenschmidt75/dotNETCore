#region

using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

#endregion

namespace LeetCode
{
    public class RedBlackTree
    {
        private Node _root;

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

        public void Remove(int v)
        {
            // SS: find node to delete
            var current = _root;
            while (current != null && current.Value != v)
            {
                current = v < current.Value ? current.Left : current.Right;
            }

            if (current == null)
            {
                // SS: there is no node with value v
                return;
            }

            // SS: does the node to be deleted have at most 1 child?
            if (IsLessThanTwoChildren(current))
            {
                Remove(current);
            }
            else
            {
                // SS: convert to a 0 or 1 child situation
                var inorderSuccessor = FindInorderSuccessor(current);
                current.Value = inorderSuccessor.Value;
                Remove(inorderSuccessor);
            }
        }

        private static Node FindInorderSuccessor(Node node)
        {
            // SS: find the left-most node in the right subtree
            if (node.Right == null)
            {
                throw new InvalidOperationException();
            }

            var prev = node;
            var current = node.Right;
            while (current != null)
            {
                prev = current;
                current = current.Left;
            }

            return prev;
        }

        private void Remove(Node node)
        {
            if (node.Color == Color.Red)
            {
                // SS: if the node to be deleted is red, simply delete node
                DisconnectNode(node);
            }
            else if (HasSingleRedRightChild(node))
            {
                // SS: if the node to be deleted is black and has a single red child, replace this
                // node with its child and recolor this node to black.
                node.Value = node.Right.Value;
                node.Right = null;
            }
            else if (HasSingleRedLeftChild(node))
            {
                // SS: if the node to be deleted has a single red child, replace this node with its
                // child and recolor this node to black.
                node.Value = node.Left.Value;
                node.Left = null;
            }
            else
            {
                // SS: we have a Double-Black situation
                var doubleBlackNode = node;
                while (true)
                {
                    if (IsCase1(doubleBlackNode))
                    {
                        // SS: if the root is the double-black node, it means that each path has
                        // one less black node, so exit
                        break;
                    }

                    if (IsCase2Left(doubleBlackNode))
                    {
                        // SS: left-rotation around parent
                        RotateLeft(doubleBlackNode.Parent.Right, false);

                        // SS: check this...
                        doubleBlackNode.Parent.Color = Color.Red;
                        doubleBlackNode.Parent.Parent.Color = Color.Black;

                        // SS: Case 2 is not a terminating case
                        continue;
                    }

                    if (IsCase2Right(doubleBlackNode))
                    {
                        // SS: right-rotation around parent
                        RotateRight(doubleBlackNode.Parent.Left, false);

                        // SS: check this...
                        doubleBlackNode.Parent.Color = Color.Red;
                        doubleBlackNode.Parent.Parent.Color = Color.Black;

                        // SS: Case 2 is not a terminating case
                        continue;
                    }

                    if (IsCase3Left(doubleBlackNode))
                    {
                        // SS: move double-black node to parent
                        var parent = doubleBlackNode.Parent;
                        parent.Right.Color = Color.Red;

                        if (doubleBlackNode is {Left: null, Right: null})
                        {
                            // SS: Double-Black node has no children, so delete
                            DisconnectNode(doubleBlackNode);
                        }

                        doubleBlackNode = parent;

                        // SS: case 3 is not a terminating case
                        continue;
                    }

                    if (IsCase3Right(doubleBlackNode))
                    {
                        // SS: move double-black node to parent
                        var parent = doubleBlackNode.Parent;
                        parent.Left.Color = Color.Red;

                        if (doubleBlackNode is {Left: null, Right: null})
                        {
                            // SS: Double-Black node has no children, so delete
                            DisconnectNode(doubleBlackNode);
                        }

                        doubleBlackNode = parent;

                        // SS: case 3 is not a terminating case
                        continue;
                    }

                    if (IsCase4Left(doubleBlackNode))
                    {
                        // SS: recolor
                        doubleBlackNode.Parent.Color = Color.Black;
                        doubleBlackNode.Parent.Right.Color = Color.Red;

                        if (doubleBlackNode is {Left: null, Right: null})
                        {
                            // SS: Double-Black node has no children, so delete
                            DisconnectNode(doubleBlackNode);
                        }

                        // SS: case 4 is terminating
                        break;
                    }

                    if (IsCase4Right(doubleBlackNode))
                    {
                        // SS: recolor
                        doubleBlackNode.Parent.Color = Color.Black;
                        doubleBlackNode.Parent.Left.Color = Color.Red;

                        if (doubleBlackNode is {Left: null, Right: null})
                        {
                            // SS: Double-Black node has no children, so delete
                            DisconnectNode(doubleBlackNode);
                        }

                        // SS: case 4 is terminating
                        break;
                    }

                    if (IsCase5Left(doubleBlackNode))
                    {
                        // SS: right-rotation around sibling
                        RotateRight(doubleBlackNode.Parent.Right.Left, false);
                        doubleBlackNode.Parent.Right.Color = Color.Black;
                        doubleBlackNode.Parent.Right.Right.Color = Color.Red;

                        // SS: case 5 is not terminating
                        continue;
                    }

                    if (IsCase5Right(doubleBlackNode))
                    {
                        // SS: left-rotation around sibling
                        RotateLeft(doubleBlackNode.Parent.Left.Right, false);
                        doubleBlackNode.Parent.Left.Color = Color.Black;
                        doubleBlackNode.Parent.Left.Left.Color = Color.Red;

                        // SS: case 5 is not terminating
                        continue;
                    }

                    if (IsCase6Left(doubleBlackNode))
                    {
                        // SS: left-rotation around parent, from sibling
                        RotateLeft(doubleBlackNode.Parent.Right, false);

                        // SS: recolor
                        doubleBlackNode.Parent.Parent.Color = doubleBlackNode.Parent.Color;
                        doubleBlackNode.Parent.Color = Color.Black;
                        doubleBlackNode.Parent.Parent.Right.Color = Color.Black;

                        if (doubleBlackNode is {Left: null, Right: null})
                        {
                            // SS: Double-Black node has no children, so delete
                            DisconnectNode(doubleBlackNode);
                        }

                        // SS: case 6 in terminating
                        break;
                    }

                    if (IsCase6Right(doubleBlackNode))
                    {
                        // SS: right-rotation around parent
                        RotateRight(doubleBlackNode.Parent.Left, false);

                        // SS: recolor
                        doubleBlackNode.Parent.Parent.Color = doubleBlackNode.Parent.Color;
                        doubleBlackNode.Parent.Color = Color.Black;
                        doubleBlackNode.Parent.Parent.Left.Color = Color.Black;

                        if (doubleBlackNode is {Left: null, Right: null})
                        {
                            // SS: Double-Black node has no children, so delete
                            DisconnectNode(doubleBlackNode);
                        }

                        // SS: case 6 in terminating
                        break;
                    }

                    throw new InvalidOperationException();
                }
            }
        }

        private static void DisconnectNode(Node node)
        {
            var parent = node.Parent;
            if (parent.Left == node)
            {
                parent.Left = null;
            }
            else
            {
                parent.Right = null;
            }
        }

        private static bool IsCase6Left(Node doubleBlackNode)
        {
            var parent = doubleBlackNode.Parent;
            if (parent.Left != doubleBlackNode)
            {
                return false;
            }

            var sibling = parent.Right;
            if (sibling is not {Color: Color.Black})
            {
                return false;
            }

            var y = sibling.Right;
            return y is {Color: Color.Red};
        }

        private static bool IsCase6Right(Node doubleBlackNode)
        {
            var parent = doubleBlackNode.Parent;
            if (parent.Right != doubleBlackNode)
            {
                return false;
            }

            var sibling = parent.Left;
            if (sibling is not {Color: Color.Black})
            {
                return false;
            }

            var y = sibling.Left;
            return y is {Color: Color.Red};
        }

        private static bool IsCase5Left(Node doubleBlackNode)
        {
            var parent = doubleBlackNode.Parent;
            if (parent.Left != doubleBlackNode)
            {
                return false;
            }

            var sibling = parent.Right;
            if (sibling is not {Color: Color.Black})
            {
                return false;
            }

            var x = sibling.Left;
            if (x is not {Color: Color.Red})
            {
                return false;
            }

            var y = sibling.Right;
            return y is not {Color: Color.Red};
        }

        private static bool IsCase5Right(Node doubleBlackNode)
        {
            var parent = doubleBlackNode.Parent;
            if (parent.Right != doubleBlackNode)
            {
                return false;
            }

            var sibling = parent.Left;
            if (sibling is not {Color: Color.Black})
            {
                return false;
            }

            var x = sibling.Right;
            if (x is not {Color: Color.Red})
            {
                return false;
            }

            var y = sibling.Left;
            return y is not {Color: Color.Red};
        }

        private static bool IsCase4Left(Node doubleBlackNode)
        {
            var parent = doubleBlackNode.Parent;
            if (parent.Left != doubleBlackNode || parent.Color != Color.Red)
            {
                return false;
            }

            var sibling = parent.Right;
            if (sibling is not {Color: Color.Black})
            {
                return false;
            }

            var x = sibling.Left;
            if (x is {Color: Color.Red})
            {
                return false;
            }

            var y = sibling.Right;
            return y is not {Color: Color.Red};
        }

        private static bool IsCase4Right(Node doubleBlackNode)
        {
            var parent = doubleBlackNode.Parent;
            if (parent.Right != doubleBlackNode || parent.Color != Color.Red)
            {
                return false;
            }

            var sibling = parent.Left;
            if (sibling is not {Color: Color.Black})
            {
                return false;
            }

            var x = sibling.Left;
            if (x is {Color: Color.Red})
            {
                return false;
            }

            var y = sibling.Right;
            return y is not {Color: Color.Red};
        }

        private static bool IsCase3Left(Node doubleBlackNode)
        {
            var parent = doubleBlackNode.Parent;
            if (parent.Left != doubleBlackNode || parent.Color != Color.Black)
            {
                return false;
            }

            var sibling = parent.Right;
            if (sibling is not {Color: Color.Black})
            {
                return false;
            }

            var x = sibling.Left;
            if (x is {Color: Color.Red})
            {
                return false;
            }

            var y = sibling.Right;
            return y is not {Color: Color.Red};
        }

        private static bool IsCase3Right(Node doubleBlackNode)
        {
            var parent = doubleBlackNode.Parent;
            if (parent.Right != doubleBlackNode || parent.Color != Color.Black)
            {
                return false;
            }

            var sibling = parent.Left;
            if (sibling is not {Color: Color.Black})
            {
                return false;
            }

            var x = sibling.Left;
            if (x is {Color: Color.Red})
            {
                return false;
            }

            var y = sibling.Right;
            return y is not {Color: Color.Red};
        }

        private static bool IsCase2Left(Node doubleBlackNode)
        {
            var parent = doubleBlackNode.Parent;
            if (parent.Left != doubleBlackNode || parent.Color != Color.Black)
            {
                return false;
            }

            var sibling = parent.Right;
            if (sibling is not {Color: Color.Red})
            {
                return false;
            }

            var x = sibling.Left;
            if (x is not {Color: Color.Black})
            {
                return false;
            }

            var y = sibling.Right;
            return y is not {Color: Color.Red};
        }

        private static bool IsCase2Right(Node doubleBlackNode)
        {
            var parent = doubleBlackNode.Parent;
            if (parent.Right != doubleBlackNode || parent.Color != Color.Black)
            {
                return false;
            }

            var sibling = parent.Left;
            if (sibling is not {Color: Color.Red})
            {
                return false;
            }

            var x = sibling.Left;
            if (x is not {Color: Color.Black})
            {
                return false;
            }

            var y = sibling.Right;
            return y is not {Color: Color.Red};
        }

        private bool IsCase1(Node doubleBlackNode)
        {
            return doubleBlackNode == _root;
        }

        private static bool HasSingleRedLeftChild(Node node)
        {
            return node.Color == Color.Black && node.Right == null && node.Left is {Color: Color.Red};
        }

        private static bool HasSingleRedRightChild(Node node)
        {
            return node.Color == Color.Black && node.Left == null && node.Right is {Color: Color.Red};
        }

        private static bool IsLessThanTwoChildren(Node node)
        {
            var cnt = 0;
            if (node.Left != null)
            {
                cnt++;
            }

            if (node.Right != null)
            {
                cnt++;
            }

            return cnt < 2;
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

                // SS: if a rotation happened at a lower level, nodeLeft becomes
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
                var nodeRight = InsertInternal(node.Right, v);

                // SS: if a rotation happened at a lower level, nodeRight becomes
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

        private Node RotateLeft(Node node, bool flipColors)
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
            else
            {
                _root = node;
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

        private Node RotateRight(Node node, bool flipColors)
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
            else
            {
                _root = node;
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

        public (bool found, int value) LowerBound(int v)
        {
            // SS: Find the first node whose value is not < v, or
            // the first whose value is >= v, if it exists.
            bool found = false;
            int lowerBound = -1;
            Node current = _root;
            while (current != null)
            {
                if (current.Value < v)
                {
                    current = current.Right;
                }
                else
                {
                    found = true;
                    lowerBound = current.Value;
                    current = current.Left;
                }
            }

            return (found, lowerBound);
        } 

        public (bool found, int value) UpperBound(int v)
        {
            // SS: Find the smallest node whose value is > v, if it exists.
            bool found = false;
            int upperBound = -1;
            Node current = _root;
            while (current != null)
            {
                if (current.Value <= v)
                {
                    current = current.Right;
                }
                else
                {
                    if (found)
                    {
                        if (upperBound > current.Value)
                        {
                            upperBound = current.Value;
                        }
                        else
                        {
                            // SS: there cannot be a smaller upper bound
                            break;
                        }
                    }
                    else
                    {
                        found = true;
                        upperBound = current.Value;
                    }

                    current = current.Left;
                }
            }
            
            return (found, upperBound);
        } 

        internal enum Color
        {
            Black
            , Red
        }

        internal class Node
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
                Assert.True(IsRedBlackTree(tree._root));
                var result = InorderTravsersal(tree._root);
                CollectionAssert.AreEqual(new[]
                {
                    (-10, Color.Red)
                    , (10, Color.Black)
                    , (20, Color.Red)
                }, result);
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
                Assert.AreEqual(Color.Black, tree._root.Color);

                tree.Insert(20);
                Assert.AreEqual(20, tree._root.Right.Value);
                Assert.AreEqual(Color.Red, tree._root.Right.Color);

                tree.Insert(-10);
                Assert.AreEqual(-10, tree._root.Left.Value);
                Assert.AreEqual(Color.Red, tree._root.Left.Color);

                // SS: insertion causes RR-violation, recolor case                
                tree.Insert(15);
                Assert.AreEqual(15, tree._root.Right.Left.Value);
                Assert.AreEqual(Color.Red, tree._root.Right.Left.Color);
                Assert.AreEqual(Color.Black, tree._root.Color);
                Assert.AreEqual(Color.Black, tree._root.Left.Color);
                Assert.AreEqual(Color.Black, tree._root.Right.Color);

                // SS: insertion causes RR-violation, RL type                
                tree.Insert(17);
                Assert.AreEqual(17, tree._root.Right.Value);

                Assert.AreEqual(Color.Black, tree._root.Color);
                Assert.AreEqual(Color.Black, tree._root.Left.Color);
                Assert.AreEqual(Color.Black, tree._root.Right.Color);
                Assert.AreEqual(Color.Red, tree._root.Right.Left.Color);
                Assert.AreEqual(Color.Red, tree._root.Right.Right.Color);
            }

            [Test]
            public void TestRotateRightAroundRoot()
            {
                // Arrange
                var tree = new RedBlackTree();
                tree.Insert(10);
                tree.Insert(5);

                // Act

                // SS: creates RR-violation, need to rotate right around root
                tree.Insert(3);

                // Assert
                Assert.True(IsRedBlackTree(tree._root));
                var result = InorderTravsersal(tree._root);
                CollectionAssert.AreEqual(new[]
                {
                    (3, Color.Red)
                    , (5, Color.Black)
                    , (10, Color.Red)
                }, result);
            }

            [Test]
            public void TestCreateTree3()
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
                tree.Insert(4);

                // Assert
                Assert.True(IsRedBlackTree(tree._root));
            }

            [Test]
            public void TestCreateTree4()
            {
                // Arrange
                var tree = new RedBlackTree();
                tree.Insert(10);
                tree.Insert(20);
                tree.Insert(-10);
                tree.Insert(15);
                tree.Insert(17);
                tree.Insert(40);
                tree.Insert(50);
                tree.Insert(60);

                // Act
                // Assert
                Assert.True(IsRedBlackTree(tree._root));
            }

            [Test]
            public void TestDeleteRedNode()
            {
                // https://youtu.be/CTvfzU_uNKE?t=113

                // Arrange
                var tree = new RedBlackTree();

                var root = new Node {Value = 10, Color = Color.Black};
                tree._root = root;

                root.Left = new Node
                {
                    Value = 5
                    , Color = Color.Red
                    , Left = new Node {Value = -5, Color = Color.Black}
                    , Right = new Node {Value = 7, Color = Color.Black}
                };

                root.Right = new Node
                {
                    Value = 30
                    , Color = Color.Red
                    , Left = new Node {Value = 20, Color = Color.Black}
                    , Right = new Node
                    {
                        Value = 38
                        , Color = Color.Black
                        , Left = new Node {Value = 35, Color = Color.Red}
                    }
                };

                root.Left.Parent = root;
                root.Right.Parent = root;

                root.Left.Left.Parent = root.Left;
                root.Left.Right.Parent = root.Left;

                root.Right.Right.Parent = root.Right;
                root.Right.Left.Parent = root.Right;

                root.Right.Right.Left.Parent = root.Right.Right;

                // Act
                tree.Remove(30);

                // Assert
                Assert.True(IsRedBlackTree(tree._root));
                var result = InorderTravsersal(tree._root);
                CollectionAssert.AreEqual(new[]
                {
                    (-5, Color.Black)
                    , (5, Color.Red)
                    , (7, Color.Black)
                    , (10, Color.Black)
                    , (20, Color.Black)
                    , (35, Color.Red)
                    , (38, Color.Black)
                }, result);
            }

            [Test]
            public void TestDeleteBlackNodeWithSingleBlackChild()
            {
                // https://youtu.be/CTvfzU_uNKE?t=222

                // Arrange
                var tree = new RedBlackTree();

                var root = new Node {Value = 10, Color = Color.Black};
                tree._root = root;

                root.Left = new Node
                {
                    Value = 5
                    , Color = Color.Black
                    , Left = new Node {Value = -5, Color = Color.Black}
                    , Right = new Node {Value = 7, Color = Color.Black}
                };

                root.Right = new Node
                {
                    Value = 30
                    , Color = Color.Black
                    , Left = new Node {Value = 20, Color = Color.Black}
                    , Right = new Node
                    {
                        Value = 38
                        , Color = Color.Red
                        , Left = new Node
                        {
                            Value = 32
                            , Color = Color.Black
                            , Right = new Node {Value = 35, Color = Color.Red}
                        }
                        , Right = new Node {Value = 41, Color = Color.Black}
                    }
                };

                root.Left.Parent = root;
                root.Right.Parent = root;

                root.Left.Left.Parent = root.Left;
                root.Left.Right.Parent = root.Left;

                root.Right.Right.Parent = root.Right;
                root.Right.Left.Parent = root.Right;

                root.Right.Right.Left.Parent = root.Right.Right;
                root.Right.Right.Right.Parent = root.Right.Right;

                root.Right.Right.Left.Right.Parent = root.Right.Right.Left;

                // Act
                tree.Remove(30);

                // Assert
                Assert.True(IsRedBlackTree(tree._root));
                var result = InorderTravsersal(tree._root);
                CollectionAssert.AreEqual(new[]
                {
                    (-5, Color.Black)
                    , (5, Color.Black)
                    , (7, Color.Black)
                    , (10, Color.Black)
                    , (20, Color.Black)
                    , (32, Color.Black)
                    , (35, Color.Black)
                    , (38, Color.Red)
                    , (41, Color.Black)
                }, result);
            }

            [Test]
            public void TestCase4Left()
            {
                // https://youtu.be/CTvfzU_uNKE?t=580

                // Arrange
                var tree = new RedBlackTree();

                var root = new Node {Value = 10, Color = Color.Black};
                tree._root = root;

                root.Left = new Node {Value = -10, Color = Color.Black};

                root.Right = new Node
                {
                    Value = 30
                    , Color = Color.Red
                    , Left = new Node {Value = 20, Color = Color.Black}
                    , Right = new Node {Value = 38, Color = Color.Black}
                };

                root.Left.Parent = root;
                root.Right.Parent = root;

                root.Right.Right.Parent = root.Right;
                root.Right.Left.Parent = root.Right;

                // Act
                tree.Remove(20);

                // Assert
                Assert.True(IsRedBlackTree(tree._root));
                var result = InorderTravsersal(tree._root);
                CollectionAssert.AreEqual(new[]
                {
                    (-10, Color.Black)
                    , (10, Color.Black)
                    , (30, Color.Black)
                    , (38, Color.Red)
                }, result);
            }

            [Test]
            public void TestCase6Left()
            {
                // https://youtu.be/CTvfzU_uNKE?t=719

                // Arrange
                var tree = new RedBlackTree();

                var root = new Node {Value = 10, Color = Color.Black};
                tree._root = root;

                root.Left = new Node {Value = -10, Color = Color.Black};

                root.Right = new Node
                {
                    Value = 30
                    , Color = Color.Black
                    , Left = new Node {Value = 25, Color = Color.Red}
                    , Right = new Node {Value = 40, Color = Color.Red}
                };

                root.Left.Parent = root;
                root.Right.Parent = root;

                root.Right.Right.Parent = root.Right;
                root.Right.Left.Parent = root.Right;

                // Act
                tree.Remove(-10);

                // Assert
                Assert.True(IsRedBlackTree(tree._root));
                var result = InorderTravsersal(tree._root);
                CollectionAssert.AreEqual(new[]
                {
                    (10, Color.Black)
                    , (25, Color.Red)
                    , (30, Color.Black)
                    , (40, Color.Black)
                }, result);
            }

            [Test]
            public void TestCase3Left()
            {
                // https://youtu.be/CTvfzU_uNKE?t=913

                // Arrange
                var tree = new RedBlackTree();

                var root = new Node {Value = 10, Color = Color.Black};
                tree._root = root;

                root.Left = new Node {Value = -10, Color = Color.Black};

                root.Right = new Node
                {
                    Value = 30
                    , Color = Color.Black
                };

                root.Left.Parent = root;
                root.Right.Parent = root;

                // Act
                tree.Remove(-10);

                // Assert
                Assert.True(IsRedBlackTree(tree._root));
                var result = InorderTravsersal(tree._root);
                CollectionAssert.AreEqual(new[]
                {
                    (10, Color.Black)
                    , (30, Color.Red)
                }, result);
            }

            [Test]
            public void TestCase5Left()
            {
                // https://youtu.be/CTvfzU_uNKE?t=1078

                // Arrange
                var tree = new RedBlackTree();

                var root = new Node {Value = 10, Color = Color.Black};
                tree._root = root;

                root.Left = new Node
                {
                    Value = -30
                    , Color = Color.Black
                    , Left = new Node {Value = -40, Color = Color.Black}
                    , Right = new Node {Value = -20, Color = Color.Black}
                };

                root.Right = new Node
                {
                    Value = 50
                    , Color = Color.Black
                    , Left = new Node
                    {
                        Value = 30
                        , Color = Color.Red
                        , Left = new Node {Value = 15, Color = Color.Black}
                        , Right = new Node {Value = 40, Color = Color.Black}
                    }
                    , Right = new Node
                    {
                        Value = 70
                        , Color = Color.Black
                    }
                };

                root.Left.Parent = root;
                root.Right.Parent = root;

                root.Left.Left.Parent = root.Left;
                root.Left.Right.Parent = root.Left;

                root.Right.Right.Parent = root.Right;
                root.Right.Left.Parent = root.Right;

                root.Right.Left.Left.Parent = root.Right.Left;
                root.Right.Left.Right.Parent = root.Right.Left;

                // Act
                tree.Remove(-40);

                // Assert
                Assert.True(IsRedBlackTree(tree._root));
                var result = InorderTravsersal(tree._root);
                CollectionAssert.AreEqual(new[]
                {
                    (-30, Color.Black)
                    , (-20, Color.Red)
                    , (10, Color.Black)
                    , (15, Color.Black)
                    , (30, Color.Black)
                    , (40, Color.Black)
                    , (50, Color.Black)
                    , (70, Color.Black)
                }, result);
            }

            [Test]
            public void TestCase2Left()
            {
                // https://youtu.be/CTvfzU_uNKE?t=1472

                // Arrange
                var tree = new RedBlackTree();

                var root = new Node {Value = 10, Color = Color.Black};
                tree._root = root;

                root.Left = new Node
                {
                    Value = -10
                    , Color = Color.Black
                    , Left = new Node {Value = -20, Color = Color.Black}
                    , Right = new Node {Value = -5, Color = Color.Black}
                };

                root.Right = new Node
                {
                    Value = 40
                    , Color = Color.Black
                    , Left = new Node {Value = 20, Color = Color.Black}
                    , Right = new Node
                    {
                        Value = 60
                        , Color = Color.Red
                        , Left = new Node {Value = 50, Color = Color.Black}
                        , Right = new Node {Value = 80, Color = Color.Black}
                    }
                };

                root.Left.Parent = root;
                root.Right.Parent = root;

                root.Left.Left.Parent = root.Left;
                root.Left.Right.Parent = root.Left;

                root.Right.Right.Parent = root.Right;
                root.Right.Left.Parent = root.Right;

                root.Right.Right.Left.Parent = root.Right.Right;
                root.Right.Right.Right.Parent = root.Right.Right;

                // Act
                tree.Remove(10);

                // Assert
                Assert.True(IsRedBlackTree(tree._root));
                var result = InorderTravsersal(tree._root);
                CollectionAssert.AreEqual(new[]
                {
                    (-20, Color.Black)
                    , (-10, Color.Black)
                    , (-5, Color.Black)
                    , (20, Color.Black)
                    , (40, Color.Black)
                    , (50, Color.Red)
                    , (60, Color.Black)
                    , (80, Color.Black)
                }, result);
            }

            private static IList<(int value, Color color)> InorderTravsersal(Node node)
            {
                var result = new List<(int, Color)>();

                var stack = new Stack<Node>();
                var current = node;
                while (true)
                {
                    if (current != null)
                    {
                        stack.Push(current);
                        current = current.Left;
                    }
                    else
                    {
                        if (stack.Any() == false)
                        {
                            break;
                        }

                        current = stack.Pop();
                        result.Add((current.Value, current.Color));
                        current = current.Right;
                    }
                }

                return result;
            }

            private static bool IsRedBlackTree(Node root)
            {
                var nBlackNodesInPath = 0;

                bool Check(Node n, Node parent, int nBlack)
                {
                    // SS: base case
                    if (n == null)
                    {
                        if (nBlackNodesInPath == 0)
                        {
                            nBlackNodesInPath = nBlack;
                        }

                        return nBlackNodesInPath == nBlack;
                    }

                    // SS: check red-red violation
                    if (n.Color == Color.Red && parent.Color == Color.Red)
                    {
                        return false;
                    }

                    nBlack = n.Color == Color.Black ? nBlack + 1 : nBlack;

                    if (Check(n.Left, n, nBlack) == false)
                    {
                        return false;
                    }

                    if (Check(n.Right, n, nBlack) == false)
                    {
                        return false;
                    }

                    return true;
                }

                return Check(root, root, 0);
            }

            [Test]
            public void TestInsertRandom()
            {
                // Arrange
                const int minValue = -1000;
                const int maxValue = 1000;
                const int nNodes = 1000;
                var seedValue = DateTime.Now.Millisecond;

                Console.WriteLine($"Using seed {seedValue}");

                var rnd = new Random(seedValue);

                // Act
                var rbTree = new RedBlackTree();
                for (var i = 0; i < nNodes; i++)
                {
                    var prevResult = InorderTravsersal(rbTree._root);

                    var nodeValue = rnd.Next(minValue, maxValue);

                    Console.WriteLine($"Inserting value {nodeValue}...");
                    rbTree.Insert(nodeValue);

                    if (IsRedBlackTree(rbTree._root) == false)
                    {
                        Console.WriteLine($"Inserting value {nodeValue} with seed {seedValue} causes a violation");
                        Assert.Fail();
                    }
                }
            }

            [Test]
            public void TestDeleteRandom()
            {
                // Arrange
                const int minValue = -1000;
                const int maxValue = 1000;
                const int nNodes = 1000;
                var seedValue = DateTime.Now.Millisecond;

                var rnd = new Random(seedValue);

                Console.WriteLine($"Using seed {seedValue}");

                var values = new List<int>();

                var rbTree = new RedBlackTree();
                for (var i = 0; i < nNodes; i++)
                {
                    var prevResult = InorderTravsersal(rbTree._root);

                    var nodeValue = rnd.Next(minValue, maxValue);
                    values.Add(nodeValue);

                    Console.WriteLine($"Inserting value {nodeValue}...");
                    rbTree.Insert(nodeValue);

                    if (IsRedBlackTree(rbTree._root) == false)
                    {
                        Console.WriteLine($"Inserting value {nodeValue} with seed {seedValue} causes a violation");
                        Assert.Fail();
                    }
                }

                // Act
                for (var i = 0; i < nNodes; i++)
                {
                    var nodeValueIdx = rnd.Next(0, nNodes);
                    var nodeValue = values[nodeValueIdx];

                    var prevResult = InorderTravsersal(rbTree._root);

                    Console.WriteLine($"Removing value {nodeValue}...");
                    rbTree.Remove(nodeValue);

                    if (IsRedBlackTree(rbTree._root) == false)
                    {
                        Console.WriteLine($"Removing value {nodeValue} with seed {seedValue} causes a violation");
                        Assert.Fail();
                    }
                }
            }
            
            [TestCase(11, true, 12)]
            [TestCase(3, true, 4)]
            [TestCase(83, true, 83)]
            [TestCase(183, false, -1)]
            public void TestLowerBound(int v, bool expectedFound, int expectedValue)
            {
                // Arrange
                var rbTree = new RedBlackTree();
                rbTree.Insert(8);
                rbTree.Insert(5);
                rbTree.Insert(10);
                rbTree.Insert(2);
                rbTree.Insert(7);
                rbTree.Insert(9);
                rbTree.Insert(15);
                rbTree.Insert(4);
                rbTree.Insert(12);
                rbTree.Insert(83);

                Assert.True(IsRedBlackTree(rbTree._root));

                // Act
                (bool found, int value) = rbTree.LowerBound(v);
                
                // Assert
                Assert.AreEqual(expectedFound, found);
                if (found)
                {
                    Assert.AreEqual(expectedValue, value);
                }
            }
            
            [TestCase(11, true, 12)]
            [TestCase(12, true, 15)]
            [TestCase(83, false, -1)]
            public void TestUpperBound(int v, bool expectedFound, int expectedValue)
            {
                // Arrange
                var rbTree = new RedBlackTree();
                rbTree.Insert(8);
                rbTree.Insert(5);
                rbTree.Insert(10);
                rbTree.Insert(2);
                rbTree.Insert(7);
                rbTree.Insert(9);
                rbTree.Insert(15);
                rbTree.Insert(4);
                rbTree.Insert(12);
                rbTree.Insert(83);

                Assert.True(IsRedBlackTree(rbTree._root));

                // Act
                (bool found, int value) = rbTree.UpperBound(v);
                
                // Assert
                Assert.AreEqual(expectedFound, found);
                if (found)
                {
                    Assert.AreEqual(expectedValue, value);
                }
            }

        }
    }
}