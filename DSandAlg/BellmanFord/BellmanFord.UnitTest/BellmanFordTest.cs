using Djikstra;
using Xunit;

namespace BellmanFord.UnitTest
{
    public class BellmanFordTest
    {
        [Fact]
        public void GrokkinAlgortihms_Ex_7_1_C()
        {
            // Arrange
            var startNode = new Node("Start");
            var aNode = new Node("A");
            var bNode = new Node("B");
            var cNode = new Node("C");
            var endNode = new Node("End");

            var graph = new Graph(startNode, endNode);
            var edges = graph.Add(startNode);
            edges.Add(new Edge {Node = aNode, Weight = 2});
            edges.Add(new Edge {Node = bNode, Weight = 2});
            edges = graph.Add(aNode);
            edges.Add(new Edge {Node = cNode, Weight = 2});
            edges.Add(new Edge {Node = endNode, Weight = 2});
            edges = graph.Add(bNode);
            edges.Add(new Edge {Node = aNode, Weight = 2});
            edges = graph.Add(cNode);
            edges.Add(new Edge {Node = bNode, Weight = -1});
            edges.Add(new Edge {Node = endNode, Weight = 2});
            graph.Add(endNode);

            // Act
            var shortestPath = BellmanFord.Run(graph);

            // Assert
            Assert.Equal(new[] {startNode, aNode, endNode}, shortestPath);
        }
    }
}