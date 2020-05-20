using System;
using System.Collections.Generic;
using System.Linq;
using Djikstra;

namespace ColoringUSA
{
    public static class ColorCountry
    {
        /// <summary>
        /// Color countries using the minimum number of colors.
        /// Complexity: We have to check each node, and all edges,
        /// so O(|E| |V|)
        /// </summary>
        public static int Run(Graph graph, Node startNode)
        {
            var toProcess = new Queue<Node>();
            graph.Nodes.ForEach(n => toProcess.Enqueue(n.Key));
            var maxColor = 0;
            while (toProcess.Any())
            {
                var currentNode = toProcess.Dequeue();
                Console.WriteLine($"Coloring country: {currentNode.Name}");

                // initialize color array
                var colors = Enumerable.Range(0, graph.Nodes.Count).Select(_ => 0).ToList();

                // mask colors already in use
                var edges = graph[currentNode];
                edges.ForEach(edge =>
                {
                    var n2 = edge.Node;
                    if (n2.Color >= 0)
                    {
                        // color already in use by a neighbor
                        colors[n2.Color] = 1;
                        Console.WriteLine($"Country {n2.Name} has color {n2.Color}...");
                    }
                    else
                    {
                        Console.WriteLine($"Country {n2.Name} has no color...");
                    }
                });

                // first unused color
                int nodeColor =
                    colors.Zip(Enumerable.Range(0, graph.Nodes.Count), (c, i) => new {Color = c, Index = i}).
                           First(arg => arg.Color == 0).Index;

                Console.WriteLine($"Color for country {currentNode.Name}: {nodeColor}");

                currentNode.Color = nodeColor;
                maxColor = Math.Max(maxColor, nodeColor);
            }

            return maxColor + 1;
        }
    }
}