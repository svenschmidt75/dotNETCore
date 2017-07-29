using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;

namespace Djikstra
{
    public static class Djikstra
    {
        public static IEnumerable<Node> Run(Graph graph)
        {
            /* For each node, the 'weights' hash map contains its cost
            * from the start node. As we move through the graph, we
            * update the cost if we find a cheaper path.
            */
            var weights = new Dictionary<Node, int>();

            /* Whereas the 'weights' hash map contains the cost, we also
             * need to keep track of the cheapest path as we traverse
             * the graph. This is done with the 'parent' has map.
            */
            var parents = new Dictionary<Node, Node>();
            var edges = graph[graph.Start];
            edges.ForEach(edge => parents[edge.Node] = graph.Start);

            // initialize nodes with weights from start node
            graph.Nodes.ForEach(node =>
            {
                if (node.Key == graph.Start)
                {
                    return;
                }
                weights[node.Key] = int.MaxValue;
            });
            var startNode = graph.Start;
            edges = graph[startNode];
            edges.ForEach(edge => weights[edge.Node] = edge.Weight);

            var processed = new HashSet<Node>();
            graph.Nodes.ForEach(gp => processed.Add(gp.Key));
            processed.Remove(graph.End);

            var currentNode = graph.Start;
            while (processed.Count > 0)
            {
                Console.WriteLine($"Ar current node ({currentNode.Name}");

                var toNodes = OrderNodesByWeight(graph, currentNode, weights);
                toNodes.ForEach(currentNodeEdge =>
                {
                    var toNode = currentNodeEdge.Node;
                    var weight = currentNodeEdge.Weight;
                    Console.WriteLine($"Edge ({currentNode.Name}, {toNode.Name}) = {weight}");

                    processed.Remove(toNode);

                    // update weights from toNode

                    // get all edges from toNode
                    var es = graph[toNode];
                    es.ForEach(edge =>
                    {
                        Console.WriteLine($"Checking ({toNode.Name}, {edge.Node.Name})");
                        var newWeight = weights[toNode] + edge.Weight;
                        if (weights[edge.Node] > newWeight)
                        {
                            Console.
                                WriteLine($"Edge ({currentNode.Name}, {toNode.Name}, {edge.Node.Name}) = {newWeight}");
                            weights[edge.Node] = newWeight;
                            parents[edge.Node] = toNode;
                        }
                    });
                });
                processed.Remove(currentNode);
            }

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

        private static IEnumerable<Edge> OrderNodesByWeight(Graph graph, Node node, Dictionary<Node, int> weights)
        {
            var edges = graph.Nodes[node];
            return edges.OrderBy(edge => edge.Weight);
        }
    }
}