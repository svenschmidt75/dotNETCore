using System;
using System.Collections.Generic;

namespace MazeSolver
{
    public static class MazeSolver
    {
        public static IEnumerable<Point> SolveByFloodfille(IMaze maze)
        {
            var processed = new HashSet<Point>();
            var start = Entrance(maze);
        }

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