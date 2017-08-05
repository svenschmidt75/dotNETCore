using System;
using System.Collections.Generic;
using System.Linq;

namespace Djikstra
{
    public static class Djikstra
    {
        public static IEnumerable<Node> Run(Graph graph)
        {
            /* For each node, the 'weights' hash map contains the node's
            * cost from the start node. As we move through the graph, we
            * update the cost if we find a cheaper path.
            */
            var weights = new Dictionary<Node, int>();

            /* Whereas the 'weights' hash map contains the cost, we also
             * need to keep track of the cheapest path as we traverse
             * the graph. This is done with the 'parent' hash map, for
             * each node, we store the node we came from.
            */
            var parents = new Dictionary<Node, Node>();

            // set the parent for the nodes that can be reached from the start node
            var edges = graph[graph.Start];
            edges.ForEach(edge => parents[edge.Node] = graph.Start);

            // initialize node weights
            graph.Nodes.ForEach(node => { weights[node.Key] = int.MaxValue; });
            var startNode = graph.Start;
            weights[startNode] = 0;

            var toProcess = new HashSet<Node>();
            graph.Nodes.ForEach(edge => toProcess.Add(edge.Key));
            while (toProcess.Any())
            {

                // TODO SS: here needs to be a check whether the node is the end node

                var currentNode = FindCheapestNode(toProcess, weights);
                toProcess.Remove(currentNode);
                Console.WriteLine($"At current node {currentNode.Name}");
                var toNodes = graph[currentNode];
                toNodes.ForEach(currentNodeEdge =>
                {
                    var n2 = currentNodeEdge.Node;
                    var weight = currentNodeEdge.Weight;
                    Console.WriteLine($"Edge ({currentNode.Name}, {n2.Name}) = {weight}");
                    var newWeight = weights[currentNode] + weight;
                    if (weights[n2] > newWeight)
                    {
                        Console.WriteLine($"Edge ({currentNode.Name}, {n2.Name}) = {newWeight}");
                        weights[n2] = newWeight;
                        parents[n2] = currentNode;
                    }
                });
            }
            var shortestPath = GetShortestPath(graph, parents);
            return shortestPath;
        }

        private static Node FindCheapestNode(HashSet<Node> toProcess, Dictionary<Node, int> weights)
        {
            var orderedByWeight = weights.OrderBy(w => w.Value).Where(w2 => toProcess.Contains(w2.Key));
            return orderedByWeight.First().Key;
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
    }
}