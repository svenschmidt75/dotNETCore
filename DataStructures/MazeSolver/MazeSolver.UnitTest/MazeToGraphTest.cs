using Xunit;

namespace MazeSolver.UnitTest
{
    public class MazeToGraphTest
    {
        [Fact]
        public void Computerphile_Maze()
        {
            // Arrange
//            Console.WriteLine($"{Directory.GetCurrentDirectory()}");
            var maze = ImageBasedMaze.Create("../../../../images/Computerphile.jpg");

            // Act
            var graph = MazeSolverUtility.CreateGraph(maze);

            // Assert
            Assert.Equal(23, graph.Nodes.Count);
        }

        [Fact]
        public void Daedelus_63x63_Maze()
        {
            // Arrange
//            Console.WriteLine($"{Directory.GetCurrentDirectory()}");
            var maze = ImageBasedMaze.Create("../../../../images/31x31.jpg");

            // Act
            var graph = MazeSolverUtility.CreateGraph(maze);

            // Assert
            Assert.Equal(660, graph.Nodes.Count);
        }

        [Fact]
        public void Daedelus_128x128_Maze()
        {
            // Arrange
//            Console.WriteLine($"{Directory.GetCurrentDirectory()}");
            var maze = ImageBasedMaze.Create("../../../../images/128x128.jpg");

            // Act
            var graph = MazeSolverUtility.CreateGraph(maze);

            // Assert
            Assert.Equal(11655, graph.Nodes.Count);
        }
    }
}