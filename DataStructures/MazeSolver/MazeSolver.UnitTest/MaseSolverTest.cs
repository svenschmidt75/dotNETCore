using System;
using System.Collections.Generic;
using System.IO;
using Xunit;

namespace MazeSolver.UnitTest
{
    public class MazeSolverTest
    {
        [Fact]
        public void Computerphile_Maze()
        {
            // Arrange
//            Console.WriteLine($"{Directory.GetCurrentDirectory()}");
            var maze = ImageBasedMaze.Create("../../../../images/Computerphile.jpg");

            // Act
            var (foundSolution, solutionPath) = MazeSolver.SimpleSolver(maze);
            maze.SavePath(solutionPath, "../../../../images/Computerphile_solution.jpg");

            // Assert
            Assert.True(foundSolution);
        }

        [Fact]
        public void Computerphile_Maze_Entrance()
        {
            // Arrange
            var maze = ImageBasedMaze.Create("../../../../images/Computerphile.jpg");

            // Act
            var entrance = MazeSolver.Entrance(maze);

            // Assert
            Assert.Equal(new Point {X = 3, Y = 0}, entrance);
        }

        [Fact]
        public void Computerphile_Maze_Exit()
        {
            // Arrange
            var maze = ImageBasedMaze.Create("../../../../images/Computerphile.jpg");

            // Act
            var entrance = MazeSolver.Exit(maze);

            // Assert
            Assert.Equal(new Point {X = 7, Y = maze.Height - 1}, entrance);
        }

        [Fact]
        public void Daedelus_128x128_Entrance()
        {
            // Arrange
            var maze = ImageBasedMaze.Create("../../../../images/128x128.jpg");

            // Act
            var (foundSolution, solutionPath) = MazeSolver.SimpleSolver(maze);
            maze.SavePath(solutionPath, "../../../../images/128x128_solution.jpg");

            // Assert
            Assert.True(foundSolution);
        }

        [Fact]
        public void Daedelus_63x63()
        {
            // Arrange
            var maze = ImageBasedMaze.Create("../../../../images/31x31.jpg");

            // Act
            var (foundSolution, solutionPath) = MazeSolver.SimpleSolver(maze);
            maze.SavePath(solutionPath, "../../../../images/31x31_solution.jpg");

            // Assert
            Assert.True(foundSolution);
        }

        [Fact]
        public void Daedelus_63x63_Entrance()
        {
            // Arrange
            var maze = ImageBasedMaze.Create("../../../../images/31x31.jpg");

            // Act
            var entrance = MazeSolver.Entrance(maze);

            // Assert
            Assert.Equal(new Point {X = 29, Y = 0}, entrance);
        }

        [Fact]
        public void Daedelus_63x63_Exit()
        {
            // Arrange
            var maze = ImageBasedMaze.Create("../../../../images/31x31.jpg");

            // Act
            var entrance = MazeSolver.Exit(maze);

            // Assert
            Assert.Equal(new Point {X = 37, Y = maze.Height - 1}, entrance);
        }

        [Fact]
        public void SimpleMaze1()
        {
            // Arrange
            IMaze maze = SimpleMaze.CreateSimpleMaze1();

            // Act
            var (foundSolution, solutionPath) = MazeSolver.SimpleSolver(maze);
            maze.SavePath(solutionPath, "../../../../images/SimpleMaze1_solution.jpg");

            // Assert
            Assert.True(foundSolution);
        }

        [Fact]
        public void SimpleMaze2()
        {
            // Arrange
            IMaze maze = SimpleMaze.CreateSimpleMaze2();

            // Act
            var (foundSolution, solutionPath) = MazeSolver.SimpleSolver(maze);
            maze.SavePath(solutionPath, "../../../../images/SimpleMaze2_solution.jpg");

            // Assert
            Assert.True(foundSolution);
        }

        [Fact]
        public void SimpleMaze3()
        {
            // Arrange
            IMaze maze = SimpleMaze.CreateSimpleMaze3();

            // Act
            var (foundSolution, _) = MazeSolver.SimpleSolver(maze);

            // Assert
            Assert.False(foundSolution);
        }
    }
}