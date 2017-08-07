using System;
using System.Collections.Generic;
using System.IO;
using Xunit;

namespace MazeSolver.UnitTest
{
    public class MaseSolverTest
    {
        [Fact]
        public void Test1()
        {
            // Arrange
//            Console.WriteLine($"{Directory.GetCurrentDirectory()}");
            var maze = ImageBasedMaze.Create("../../../../images/Computerphile.jpg");

            // Act
            var path = new List<Point>();
            bool foundSolution = MazeSolver.Follow(maze, new Point {X = 3, Y = 0}, new HashSet<Point>(), path);

            maze.SavePath(path, "../../../../images/Computerphile_solution.jpg");

            // Assert
            Assert.True(foundSolution);
        }
    }
}