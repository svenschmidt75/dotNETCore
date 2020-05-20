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

        public (bool found, Node<T> parent, Node<T> child) FindNode(T value)
        {
            var parent = Root;
            var child = Root;
            while (child != null)
            {
                var compareTo = value.CompareTo(child.Value);
                if (compareTo == 0) return (true, parent, child);

                if (compareTo == -1)
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
                if (item.CompareTo(currentNode.Value) == -1)
                    currentNode = currentNode.Left;
                else if (item.CompareTo(currentNode.Value) == 1)
                    currentNode = currentNode.Right;
                else
                    return true;

            return false;
        }

        public Node<T> FindCommonParent(Node<T> child1, Node<T> child2)
        {
            // Traverse tree, build parent-child relationship
            // For child1, child2, list all parents in two separate lists.
            // Find first repeating element, that is the common parent at the lowest level.
            // Alternatively, could convert to a graph and do BFS.

            var parentChild = new Dictionary<Node<T>, Node<T>>();

            // SS: root doesn't have any children
            parentChild[Root] = null;

            // SS: build parent/child relationship, O(N)
            Traverse(Root, parentChild);

            // SS: list all of child1's parents, O(N)
            var child1Parents = new List<Node<T>> {child1};
            var parent = parentChild[child1];
            while (parent != null)
            {
                child1Parents.Add(parent);
                parent = parentChild[parent];
            }

            // SS: list all of child2's parents, O(N)
            var child2Parents = new List<Node<T>> {child2};
            parent = parentChild[child2];
            while (parent != null)
            {
                child2Parents.Add(parent);
                parent = parentChild[parent];
            }

            // SS: find last common parent, O(N)
            var maxIdx = Math.Min(child1Parents.Count, child2Parents.Count);
            int idx;
            for (idx = 0; idx < maxIdx; idx++)
            {
                var parent1 = child1Parents[child1Parents.Count - 1 - idx];
                var parent2 = child2Parents[child2Parents.Count - 1 - idx];
                if (parent1 != parent2) break;
            }

            // SS: lowest common parent
            return child1Parents[child1Parents.Count - 1 - idx + 1];
        }

        private void Traverse(Node<T> parent, IDictionary<Node<T>, Node<T>> parentChild)
        {
            if (parent.Left != null)
            {
                parentChild[parent.Left] = parent;
                Traverse(parent.Left, parentChild);
            }

            if (parent.Right != null)
            {
                parentChild[parent.Right] = parent;
                Traverse(parent.Right, parentChild);
            }
        }
    }
}