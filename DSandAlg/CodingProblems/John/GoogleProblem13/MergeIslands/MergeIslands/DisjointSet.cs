#region

using System.Collections.Generic;
using DisjointSet;

#endregion

namespace MergeIslands
{
    public class DisjointSet
    {
        private readonly List<Node> _nodes;

        public DisjointSet()
        {
            _nodes = new List<Node>();
        }

        public int NumberOfNodes { get; set; }

        public int MakeSet()
        {
            var node = new Node
            {
                NodeId = _nodes.Count
                , Height = 0
                , ParentNode = null
            };
            _nodes.Add(node);
            NumberOfNodes++;
            return node.NodeId;
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

        public int Merge(int idx1, int idx2)
        {
            var node1RootId = Find(idx1);
            var node2RootId = Find(idx2);

            if (node1RootId == node2RootId)
            {
                // SS: both nodes are in the same set already
                return node1RootId;
            }

            NumberOfNodes--;

            var root1 = _nodes[node1RootId];
            var root2 = _nodes[node2RootId];

            // SS: attach smaller subtree (lower rank i.e. height) to larger one
            if (root1.Height < root2.Height)
            {
                root1.ParentNode = root2;
                return node2RootId;
            }

            if (root1.Height > root2.Height)
            {
                root2.ParentNode = root1;
                return node1RootId;
            }

            // SS: both subtrees are the same rank, so increase rank of the
            // node we are adding the other to
            root2.ParentNode = root1;
            root1.Height += 1;
            return node1RootId;
        }
    }
}