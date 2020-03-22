using System;
using System.Collections.Generic;
using System.Linq;
using BinaryTree;

namespace BinaryTreeRemoveSingleNodeChildren
{
    public class BinaryTree<T> where T : IComparable
    {
        public Node<T> Root { get; set; }

        public void RemoveSingleNodeChildren()
        {
            var queue = new Queue<(Node<T>, Node<T>)>();
            if (Root.Left != null)
            {
                queue.Enqueue((Root, Root.Left));
            }
            if (Root.Right != null)
            {
                queue.Enqueue((Root, Root.Right));
            }

            while (queue.Any())
            {
                var (parentNode, childNode) = queue.Dequeue();

                Node<T> childChildNode = null;
                if (childNode.Left == null && childNode.Right != null)
                {
                    childChildNode = childNode.Right;
                }
                else if (childNode.Left != null && childNode.Right == null)
                {
                    childChildNode = childNode.Left;
                }

                if (childChildNode == null)
                {
                    // SS: either has no or two children, skip
                    if (childNode.Left != null)
                    {
                        queue.Enqueue((childNode, childNode.Left));
                    }
                    if (childNode.Right != null)
                    {
                        queue.Enqueue((childNode, childNode.Right));
                    }
                    continue;
                }

                // SS: childNode has only one child, remove childNode
                if (parentNode.Left == childNode)
                {
                    parentNode.Left = childChildNode;
                }
                else
                {
                    parentNode.Right = childChildNode;
                }
                
                queue.Enqueue((parentNode, childChildNode));
            }
        }
    }
}