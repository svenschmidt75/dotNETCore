using System;
using System.Diagnostics;

namespace AVLTree
{
    public class AVLTree
    {
        public Node Root { get; set; }

        public static AVLTree Create()
        {
            return new AVLTree();
        }

        public Node Insert(int value)
        {
            Root = InsertInternal(Root, value);
            return Root;
        }

        private static Node InsertInternal(Node node, int value)
        {
            // SS: Base case
            if (node == null) return new Node {Value = value, Height = 0};

            if (value < node.Value)
                node.Left = InsertInternal(node.Left, value);
            else if (value > node.Value) node.Right = InsertInternal(node.Right, value);

            // SS: Update height
            node.Height = UpdateHeight(node);

            // SS: fix AVL violations
            node = FixViolations(node, value);

            return node;
        }

        private static int BalanceFactor(Node node)
        {
            var leftHeight = node.Left?.Height ?? -1;
            var rightHeight = node.Right?.Height ?? -1;
            var diff = leftHeight - rightHeight;
            return diff;
        }

        private static Node FixViolations(Node node, int value)
        {
            var balanceFactor = BalanceFactor(node);

            // SS: Case 1: double-left heavy
            if (balanceFactor > 1)
            {
                if (value < node.Left.Value)
                {
                    Console.WriteLine($"Node {node.Value}: left-left heavy");

                    // SS: double-left heavy
                    node = RotateRight(node);
                }
                else
                {
                    Console.WriteLine($"Node {node.Value}: left-right heavy");

                    // SS: left-right heavy
                    node.Left = RotateLeft(node.Left);
                    node = RotateRight(node);
                }
            }
            else if (balanceFactor < -1)
            {
                if (value > node.Right.Value)
                {
                    Console.WriteLine($"Node {node.Value}: right-right heavy");

                    // SS: double-right heavy
                    node = RotateLeft(node);
                }
                else
                {
                    Console.WriteLine($"Node {node.Value}: right-left heavy");

                    // SS: right-left heavy
                    node.Right = RotateRight(node.Right);
                    node = RotateLeft(node);
                }
            }

            return node;
        }

        private static Node RotateLeft(Node node)
        {
            /*         D                   B
             *          \                /  \
             *           B       =>     D    A
             *            \
             *              A
             */

            // SS: node is D
            Console.WriteLine($"Node {node.Value}: rotate left");

            var nodeB = node.Right;

            // SS: store left child of node B
            var tmpNode = nodeB.Left;

            // SS: node B's left child becomes D
            nodeB.Left = node;

            // SS: node D's right child becomes tmpNode
            node.Right = tmpNode;

            // SS: Update height
            node.Height = UpdateHeight(node);
            nodeB.Height = UpdateHeight(nodeB);

            // SS: return the new root node
            return nodeB;
        }

        private static Node RotateRight(Node node)
        {
            /*            D               B
             *          /               /  \
             *         B       =>     A     D
             *       /
             *     A
             */

            // SS: node is D
            Console.WriteLine($"Node {node.Value}: rotate right");

            var nodeB = node.Left;

            // SS: store right child of node B
            var tmpNode = nodeB.Right;

            // SS: node B's right child becomes D
            nodeB.Right = node;

            // SS: node D's left child becomes tmpNode
            node.Left = tmpNode;

            // SS: Update height
            node.Height = UpdateHeight(node);
            nodeB.Height = UpdateHeight(nodeB);

            // SS: return the new root node
            return nodeB;
        }

        private static int UpdateHeight(Node node)
        {
            // SS: Base case
            if (node == null)
                // SS: by definition, the height of a null node is -1
                return -1;

            var leftHeight = UpdateHeight(node.Left);
            var rightHeight = UpdateHeight(node.Right);
            var height = Math.Max(leftHeight, rightHeight) + 1;
            return height;
        }

        public Node Remove(int value)
        {
            // SS: remove node with value 2
            Root = RemoveInternal(Root, value);
            return Root;
        }

        private Node RemoveInternal(Node node, int value)
        {
            if (node == null) return null;

            if (node.Value == value)
            {
                // SS: delete this node

                // Case 1: no children
                if (node.Left == null && node.Right == null)
                    // SS: nothing to do
                    return null;

                // Case 2: left child, no right child
                if (node.Left != null && node.Right == null)
                    // SS: return left child as the new node
                    return node.Left;

                // Case 3: no left child, right child
                if (node.Right != null && node.Left == null)
                {
                    // SS: return right child as the new node
                    return node.Right;
                }

                // Case 4: left child, right child
                // Either return largest node in left subtree as new new,
                // or smallest node in right subtree...
                var n = FindAndDetachLargestNode(node, node.Left);
                n.Left = node.Left;
                n.Right = node.Right;
                return n;
            }

            if (node.Left != null && value < node.Value) node.Left = RemoveInternal(node.Left, value);

            if (node.Right != null && value > node.Value) node.Right = RemoveInternal(node.Right, value);

            // SS: node does not exist in tree
            return node;
        }

        private Node FindAndDetachLargestNode(Node parent, Node node)
        {
            // SS: The largest node is the furthest one all right subtrees...
            if (node == null) throw new ArgumentException();

            if (node.Right == null)
            {
                Debug.Assert(parent.Right == node);
                parent.Right = null;
                return node;
            }

            return FindAndDetachLargestNode(node, node.Right);
        }
    }
}