using Djikstra;
using Xunit;

namespace BFS.UnitTest
{
    public class DFSTest
    {
        [Fact]
        public void FindShortestPath()
        {
            // Arrange
            var youNode = new Node("You");
            var aliceNode = new Node("Alice");
            var peggyNode = new Node("Peggy");
            var bobNode = new Node("Bob");
            var anujNode = new Node("Anuj");
            var claireNode = new Node("Claire");
            var thomNode = new Node("Thom");
            var jonnyNode = new Node("Jonny");
            var kyleNode = new Node("Kyle");
            var graph = new Graph();

            var edges = graph.Add(youNode);
            edges.Add(new Edge {Node = bobNode});
            edges.Add(new Edge {Node = aliceNode});
            edges.Add(new Edge {Node = claireNode});
            edges = graph.Add(aliceNode);
            edges.Add(new Edge {Node = peggyNode});
            graph.Add(peggyNode);
            edges = graph.Add(bobNode);
            edges.Add(new Edge {Node = anujNode});
            edges.Add(new Edge {Node = peggyNode});
            graph.Add(anujNode);
            edges = graph.Add(claireNode);
            edges.Add(new Edge {Node = jonnyNode});
            edges.Add(new Edge {Node = thomNode});
            edges = graph.Add(jonnyNode);
            edges.Add(new Edge {Node = kyleNode});
            graph.Add(thomNode);

            // Act
            var path = DFS.ShortestPath(graph, youNode, kyleNode);

            // Assert
            Assert.Equal(new[] {youNode, claireNode, jonnyNode, kyleNode}, path);
        }

        [Fact]
        public void FindShortestPath_CyclicGraph()
        {
            // Arrange
            var youNode = new Node("You");
            var aliceNode = new Node("Alice");
            var peggyNode = new Node("Peggy");
            var bobNode = new Node("Bob");
            var anujNode = new Node("Anuj");
            var claireNode = new Node("Claire");
            var thomNode = new Node("Thom");
            var jonnyNode = new Node("Jonny");
            var kyleNode = new Node("Kyle");
            var graph = new Graph();

            var edges = graph.Add(youNode);
            edges.Add(new Edge {Node = bobNode});
            edges.Add(new Edge {Node = aliceNode});
            edges.Add(new Edge {Node = claireNode});
            edges = graph.Add(aliceNode);
            edges.Add(new Edge {Node = peggyNode});
            edges = graph.Add(peggyNode);
            // cyclic
            edges.Add(new Edge {Node = youNode});
            edges = graph.Add(bobNode);
            edges.Add(new Edge {Node = anujNode});
            edges.Add(new Edge {Node = peggyNode});
            graph.Add(anujNode);
            edges = graph.Add(claireNode);
            edges.Add(new Edge {Node = jonnyNode});
            edges.Add(new Edge {Node = thomNode});
            edges = graph.Add(jonnyNode);
            edges.Add(new Edge {Node = kyleNode});
            graph.Add(thomNode);

            // Act
            var path = DFS.ShortestPath(graph, youNode, kyleNode);

            // Assert
            Assert.Equal(new[] {youNode, claireNode, jonnyNode, kyleNode}, path);
        }
    }
}
