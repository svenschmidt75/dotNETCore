using System;
using System.Collections.Generic;
using System.Linq;
using Djikstra;

namespace BellmanFord
{
    public static class BellmanFord
    {
        /// <summary>
        ///     Adapted from wikipedia, https://en.wikipedia.org/wiki/Bellman%E2%80%93Ford_algorithm
        /// </summary>
        /// <param name="graph"></param>
        /// <returns></returns>
        public static IEnumerable<Node> Run(Graph graph)
        {
            var parents = new Dictionary<Node, Node>();

            // set the parent for the nodes that can be reached from the start node
            var es = graph[graph.Start];
            es.ForEach(edge => parents[edge.Node] = graph.Start);

            // initialize node weights
            var weights = new Dictionary<Node, int>();
            graph.Nodes.ForEach(node => { weights[node.Key] = int.MaxValue; });
            var startNode = graph.Start;
            weights[startNode] = 0;

            graph.Nodes.ForEach(n =>
            {
                var currentNode = n.Key;
                Console.WriteLine($"At current node {currentNode.Name}");
                var distanceToCurrentNode = weights[currentNode];
                var edges = n.Value;
                edges.ForEach(edge =>
                {
                    var n2 = edge.Node;
                    var weight = edge.Weight;
                    var weightToN2 = weights[n2];
                    Console.WriteLine($"Edge ({currentNode.Name}, {n2.Name}) = {weight}");

                    // get distance to n2
                    var newWeight = distanceToCurrentNode + weight;
                    if (newWeight < weightToN2)
                    {
                        Console.WriteLine($"Edge ({currentNode.Name}, {n2.Name}) = {newWeight}");
                        weights[n2] = newWeight;
                        parents[n2] = currentNode;
                    }
                });
            });

            if (ContainsNegativeWeightCycles(graph, weights))
                return Enumerable.Empty<Node>();

            var shortestPath = GetShortestPath(graph, parents);
            return shortestPath;
        }

        private static IEnumerable<Node> GetShortestPath(Graph graph, IReadOnlyDictionary<Node, Node> parents)
        {
            var shortestPath = new List<Node>();
            var n = graph.End;
            while (n != graph.Start)
            {
                shortestPath.Add(n);
                n = parents[n];
            }
            shortestPath.Add(graph.Start);
            shortestPath.Reverse();
            return shortestPath;
        }

        private static bool ContainsNegativeWeightCycles(Graph graph, IReadOnlyDictionary<Node, int> weights)
        {
            var hasNegativeWeightCycles = false;
            graph.Nodes.ForEach(n =>
            {
                var currentNode = n.Key;
                Console.WriteLine($"At current node {currentNode.Name}");
                var distanceToCurrentNode = weights[currentNode];
                var edges = n.Value;
                edges.ForEach(edge =>
                {
                    var n2 = edge.Node;
                    var weight = edge.Weight;
                    var weightToN2 = weights[n2];
                    Console.WriteLine($"Edge ({currentNode.Name}, {n2.Name}) = {weight}");

                    // get weight between (current)
                    var weightToN2_1 = distanceToCurrentNode + weight;
                    if (weightToN2_1 < weightToN2)
                    {
                        // negative-weight cycle, u.e. following the cycle keeps decreasing
                        // the weight
                        Console.WriteLine("Graph contains a negative-weight cycle");
                        hasNegativeWeightCycles = true;
                    }
                });
            });
            return hasNegativeWeightCycles;
        }
    }
}