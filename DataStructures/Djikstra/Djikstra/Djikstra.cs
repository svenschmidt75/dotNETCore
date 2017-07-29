using System;
using System.Collections.Generic;
using System.Linq;

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

            var startNode = graph.Start;

            // initialize nodes with weights from start node
            graph.Nodes.ForEach(node =>
            {
                weights[node.Key] = int.MaxValue;
            });
            weights[startNode] = 0;

            var processed = new Queue<Node>();
            processed.Enqueue(startNode);
            while (processed.Any())
            {
                var currentNode = processed.Dequeue();
                Console.WriteLine($"At current node {currentNode.Name}");

                var toNodes = OrderNodesByWeight(graph, currentNode, weights);
                toNodes.ForEach(currentNodeEdge =>
                {
                    var toNode = currentNodeEdge.Node;
                    var weight = currentNodeEdge.Weight;
                    Console.WriteLine($"Edge ({currentNode.Name}, {toNode.Name}) = {weight}");

                    processed.Enqueue(toNode);
                    var newWeight = weights[currentNode] + weight;
                    if (weights[toNode] > newWeight)
                    {
                        Console.WriteLine($"Edge ({currentNode.Name}, {toNode.Name}) = {newWeight}");
                        weights[toNode] = newWeight;
                        parents[toNode] = currentNode;
                    }
                });
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