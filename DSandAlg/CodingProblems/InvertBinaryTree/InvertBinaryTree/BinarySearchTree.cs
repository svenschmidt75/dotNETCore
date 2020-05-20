using System;
using System.Collections.Generic;
using System.Linq;

namespace InvertBinaryTree
{
    public class BinarySearchTree<T> where T : IComparable
    {
        public Node<T> Root { get; set; }

        public static BinarySearchTree<T> Invert(BinarySearchTree<T> bt)
        {
            var invertedBt = new BinarySearchTree<T>();
            invertedBt.Root = new Node<T> {Value = bt.Root.Value};

            var queue = new Queue<(Node<T>, Node<T>)>();
            queue.Enqueue((bt.Root, invertedBt.Root));

            while (queue.Any())
            {
                var (primaryNode, secondaryNode) = queue.Dequeue();
                if (primaryNode.Left != null)
                {
                    var n = new Node<T> {Value = primaryNode.Left.Value};
                    secondaryNode.Right = n;
                    queue.Enqueue((primaryNode.Left, n));
                }

                if (primaryNode.Right != null)
                {
                    var n = new Node<T> {Value = primaryNode.Right.Value};
                    secondaryNode.Left = n;
                    queue.Enqueue((primaryNode.Right, n));
                }
            }

            return invertedBt;
        }
    }
}