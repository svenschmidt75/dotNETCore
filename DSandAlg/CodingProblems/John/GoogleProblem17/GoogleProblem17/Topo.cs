#region

using System.Collections.Generic;
using System.Linq;

#endregion

namespace GoogleProblem17
{
    public static class Topo
    {
        public static int[] TopologicalSort(Graph g)
        {
            var verticesToVisit = new HashSet<int>();
            var q = new Queue<int>();
            foreach (var p in g.AdjacencyList)
            {
                verticesToVisit.Add(p.Key);
                q.Enqueue(p.Key);
            }

            var stack = new Stack<int>();

            while (q.Any())
            {
                var vertex = q.Dequeue();
                if (verticesToVisit.Contains(vertex) == false)
                {
                    continue;
                }

                Sort(g, vertex, verticesToVisit, stack);
            }

            var orderedVertices = new int[g.AdjacencyList.Count];
            var i = g.AdjacencyList.Count - 1;
            while (stack.Any())
            {
                var vertex = stack.Pop();
                orderedVertices[i] = vertex;
                i--;
            }

            return orderedVertices;
        }

        private static void Sort(Graph graph, int vertex, HashSet<int> verticesToVisit, Stack<int> stack)
        {
            verticesToVisit.Remove(vertex);

            var neighbors = graph.AdjacencyList[vertex];
            foreach (var neighbor in neighbors)
            {
                if (verticesToVisit.Contains(neighbor) == false)
                {
                    continue;
                }

                Sort(graph, neighbor, verticesToVisit, stack);
            }

            // SS: once all outgoing edges have been processes, add vertex to stack
            stack.Push(vertex);
        }
    }
}