using System.Collections.Generic;
using Xunit;

namespace Djikstra.UnitTest
{
    public class DjikstraTest
    {
        [Fact]
        public void GrokkinAlgortihms_Page_117()
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

        [Fact]
        public void GrokkinAlgortihms_Page_120()
        {
            // Arrange
            var startNode = new Node("Twin Peaks");
            var aNode = new Node("A");
            var bNode = new Node("B");
            var cNode = new Node("C");
            var dNode = new Node("D");
            var endNode = new Node("Golden Gate");

            var graph = new Graph(startNode, endNode);
            var edges = graph.Add(startNode);
            edges.Add(new Edge {Node = aNode, Weight = 4});
            edges.Add(new Edge {Node = bNode, Weight = 10});
            edges = graph.Add(aNode);
            edges.Add(new Edge {Node = dNode, Weight = 21});
            edges = graph.Add(bNode);
            edges.Add(new Edge {Node = cNode, Weight = 5});
            edges = graph.Add(cNode);
            edges.Add(new Edge {Node = dNode, Weight = 5});
            edges = graph.Add(dNode);
            edges.Add(new Edge {Node = endNode, Weight = 4});
            graph.Add(endNode);

            // Act
            var shortestPath = Djikstra.Run(graph);

            // Assert
            Assert.Equal(new[] {startNode, bNode, cNode, dNode, endNode}, shortestPath);
        }

    }
}