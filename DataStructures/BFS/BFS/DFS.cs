using System.Collections.Generic;
using System.Linq;
using Djikstra;

namespace BFS
{
    public static class DFS
    {
        /// <summary>
        /// DFS allows to find the shortest path while also
        /// keeping track of the path walked via a stack...
        /// A stack is useful in back tracking implementations
        /// as this is.
        /// </summary>
        public static IEnumerable<Node> ShortestPath(Graph graph, Node startNode, Node endNode)
        {
            var processed = new HashSet<Node>();
            var stack = new Stack<Node>();
            Do(graph, stack, startNode, endNode, processed);
            return Enumerable.Reverse(stack.ToList());
        }

        private static bool Do(Graph graph, Stack<Node> stack, Node currentNode, Node endNode, ISet<Node> processed)
        {
            // protect against cycles
            if (processed.Contains(currentNode))
            {
                return false;
            }
            // keep track of path walked
            stack.Push(currentNode);
            // base base for recursive schema
            if (currentNode == endNode)
            {
                return true;
            }
            processed.Add(currentNode);
            var edges = graph[currentNode];
            foreach (var edge in edges)
            {
                var found = Do(graph, stack, edge.Node, endNode, processed);
                if (found)
                {
                    return true;
                }
            }
            // back track
            stack.Pop();
            return false;
        }

    }
}