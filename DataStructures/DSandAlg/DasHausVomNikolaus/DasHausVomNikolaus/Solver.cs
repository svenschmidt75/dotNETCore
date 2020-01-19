using System;
using System.Collections.Generic;
using System.Linq;

namespace DasHausVomNikolaus
{
    public class Solver
    {
        private readonly IDictionary<(int, int), int> _edges = new Dictionary<(int, int), int>
        {
            {(2, 4), 0} // edge 1 connects nodes 2 and 4
            ,
            {(3, 4), 1} // edge 2 connects nodes 3 and 4
            ,
            {(2, 3), 2} // edge 3 connects nodes 2 and 3
            ,
            {(0, 2), 3} // edge 4 connects nodes 1 and 3
            ,
            {(1, 3), 4} // edge 5 connects nodes 2 and 4
            ,
            {(0, 1), 5} // edge 6 connects nodes 1 and 2
            ,
            {(1, 2), 6} // edge 7 connects nodes 2 and 3
            ,
            {(0, 3), 7} // edge 8 connects nodes 1 and 4
        };

        private readonly IDictionary<int, int[]> _graph = new Dictionary<int, int[]>
        {
            {0, new[] {1, 2, 3}} // node 1
            ,
            {1, new[] {0, 2, 3}} // node 2
            ,
            {2, new[] {0, 1, 3, 4}} // node 3
            ,
            {3, new[] {0, 1, 2, 4}} // node 4
            ,
            {4, new[] {2, 3}} // node 5
        };

        public List<List<int>> Solutions { get; } = new List<List<int>>();

        private int GetEdgeIndex(int node1Index, int node2Index)
        {
            var n1 = Math.Min(node1Index, node2Index);
            var n2 = Math.Max(node1Index, node2Index);
            return _edges[(n1, n2)];
        }

        public void RunRecursive()
        {
            // SS: loop over all indices to find all solutions
            for (var i = 0; i < 5; i++)
            {
                var edgeVisited = new HashSet<int>();
                var path = new List<int> {i};
                FindSolutionsRecursive(i, edgeVisited, path);
            }
        }

        private void FindSolutionsRecursive(int startIndex, HashSet<int> edgeVisited, List<int> path)
        {
            var index = startIndex;
            var connectedNodes = _graph[index];
            
            // SS: this is using DFS
            
            foreach (var targetNode in connectedNodes)
            {
                // SS: have we visited this edge already?
                var edgeIndex = GetEdgeIndex(index, targetNode);
                if (edgeVisited.Contains(edgeIndex))
                    // SS: skip this node, edge already visited
                    continue;

                var newEdgeVisited = new HashSet<int>(edgeVisited);

                // SS: mark node as visited
                newEdgeVisited.Add(edgeIndex);

                // SS: remember path taken
                var newPath = new List<int>(path) {targetNode};

                FindSolutionsRecursive(targetNode, newEdgeVisited, newPath);
            }

            // SS: check for solution
            if (path.Count == 9 && edgeVisited.Count == 8) Solutions.Add(path);
        }

        public void FindSolutionsNonRecursive()
        {
            // SS: in a non-recursive way, we have to "remember" various properties
            // that we store in stacks. Note that in a recursive method, we typically
            // use the call stack to remember local properties.
            var edgeVisited = new Stack<int>();
            var path = new Stack<int>();
            var currentNodeIndex = new Stack<int>();
            for (var startIndex = 0; startIndex < 5; startIndex++)
            {
                var index = startIndex;
                path.Push(index);
                var nodeIndex = 0;
                do
                {
                    var connectedNodes = _graph[index];
                    for (; nodeIndex < connectedNodes.Length; nodeIndex++)
                    {
                        var targetNode = connectedNodes[nodeIndex];
                        // SS: have we visited this edge already?
                        var edgeIndex = GetEdgeIndex(index, targetNode);
                        if (edgeVisited.Contains(edgeIndex))
                            // SS: skip this node, edge already visited
                            continue;

                        // SS: remember visited edges
                        edgeVisited.Push(edgeIndex);

                        // SS: remember path taken
                        path.Push(targetNode);

                        // SS: remember nodeIndex
                        currentNodeIndex.Push(nodeIndex);

                        index = targetNode;
                        connectedNodes = _graph[index];
                        nodeIndex = -1;
                    }

                    // SS: check for solution
                    if (path.Count == 9 && edgeVisited.Count == 8)
                    {
                        var list = path.ToArray().Reverse().ToList();
                        Console.WriteLine($"Solution: {string.Join(",", list)}");
                        Solutions.Add(list);
                    }

                    // SS: done with this node
                    path.Pop();
                    if (path.TryPeek(out index) == false)
                        // SS: done
                        break;
                    edgeVisited.Pop();
                    nodeIndex = currentNodeIndex.Pop() + 1;
                } while (true);
            }
        }
    }
}