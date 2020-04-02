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

        public static bool FindNodeInTree(Node root, int nodeIndex)
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
    }
}