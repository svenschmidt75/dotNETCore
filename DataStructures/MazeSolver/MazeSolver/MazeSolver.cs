using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

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
        public static (bool, IEnumerable<Point>) SimpleSolver(IMaze maze)
        {
            // TODO SS: Print elapsed time
            HashSet<Point> visited = new HashSet<Point>();
            List<Point> path = new List<Point>();
            var start = Entrance(maze);
            using (ElapsedTime())
            {
                return (SimpleSolver_internal(maze, start, visited, path), path);
            }
        }

        private static IDisposable ElapsedTime()
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();
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

        public static Graph CreateGraph(IMaze maze)
        {
            var nodes = Enumerable.Range(0, maze.Height).
                                   Select(_1 => Enumerable.Range(0, maze.Width).Select(_2 => (Node) null).ToList()).
                                   ToList();
            var start = Entrance(maze);
            var startNode = new Node($"{start.X}, {start.Y}");
            nodes[0][start.X] = startNode;
            var end = Exit(maze);
            var endNode = new Node($"{end.X}, {end.Y}");
            nodes[maze.Height - 1][end.X] = endNode;
            var graph = new Graph(startNode, endNode);
            graph.Add(startNode);
            graph.Add(endNode);

            for (int y = 1; y < maze.Height - 1; y++)
            {
                int prevX = 1;
                Node prevNode = null;
                for (int x = 1; x < maze.Width - 1; x++)
                {
                    if (maze.IsWall(x, y))
                    {
                        prevNode = null;
                        prevX = x;
                        continue;
                    }
                    if (maze.IsWall(x - 1, y) && maze.IsWall(x, y) == false && maze.IsWall(x + 1, y))
                    {
                        // 101
                        if (maze.IsWall(x, y - 1) == false && maze.IsWall(x, y + 1) == false)
                        {
                            continue;
                        }
                    }
                    if (maze.IsWall(x, y - 1) && maze.IsWall(x, y) == false && maze.IsWall(x, y + 1))
                    {
                        // 1
                        // 0
                        // 1
                        if (maze.IsWall(x - 1, y) == false && maze.IsWall(x + 1, y) == false)
                        {
                            continue;
                        }
                    }
                    Console.WriteLine($"Creating node at ({x}, {y})");
                    var node = new Node($"{x}, {y}");
                    nodes[y][x] = node;
                    var edges = graph.Add(node);
                    if (prevNode != null)
                    {
                        int distance = x - prevX;
                        edges.Add(new Edge {Node = prevNode, Weight = distance});
                        graph.Nodes[prevNode].Add(new Edge {Node = node, Weight = distance});
                        Console.WriteLine($"Connection nodes ({prevNode.Name}) and ({node.Name}) with distance {distance}");
                    }
                    prevNode = node;
                    prevX = x;
                }
            }

            Console.WriteLine("Connection along y axis...");

            for (int x = 1; x < maze.Width - 1; x++)
            {
                Node prevNode = null;
                int prevY = 0;
                for (int y = 0; y < maze.Height; y++)
                {
                    if (maze.IsWall(x, y))
                    {
                        prevNode = null;
                        prevY = y;
                        continue;
                    }

                    var node = nodes[y][x];
                    if (node == null)
                    {
                        continue;
                    }
                    if (prevNode != null)
                    {
                        int distance = y - prevY;
                        graph.Nodes[node].Add(new Edge {Node = prevNode, Weight = distance});
                        graph.Nodes[prevNode].Add(new Edge {Node = node, Weight = distance});
                        Console.WriteLine($"Connection nodes ({prevNode.Name}) and ({node.Name}) with distance {distance}");
                    }
                    prevNode = node;
                    prevY = y;
                }
            }

            return graph;
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