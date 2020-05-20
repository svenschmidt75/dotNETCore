using System;
using System.Collections.Generic;

namespace MazeSolver
{
    public static class SimpleMazeSolver
    {
        public static Point North = new Point {X = 0, Y = -1};
        public static Point South = new Point {X = 0, Y = 1};
        public static Point West = new Point {X = -1, Y = 0};
        public static Point East = new Point {X = 1, Y = 0};
        public static IEnumerable<Point> Directions = new[] {North, South, East, West};

        /// <summary>
        ///     This is a simple solver. It checks all 4 cardianal directions
        ///     and moves along those, that it has not yet seen.
        /// </summary>
        public static (bool, IEnumerable<Point>) SimpleSolver(IMaze maze)
        {
            Console.WriteLine(Environment.NewLine + "Finding solution using naive solve...");
            var visited = new HashSet<Point>();
            var path = new List<Point>();
            var start = MazeSolverUtility.Entrance(maze);
            using (MazeSolverUtility.ElapsedTime())
            {
                var solutionPath = SimpleSolver_internal(maze, start, visited, path);
                Console.WriteLine($"Solution using naive solve has {path.Count} nodes");
                return (solutionPath, path);
            }
        }

        private static bool SimpleSolver_internal(IMaze maze, Point start, HashSet<Point> visited, List<Point> path)
        {
            if (visited.Contains(start))
            {
                return false;
            }
            visited.Add(start);
            path.Add(start);
            if (start.Y == maze.Height - 1)
            {
                return true;
            }
            foreach (var direction in Directions)
            {
                var newPoint = new Point {X = start.X + direction.X, Y = start.Y + direction.Y};
                if (newPoint.X < 1 || newPoint.X >= maze.Width - 1)
                {
                    continue;
                }
                if (newPoint.Y < 1 || newPoint.Y > maze.Height - 1)
                {
                    continue;
                }
                if (maze.IsWall(newPoint.X, newPoint.Y))
                {
                    continue;
                }
                if (SimpleSolver_internal(maze, newPoint, visited, path))
                {
                    return true;
                }
            }
            path.Remove(start);
            return false;
        }
    }
}