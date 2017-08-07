using System;
using System.Collections.Generic;
using System.Linq;
using Djikstra;

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

        public static bool Follow(IMaze maze, Point start, HashSet<Point> visited, List<Point> path)
        {
            if (visited.Contains(start))
            {
                return false;
            }
            visited.Add(start);
            path.Add(start);
            if (start.Y == maze.Height - 1)
            {
                // exit found
                return true;
            }
            foreach (var direction in Directions)
            {
                var newPoint = new Point {X = start.X + direction.X, Y = start.Y + direction.Y};
                if (newPoint.X < 1 || newPoint.X >= maze.Width - 1)
                {
                    // at wall, cannot go in this direction
                    continue;
                }
                if (newPoint.Y < 1 || newPoint.Y > maze.Height - 1)
                {
                    // at wall, cannot go in this direction
                    continue;
                }
                if (maze.IsWall(newPoint.X, newPoint.Y))
                {
                    continue;
                }
                if (Follow(maze, newPoint, visited, path))
                {
                    return true;
                }
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