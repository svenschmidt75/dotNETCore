using System;
using System.Collections.Generic;

namespace BinarySearchTree
{
    public class BinarySearchTree<T> where T : IComparable
    {
        private Node<T> Root { get; set; }

        public void Insert(T item)
        {
            var node = new Node<T> {Value = item};
            if (Root == null)
            {
                Root = node;
                return;
            }

            // SS: find the node to add the new node to
            var currentNode = Root;
            while (currentNode != null)
            {
                var parent = currentNode;
                if (item.CompareTo(currentNode.Value) < 1)
                {
                    currentNode = currentNode.Left;
                    if (currentNode == null)
                    {
                        parent.Left = node;
                        break;
                    }
                }
                else if (item.CompareTo(currentNode.Value) == 1)
                {
                    currentNode = currentNode.Right;
                    if (currentNode == null)
                    {
                        parent.Right = node;
                        break;
                    }
                }
            }
        }

        public bool Search(T item)
        {
            if (Root == null) return false;

            var currentNode = Root;
            while (currentNode != null)
            {
                var parent = currentNode;
                if (item.CompareTo(currentNode.Value) == -1)
                    currentNode = currentNode.Left;
                else if (item.CompareTo(currentNode.Value) == 1)
                    currentNode = currentNode.Right;
                else
                    return true;
            }

            return false;
        }

        private void TraverseInOrder(Node<T> node, List<T> l)
        {
            if (node == null) return;

            TraverseInOrder(node.Left, l);
            l.Add(node.Value);
            TraverseInOrder(node.Right, l);
        }

        public IEnumerable<T> TraverseInOrder()
        {
            var l = new List<T>();

            if (Root == null) return l;

            TraverseInOrder(Root, l);
            return l;
        }
    }
}