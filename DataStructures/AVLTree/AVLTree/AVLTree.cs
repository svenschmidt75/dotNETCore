using System;

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

            return node;
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
    }
}