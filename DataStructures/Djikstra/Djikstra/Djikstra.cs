using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Djikstra
{
    public static class Djikstra
    {
        public static IEnumerable<Node> Run(Graph graph)
        {
            var weights = new Dictionary<Node, int>();
            var parents = new Dictionary<Node, Node>();
            var processed = new HashSet<Node>();

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
            var edges = graph[startNode];
            edges.ForEach(edge => weights[edge.Node] = edge.Weight);

            graph.Nodes.ForEach(gp =>
            {
                var currentNode = gp.Key;
                // process all edges in ascending weights
                var nodesCount = graph.Nodes.Count;
//                while (processed.Count < nodesCount)
                {
                    weights.ToList().OrderBy(pair => pair.Value).ForEach(pair =>
                    {
                        var toNode = pair.Key;
                        var weight = pair.Value;
                        Console.WriteLine($"Edge ({currentNode.Name}, {toNode.Name}) = {weight}");

                        // update weights from toNode

                        // get all edges from toNode
                        var es = graph[toNode];
                        es.ForEach(edge =>
                        {
                            var newWeight = weight + edge.Weight;
                            if (weights[edge.Node] > newWeight)
                            {
                                Console.WriteLine($"Edge ({currentNode.Name}, {toNode.Name}, {edge.Node.Name}) = {newWeight}");
                                weights[edge.Node] = newWeight;
                                parents[edge.Node] = toNode;
                            }
                        });
                    });

                    // all weights are updated...
                    processed.Add(currentNode);
                }
            });

            return Enumerable.Empty<Node>();
        }
    }
}