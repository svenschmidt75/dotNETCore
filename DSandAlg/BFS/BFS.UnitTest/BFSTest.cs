using System;
using Djikstra;
using Xunit;

namespace BFS.UnitTest
{
    public class BFSTest
    {
        [Fact]
        public void PersonIsMangoSeller()
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
            graph.Add(anujNode);
            edges = graph.Add(claireNode);
            edges.Add(new Edge {Node = jonnyNode});
            edges.Add(new Edge {Node = thomNode});
            graph.Add(jonnyNode);
            graph.Add(thomNode);

            // Act
            var (found, foundNode) = BFS.ShortestPath(graph, youNode, node => node.Name.EndsWith('m'));

            // Assert
            Assert.True(found);
            Assert.Equal(thomNode.Name, foundNode.Name);
        }
    }
}