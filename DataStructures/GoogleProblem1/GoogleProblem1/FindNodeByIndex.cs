using System;
using System.Collections.Generic;
using System.Linq;

namespace GoogleProblem1
{
    public static class FindNodeByIndex
    {
        public static List<int> CalculateIndices(int nodeIndex)
        {
            var indices = new List<int>();
            var index = nodeIndex;
            do
            {
                indices.Add(index);
                index /= 2;
            } while (index > 0);

            indices.Reverse();
            return indices;
        }

        public static bool FindNodeInTree1(Node root, int nodeIndex)
        {
            if (root == null) return false;
            if (nodeIndex < 1) return false;

            var indexPath = CalculateIndices(nodeIndex);

            var queue = new Queue<(Node, int)>();
            queue.Enqueue((root, 1));

            var positionIndexPath = 0;

            while (queue.Any())
            {
                var (node, index) = queue.Dequeue();
                if (index == nodeIndex) return true;
                positionIndexPath++;
                var childIndex = indexPath[positionIndexPath];
                if (index * 2 == childIndex)
                {
                    // left child
                    if (node.Left == null) return false;
                    queue.Enqueue((node.Left, 2 * index));
                }
                else if (index * 2 + 1 == childIndex)
                {
                    // right child
                    if (node.Right == null) return false;
                    queue.Enqueue((node.Right, 2 * index + 1));
                }
            }

            return false;
        }

        public static bool FindNodeInTree2(Node root, int nodeIndex)
        {
            if (root == null) return false;
            if (nodeIndex < 1) return false;

            var indexPath = CalculateIndices(nodeIndex);
            var positionIndexPath = 1;

            var parent = root;
            var idx = 1;

            while (parent != null && idx != nodeIndex)
            {
                if (indexPath[positionIndexPath] == 2 * idx)
                {
                    parent = parent.Left;
                    idx *= 2;
                }
                else if (indexPath[positionIndexPath] == 2 * idx + 1)
                {
                    parent = parent.Right;
                    idx = 2 * idx + 1;
                }

                positionIndexPath++;
            }

            return parent != null;
        }

        public static int FindHeight(Node node)
        {
            if (node == null)
            {
                return 0;
            }

            return 1 + FindHeight(node.Left);
        }

        public static int BinarySearch(Node root, int minIndex, int maxIndex)
        {
            // SS: Binary search at O(log n), each step is O(log n), so total is O(log^2 n).
            // Note: range is [min, max), i.e. min is inclusive, max exclusive
            int lastIndex = -1;
            int min = minIndex;
            int max = maxIndex;
            int mid;

            do
            {
                mid = (min + max) / 2;
                if (FindNodeInTree1(root, mid))
                {
                    // SS: node was found
                    min = mid + 1;
                    lastIndex = mid;
                }
                else
                {
                    max = mid;
                }
            } while (min < max);

            return lastIndex;
        }
        
        public static int NumberOfNodes(Node root)
        {
            /* Find number of nodes in a complete binary tree...
             * Approach 1: Do tree traversal at O(n) cost
             * Approach 2: Traverse the left subtrees to find the height of the tree at O(log n).
             *             Since the tree is complete, this is the only level that might
             *             not be complete. We need to find the right-most node on this
             *             level. The first node on that level has index 2^{height - 1},
             *             and the last one has index 2^{height}-1. For all those nodes,
             *             we check if their index exists, from left to right. This will
             *             take runtime: at most half of the nodes are at the deepest level,
             *             and for each one, we have to run FindNodeInTree at O(log n), hence
             *             total runtime is O(n log n).
             * Approach 3: Traverse the left subtrees to find the height of the tree at O(log n).
             *             Indices for nodes at the last level are: [2^{height - 1}, 2^{height} - 1].
             *             For the indices, do binary search at O(log n). Total runtime: O(log^2 n)!!!
             */
            if (root == null)
            {
                return 0;
            }
            
            // SS: find height of tree at O(log n)
            int height = FindHeight(root);
            int minNodeIndex = (int)Math.Pow(2, height - 1);
            int maxNodeIndex = 2 * minNodeIndex;
            var nNodes = BinarySearch(root, minNodeIndex, maxNodeIndex);
            return nNodes;
        }
        
    }
}