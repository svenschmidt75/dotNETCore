using System;
using Djikstra;
using Xunit;

namespace ColoringUSA.UnitTest
{
    public class ColorCountryTest
    {
        [Fact]
        public void Test1()
        {
            // Arrange
            var graph = new Graph();
            var aNode = new Node("A");
            var bNode = new Node("B");
            var cNode = new Node("C");
            var dNode = new Node("D");
            var eNode = new Node("E");
            var fNode = new Node("F");
            var gNode = new Node("G");
            var hNode = new Node("H");
            var edges = graph.Add(aNode);
            edges.Add(new Edge {Node = bNode});
            edges.Add(new Edge {Node = dNode});
            edges.Add(new Edge {Node = eNode});
            edges.Add(new Edge {Node = fNode});
            edges = graph.Add(bNode);
            edges.Add(new Edge {Node = aNode});
            edges.Add(new Edge {Node = dNode});
            edges.Add(new Edge {Node = cNode});
            edges.Add(new Edge {Node = hNode});
            edges.Add(new Edge {Node = gNode});
            edges.Add(new Edge {Node = fNode});
            edges = graph.Add(cNode);
            edges.Add(new Edge {Node = bNode});
            edges.Add(new Edge {Node = dNode});
            edges.Add(new Edge {Node = hNode});
            edges = graph.Add(dNode);
            edges.Add(new Edge {Node = aNode});
            edges.Add(new Edge {Node = bNode});
            edges.Add(new Edge {Node = cNode});
            edges.Add(new Edge {Node = eNode});
            edges = graph.Add(eNode);
            edges.Add(new Edge {Node = aNode});
            edges.Add(new Edge {Node = dNode});
            edges = graph.Add(fNode);
            edges.Add(new Edge {Node = aNode});
            edges.Add(new Edge {Node = bNode});
            edges.Add(new Edge {Node = gNode});
            edges = graph.Add(gNode);
            edges.Add(new Edge {Node = fNode});
            edges.Add(new Edge {Node = bNode});
            edges.Add(new Edge {Node = hNode});
            edges = graph.Add(hNode);
            edges.Add(new Edge {Node = gNode});
            edges.Add(new Edge {Node = bNode});
            edges.Add(new Edge {Node = cNode});

            // Act
            var nColors = ColorCountry.Run(graph, aNode);

            // Assert
            Assert.Equal(3, nColors);
        }
    }
}