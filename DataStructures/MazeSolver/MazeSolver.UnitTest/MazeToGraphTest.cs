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
        }
    }
}