using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
            var (foundSolution, solutionPath) = SimpleMazeSolver.SimpleSolver(maze);
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
            var entrance = MazeSolverUtility.Entrance(maze);

            // Assert
            Assert.Equal(new Point {X = 3, Y = 0}, entrance);
        }

        [Fact]
        public void Computerphile_Maze_Exit()
        {
            // Arrange
            var maze = ImageBasedMaze.Create("../../../../images/Computerphile.jpg");

            // Act
            var entrance = MazeSolverUtility.Exit(maze);

            // Assert
            Assert.Equal(new Point {X = 7, Y = maze.Height - 1}, entrance);
        }

        [Fact]
        public void Daedelus_128x128_Entrance()
        {
            // Arrange
            var maze = ImageBasedMaze.Create("../../../../images/128x128.jpg");

            // Act
            var (foundSolution, solutionPath) = SimpleMazeSolver.SimpleSolver(maze);
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
            var (foundSolution, solutionPath) = SimpleMazeSolver.SimpleSolver(maze);
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
            var entrance = MazeSolverUtility.Entrance(maze);

            // Assert
            Assert.Equal(new Point {X = 29, Y = 0}, entrance);
        }

        [Fact]
        public void Daedelus_63x63_Exit()
        {
            // Arrange
            var maze = ImageBasedMaze.Create("../../../../images/31x31.jpg");

            // Act
            var entrance = MazeSolverUtility.Exit(maze);

            // Assert
            Assert.Equal(new Point {X = 37, Y = maze.Height - 1}, entrance);
        }

        [Fact]
        public void SimpleMaze1()
        {
            // Arrange
            IMaze maze = SimpleMaze.CreateSimpleMaze1();

            // Act
            var (foundSolution, solutionPath) = SimpleMazeSolver.SimpleSolver(maze);
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
            var (foundSolution, solutionPath) = SimpleMazeSolver.SimpleSolver(maze);
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
            var (foundSolution, _) = SimpleMazeSolver.SimpleSolver(maze);

            // Assert
            Assert.False(foundSolution);
        }

        [Fact]
        public void SimpleMaze1_Djikstra()
        {
            // Arrange
            var maze = ImageBasedMaze.Create("../../../../images/Computerphile.jpg");
            var graph = MazeSolverUtility.CreateGraph(maze);

            // Act
            IEnumerable<Point> solutionPath = Djikstra.Djikstra.Run(graph).NodeToPoint();
            maze.SavePath(solutionPath, "../../../../images/Computerphile_Djikstra_solution.jpg");

            // Assert
            Assert.Equal(solutionPath, new List<Point>
            {
                new Point {X = 3, Y = 0},
                new Point {X = 3, Y = 1},
                new Point {X = 6, Y = 1},
                new Point {X = 6, Y = 3},
                new Point {X = 5, Y = 3},
                new Point {X = 5, Y = 5},
                new Point {X = 5, Y = 8},
                new Point {X = 7, Y = 8},
                new Point {X = 7, Y = 9}
            });
        }

        [Fact]
        public void SimpleMaze1_AStar()
        {
            // Arrange
            var maze = ImageBasedMaze.Create("../../../../images/Computerphile.jpg");
            var graph = MazeSolverUtility.CreateGraph(maze);

            // Act
            IEnumerable<Point> solutionPath = Astar.Astar.Run(graph).NodeToPoint();
            maze.SavePath(solutionPath, "../../../../images/Computerphile_A*_solution.jpg");

            // Assert
            Assert.Equal(solutionPath, new List<Point>
            {
                new Point {X = 3, Y = 0},
                new Point {X = 3, Y = 1},
                new Point {X = 6, Y = 1},
                new Point {X = 6, Y = 3},
                new Point {X = 5, Y = 3},
                new Point {X = 5, Y = 5},
                new Point {X = 5, Y = 8},
                new Point {X = 7, Y = 8},
                new Point {X = 7, Y = 9}
            });
        }
    }
}