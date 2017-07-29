using System;
using System.Collections.Generic;
using System.Linq;
using Djikstra;

namespace BFS
{
    public static class BFS
    {
        /// <summary>
        /// Search the graph from the startNode for a node that
        /// satisfies the predicate.
        /// Note that we cannot give the shortest path with BFS.
        /// To get that, we need to use DFS which uses back tracking...
        /// </summary>
        public static (bool, Node) ShortestPath(Graph graph, Node startNode, Func<Node, bool> predicate)
        {
            var processed = new HashSet<Node>();
            var toProcess = new Queue<Node>();
            toProcess.Enqueue(startNode);
            while (toProcess.Any())
            {
                var currentNode = toProcess.Dequeue();
                if (processed.Contains(currentNode))
                {
                    continue;
                }
                processed.Add(currentNode);
                if (predicate(currentNode))
                {
                    return (true, currentNode);
                }
                var edges = graph[currentNode];
                edges.ForEach(edge => toProcess.Enqueue(edge.Node));
            }
            return (false, null);
        }
    }
}