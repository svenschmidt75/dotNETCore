using System;
using System.Collections.Generic;
using System.IO;
using Xunit;

namespace MazeSolver.UnitTest
{
    public class MaseSolverTest
    {
        [Fact]
        public void Computerphile_Maze()
        {
            // Arrange
//            Console.WriteLine($"{Directory.GetCurrentDirectory()}");
            var maze = ImageBasedMaze.Create("../../../../images/Computerphile.jpg");

            // Act
            var path = new List<Point>();
            bool foundSolution = MazeSolver.SimpleSolver(maze, new Point {X = 3, Y = 0}, new HashSet<Point>(), path);

            maze.SavePath(path, "../../../../images/Computerphile_solution.jpg");

            // Assert
            Assert.True(foundSolution);
        }

        [Fact]
        public void Daedelus_31x31()
        {
            // Arrange
//            Console.WriteLine($"{Directory.GetCurrentDirectory()}");
            var maze = ImageBasedMaze.Create("../../../../images/31x31.jpg");

            // Act
            var path = new List<Point>();
            var start = MazeSolver.Entrance(maze);
            bool foundSolution = MazeSolver.SimpleSolver(maze, start, new HashSet<Point>(), path);

            maze.SavePath(path, "../../../../images/31x31_solution.jpg");

            // Assert
            Assert.True(foundSolution);
        }

        [Fact]
        public void SimpleMaze1()
        {
            // Arrange
            IMaze maze = SimpleMaze.CreateSimpleMaze1();

            // Act
            var path = new List<Point>();
            bool foundSolution = MazeSolver.SimpleSolver(maze, new Point {X = 3, Y = 0}, new HashSet<Point>(), path);

            maze.SavePath(path, "../../../../images/SimpleMaze1_solution.jpg");

            // Assert
            Assert.True(foundSolution);
        }

        [Fact]
        public void SimpleMaze2()
        {
            // Arrange
            IMaze maze = SimpleMaze.CreateSimpleMaze2();

            // Act
            var path = new List<Point>();
            bool foundSolution = MazeSolver.SimpleSolver(maze, new Point {X = 3, Y = 0}, new HashSet<Point>(), path);

            maze.SavePath(path, "../../../../images/SimpleMaze2_solution.jpg");

            // Assert
            Assert.True(foundSolution);
        }

        [Fact]
        public void SimpleMaze3()
        {
            // Arrange
            IMaze maze = SimpleMaze.CreateSimpleMaze3();

            // Act
            var path = new List<Point>();
            bool foundSolution = MazeSolver.SimpleSolver(maze, new Point {X = 3, Y = 0}, new HashSet<Point>(), path);

            // Assert
            Assert.False(foundSolution);
        }
    }
}