#region

using System.Collections.Generic;

#endregion

namespace DisjointSet
{
    public class DisjointSet
    {
        private readonly List<Node> _nodes;

        private DisjointSet(int size)
        {
            _nodes = new List<Node>(size);
        }

        public static DisjointSet MakeSet(int size)
        {
            var disjointSet = new DisjointSet(size);
            for (var i = 0; i < size; i++)
            {
                var node = new Node
                {
                    NodeId = i
                    , Height = 0
                    , ParentNode = null
                };
                disjointSet._nodes.Add(node);
            }

            return disjointSet;
        }

        public int Find(int idx)
        {
            // SS: Note that we are not updating the height/rank after path compression,
            // because there is not efficient way to do so.

            // SS: return the root node id
            var node = _nodes[idx];

            // SS: find the root node (it is the one with ParentNode == null)
            var currentNode = node;
            while (currentNode.ParentNode != null)
            {
                currentNode = currentNode.ParentNode;
            }

            // SS: currentNode is root node
            var rootNode = currentNode;
            currentNode = node;

            // SS: path compression
            while (currentNode != rootNode)
            {
                var n = currentNode.ParentNode;
                currentNode.ParentNode = rootNode;
                currentNode = n;
            }

            return rootNode.NodeId;
        }

        public void Merge(int idx1, int idx2)
        {
            var node1RootId = Find(idx1);
            var node2RootId = Find(idx2);

            if (node1RootId == node2RootId)
            {
                // SS: both nodes are in the same set already
                return;
            }

            var root1 = _nodes[node1RootId];
            var root2 = _nodes[node2RootId];

            // SS: attach smaller subtree (lower rank i.e. height) to larger one
            if (root1.Height < root2.Height)
            {
                root1.ParentNode = root2;
            }
            else if (root1.Height > root2.Height)
            {
                root2.ParentNode = root1;
            }
            else
            {
                // SS: both subtrees are the same rank, so increase rank of the
                // node we are adding the other to
                root2.ParentNode = root1;
                root1.Height += 1;
            }
        }
    }
}