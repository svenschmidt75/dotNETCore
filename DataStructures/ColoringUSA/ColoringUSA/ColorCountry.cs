using System;
using System.Collections.Generic;
using System.Linq;
using Djikstra;

namespace ColoringUSA
{
    public static class ColorCountry
    {
        public static int Run(Graph graph, Node startNode)
        {
            // return number of colors
            // run a check after done


            var toProcess = new Queue<Node>();
            graph.Nodes.ForEach(n => toProcess.Enqueue(n.Key));
            int maxColor = 0;

            while (toProcess.Any())
            {
                var currentNode = toProcess.Dequeue();
                var colors = Enumerable.Range(0, graph.Nodes.Count).Select(_ => 0).ToList();

                // mask colors already in use
                var edges = graph[currentNode];
                edges.ForEach(edge =>
                {
                    var n2 = edge.Node;
                    colors[n2.Color] = 1;
                });

                // first unused color
                int nodeColor =
                    colors.Zip(Enumerable.Range(0, graph.Nodes.Count), (c, i) => new {Color = c, Index = i}).
                           First(arg => arg.Color == 0).Index;

                currentNode.Color = nodeColor;
            }

            return maxColor;
        }
    }
}