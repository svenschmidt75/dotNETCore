using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace MazeSolver
{
    public static class MazeSolver
    {
        public static Point North = new Point {X = 0, Y = -1};
        public static Point South = new Point {X = 0, Y = 1};
        public static Point West = new Point {X = -1, Y = 0};
        public static Point East = new Point {X = 1, Y = 0};
        public static IEnumerable<Point> Directions = new[] {North, South, East, West};

        public static IEnumerable<Point> SolveByFloodfille(IMaze maze)
        {
            var processed = new HashSet<Point>();
            var start = Entrance(maze);
            return Enumerable.Empty<Point>();
        }

        /// <summary>
        ///     This is a simple solver. It checks all 4 cardianal directions
        ///     and moves along those, that it has not yet seen.
        /// </summary>
        public static bool SimpleSolver(IMaze maze, HashSet<Point> visited, List<Point> path)
        {
            // TODO SS: Print elapsed time
            var start = Entrance(maze);
            using (ElapsedTime())
            {
                return SimpleSolver_internal(maze, start, visited, path);
            }
        }

        private static IDisposable ElapsedTime()
        {
            var stopWatch = new Stopwatch();
            return new DisposableHelper(() =>
            {
                var elapsedTime = stopWatch.ElapsedMilliseconds / 1000.0f;
                Console.WriteLine($"Elapsed time: {elapsedTime}");
            });
        }

        private static bool SimpleSolver_internal(IMaze maze, Point start, HashSet<Point> visited, List<Point> path)
        {
            if (visited.Contains(start))
                return false;
            visited.Add(start);
            path.Add(start);
            if (start.Y == maze.Height - 1)
                return true;
            foreach (var direction in Directions)
            {
                var newPoint = new Point {X = start.X + direction.X, Y = start.Y + direction.Y};
                if (newPoint.X < 1 || newPoint.X >= maze.Width - 1)
                    continue;
                if (newPoint.Y < 1 || newPoint.Y > maze.Height - 1)
                    continue;
                if (maze.IsWall(newPoint.X, newPoint.Y))
                    continue;
                if (SimpleSolver_internal(maze, newPoint, visited, path))
                    return true;
            }
            path.Remove(start);
            return false;
        }

//        public static Graph CreateGraph(IMaze maze)
//        {
//        }

        public static Point Entrance(IMaze maze)
        {
            var (found, entrance) = GetPoint(maze, 0);
            if (found == false)
                throw new InvalidOperationException("Maze has no entrance");
            return entrance;
        }

        private static (bool, Point) GetPoint(IMaze maze, int y)
        {
            for (var x = 0; x < maze.Width; x++)
                if (maze.IsWall(x, y) == false)
                    return (true, new Point {X = x, Y = y});
            return (false, new Point());
        }

        public static Point Exit(IMaze maze)
        {
            var (found, entrance) = GetPoint(maze, maze.Height - 1);
            if (found == false)
                throw new InvalidOperationException("Maze has no exit");
            return entrance;
        }
    }
}