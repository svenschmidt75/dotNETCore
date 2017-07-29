using System.Collections.Generic;
using Xunit;

namespace Djikstra.UnitTest
{
    public class DjikstraTest
    {
        [Fact]
        public void Test1()
        {
            // Arrange
            var startNode = new Node("Start");
            var aNode = new Node("A");
            var bNode = new Node("B");
            var endNode = new Node("Fin");

            var graph = new Graph(startNode, endNode);
            var edges = graph.Add(startNode);
            edges.Add(new Edge {Node = aNode, Weight = 6});
            edges.Add(new Edge {Node = bNode, Weight = 2});
            edges = graph.Add(aNode);
            edges.Add(new Edge {Node = endNode, Weight = 1});
            edges = graph.Add(bNode);
            edges.Add(new Edge {Node = aNode, Weight = 3});
            edges.Add(new Edge {Node = endNode, Weight = 5});
            graph.Add(endNode);

            // Act
            var shortestPath = Djikstra.Run(graph);

            // Assert
            Assert.Equal(new[] {startNode, bNode, aNode, endNode}, shortestPath);
        }
    }
}