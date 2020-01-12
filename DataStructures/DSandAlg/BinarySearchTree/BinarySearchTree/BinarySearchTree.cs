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

        public void Remove(T value)
        {
            var (found, deleteParent, deleteNode) = FindNode(value);
            if (found == false)
            {
                return;
            }

            if (deleteNode.Right == null)
            {
                // SS: node to delete has no right child, so make parent point to
                // left child of node to delete
                if (deleteParent.Left == deleteNode)
                {
                    deleteParent.Left = deleteNode.Left;
                }
                if (deleteParent.Right == deleteNode)
                {
                    deleteParent.Right = deleteNode.Left;
                }
            }
            else if (deleteNode.Left == null)
            {
                // SS: node to delete has no left child, so make parent point to
                // right child of node to delete
                if (deleteParent.Left == deleteNode)
                {
                    deleteParent.Left = deleteNode.Right;
                }
                if (deleteParent.Right == deleteNode)
                {
                    deleteParent.Right = deleteNode.Right;
                }
            }
            else
            {
                int nodeToReplace = deleteParent.Left == deleteNode ? 0 : 1;
                
                // SS: node to delete has two children.
                // Find the smallest node on the right subtree.
                var (p, c) = FindSmallestNode(deleteNode, deleteNode.Right);
                
                // SS: We know c cannot have a left child, but a right one
                if (p.Left == c)
                {
                    // SS: disconnect c from parent, and connect c's right child to
                    // c's parent instead
                    p.Left = c.Right;
                }
                else
                {
                    // SS: c must be the right child of the node to delete
                    if (p.Right != c)
                    {
                        throw new InvalidOperationException("unexpected");
                    }
                }

                // SS: make c the new child of deleteParent
                c.Left = deleteNode.Left;

                if (deleteNode.Right != c)
                {
                    c.Right = deleteNode.Right;
                }

                if (nodeToReplace == 0)
                {
                    deleteParent.Left = c;
                }
                else
                {
                    deleteParent.Right = c;
                }
            }
        }

        public (Node<T> parent, Node<T> child) FindSmallestNode(Node<T> parent, Node<T> node)
        {
            Node<T> minNode = node;
            Node<T> minNodeParent = parent;
            while (node != null)
            {
                if (node.Left != null)
                {
                    minNode = node.Left;
                    minNodeParent = node;
                    node = node.Left;
                }
                else
                {
                    // SS: node does not have a left child, so this is the
                    // minimum value node
                    break;
                }
            }
            return (minNodeParent, minNode);
        }

        public (bool found, Node<T> parent, Node<T> child) FindNode(T value)
        {
            var parent = Root;
            var child = Root;
            while (child != null)
            {
                var compareTo = value.CompareTo(child.Value);
                if (compareTo == 0)
                {
                    return (true, parent, child);
                }
                else if (compareTo == -1)
                {
                    parent = child;
                    child = child.Left;
                }
                else if (compareTo == 1)
                {
                    parent = child;
                    child = child.Right;
                }
            }
            return (false, null, null);
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