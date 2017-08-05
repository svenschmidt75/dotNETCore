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

        [Fact]
        public void GrokkinAlgortihms_Page_125()
        {
            // Arrange
            var startNode = new Node("Book - Start");
            var aNode = new Node("LP");
            var bNode = new Node("Poster");
            var cNode = new Node("Guitar");
            var dNode = new Node("Drum");
            var endNode = new Node("Piano - End");

            var graph = new Graph(startNode, endNode);
            var edges = graph.Add(startNode);
            edges.Add(new Edge {Node = aNode, Weight = 5});
            edges.Add(new Edge {Node = bNode, Weight = 0});
            edges = graph.Add(aNode);
            edges.Add(new Edge {Node = cNode, Weight = 15});
            edges.Add(new Edge {Node = dNode, Weight = 20});
            edges = graph.Add(bNode);
            edges.Add(new Edge {Node = cNode, Weight = 30});
            edges.Add(new Edge {Node = dNode, Weight = 35});
            edges = graph.Add(cNode);
            edges.Add(new Edge {Node = endNode, Weight = 20});
            edges = graph.Add(dNode);
            edges.Add(new Edge {Node = endNode, Weight = 10});
            graph.Add(endNode);

            // Act
            var shortestPath = Djikstra.Run(graph);

            // Assert
            Assert.Equal(new[] {startNode, aNode, dNode, endNode}, shortestPath);
        }

        [Fact]
        public void GrokkinAlgortihms_Ex_7_1_A()
        {
            // Arrange
            var startNode = new Node("Start");
            var aNode = new Node("A");
            var bNode = new Node("B");
            var cNode = new Node("C");
            var dNode = new Node("D");
            var endNode = new Node("End");

            var graph = new Graph(startNode, endNode);
            var edges = graph.Add(startNode);
            edges.Add(new Edge {Node = aNode, Weight = 5});
            edges.Add(new Edge {Node = bNode, Weight = 2});
            edges = graph.Add(aNode);
            edges.Add(new Edge {Node = cNode, Weight = 4});
            edges.Add(new Edge {Node = dNode, Weight = 2});
            edges = graph.Add(bNode);
            edges.Add(new Edge {Node = aNode, Weight = 8});
            edges.Add(new Edge {Node = dNode, Weight = 7});
            edges = graph.Add(cNode);
            edges.Add(new Edge {Node = endNode, Weight = 3});
            edges.Add(new Edge {Node = dNode, Weight = 6});
            edges = graph.Add(dNode);
            edges.Add(new Edge {Node = endNode, Weight = 1});
            graph.Add(endNode);

            // Act
            var shortestPath = Djikstra.Run(graph);

            // Assert
            Assert.Equal(new[] {startNode, aNode, dNode, endNode}, shortestPath);
        }

        [Fact]
        public void GrokkinAlgortihms_Ex_7_1_B()
        {
            // Arrange
            var startNode = new Node("Start");
            var aNode = new Node("A");
            var bNode = new Node("B");
            var cNode = new Node("C");
            var endNode = new Node("End");

            var graph = new Graph(startNode, endNode);
            var edges = graph.Add(startNode);
            edges.Add(new Edge {Node = aNode, Weight = 10});
            edges = graph.Add(aNode);
            edges.Add(new Edge {Node = bNode, Weight = 20});
            edges = graph.Add(bNode);
            edges.Add(new Edge {Node = cNode, Weight = 1});
            edges.Add(new Edge {Node = endNode, Weight = 30});
            edges = graph.Add(cNode);
            edges.Add(new Edge {Node = aNode, Weight = 1});
            graph.Add(endNode);

            // Act
            var shortestPath = Djikstra.Run(graph);

            // Assert
            Assert.Equal(new[] {startNode, aNode, bNode, endNode}, shortestPath);
        }

        [Fact]
        public void Computerphile_Djikstra()
        {
            // Arrange
            var startNode = new Node("Start");
            var aNode = new Node("A");
            var bNode = new Node("B");
            var cNode = new Node("C");
            var dNode = new Node("D");
            var fNode = new Node("F");
            var gNode = new Node("G");
            var hNode = new Node("H");
            var iNode = new Node("I");
            var jNode = new Node("J");
            var kNode = new Node("K");
            var lNode = new Node("L");
            var endNode = new Node("End");

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
            var shortestPath = Djikstra.Run(graph);

            // Assert
            Assert.Equal(new[] {startNode, bNode, hNode, gNode, endNode}, shortestPath);
        }

        [Fact]
        public void Computerphile_Djikstra_NotOptimal()
        {
            // Arrange
            var startNode = new Node("Start");
            var aNode = new Node("A");
            var bNode = new Node("B");
            var cNode = new Node("C");
            var dNode = new Node("D");
            var eNode = new Node("E");
            var fNode = new Node("F");
            var endNode = new Node("End");

            var graph = new Graph(startNode, endNode);
            var edges = graph.Add(startNode);
            edges.Add(new Edge {Node = aNode, Weight = 1});
            edges = graph.Add(aNode);
            edges.Add(new Edge {Node = startNode, Weight = 1});
            edges.Add(new Edge {Node = bNode, Weight = 1});
            edges.Add(new Edge {Node = eNode, Weight = 2});
            edges = graph.Add(bNode);
            edges.Add(new Edge {Node = aNode, Weight = 1});
            edges.Add(new Edge {Node = cNode, Weight = 1});
            edges = graph.Add(cNode);
            edges.Add(new Edge {Node = dNode, Weight = 1});
            edges.Add(new Edge {Node = bNode, Weight = 1});
            edges = graph.Add(dNode);
            edges.Add(new Edge {Node = cNode, Weight = 1});
            edges = graph.Add(eNode);
            edges.Add(new Edge {Node = aNode, Weight = 2});
            edges.Add(new Edge {Node = fNode, Weight = 2});
            edges = graph.Add(fNode);
            edges.Add(new Edge {Node = endNode, Weight = 2});
            edges.Add(new Edge {Node = eNode, Weight = 2});
            graph.Add(endNode);

            // Act
            var shortestPath = Djikstra.Run(graph);

            // Assert
            Assert.Equal(new[] {startNode, aNode, eNode, fNode, endNode}, shortestPath);
        }
    }
}