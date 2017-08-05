using System;
using Djikstra;
using Xunit;

namespace Astar.UnitTest
{
    public class AstarTest
    {

        [Fact]
        public void Computerphile_Djikstra()
        {
            // Arrange
            var startNode = new Node("Start", 10);
            var aNode = new Node("A", 9);
            var bNode = new Node("B", 7);
            var cNode = new Node("C", 8);
            var dNode = new Node("D", 8);
            var fNode = new Node("F", 6);
            var gNode = new Node("G", 3);
            var hNode = new Node("H", 6);
            var iNode = new Node("I", 4);
            var jNode = new Node("J", 4);
            var kNode = new Node("K", 3);
            var lNode = new Node("L", 6);
            var endNode = new Node("End", 0);

            var graph = new Graph(startNode, endNode);
            var edges = graph.Add(startNode);
            edges.Add(new Edge {Node = aNode, Weight = 7});
            edges.Add(new Edge {Node = bNode, Weight = 2});
            edges.Add(new Edge {Node = cNode, Weight = 3});
            edges = graph.Add(aNode);
            edges.Add(new Edge {Node = startNode, Weight = 7});
            edges.Add(new Edge {Node = bNode, Weight = 3});
            edges.Add(new Edge {Node = dNode, Weight = 4});
            edges = graph.Add(bNode);
            edges.Add(new Edge {Node = startNode, Weight = 2});
            edges.Add(new Edge {Node = aNode, Weight = 3});
            edges.Add(new Edge {Node = dNode, Weight = 4});
            edges.Add(new Edge {Node = hNode, Weight = 1});
            edges = graph.Add(cNode);
            edges.Add(new Edge {Node = startNode, Weight = 3});
            edges.Add(new Edge {Node = lNode, Weight = 2});
            edges = graph.Add(dNode);
            edges.Add(new Edge {Node = aNode, Weight = 4});
            edges.Add(new Edge {Node = bNode, Weight = 4});
            edges.Add(new Edge {Node = fNode, Weight = 5});
            edges = graph.Add(fNode);
            edges.Add(new Edge {Node = dNode, Weight = 5});
            edges.Add(new Edge {Node = hNode, Weight = 3});
            edges = graph.Add(hNode);
            edges.Add(new Edge {Node = bNode, Weight = 1});
            edges.Add(new Edge {Node = fNode, Weight = 3});
            edges.Add(new Edge {Node = gNode, Weight = 2});
            edges = graph.Add(lNode);
            edges.Add(new Edge {Node = cNode, Weight = 2});
            edges.Add(new Edge {Node = iNode, Weight = 4});
            edges.Add(new Edge {Node = jNode, Weight = 4});
            edges = graph.Add(iNode);
            edges.Add(new Edge {Node = lNode, Weight = 4});
            edges.Add(new Edge {Node = kNode, Weight = 4});
            edges = graph.Add(jNode);
            edges.Add(new Edge {Node = lNode, Weight = 4});
            edges.Add(new Edge {Node = kNode, Weight = 4});
            edges = graph.Add(kNode);
            edges.Add(new Edge {Node = iNode, Weight = 4});
            edges.Add(new Edge {Node = jNode, Weight = 4});
            edges.Add(new Edge {Node = endNode, Weight = 5});
            edges = graph.Add(gNode);
            edges.Add(new Edge {Node = hNode, Weight = 2});
            edges.Add(new Edge {Node = endNode, Weight = 2});
            graph.Add(endNode);

            // Act
            var shortestPath = Astar.Run(graph);

            // Assert
            Assert.Equal(new[] {startNode, bNode, hNode, gNode, endNode}, shortestPath);
        }
    }
}