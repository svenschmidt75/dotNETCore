using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace MazeSolver
{
    public static class MazeSolverUtility
    {
        public static Graph CreateGraph(IMaze maze)
        {
            Console.WriteLine(Environment.NewLine + "Creating graph...");
            var nodes = Enumerable.Range(0, maze.Height).
                                   Select(_1 => Enumerable.Range(0, maze.Width).Select(_2 => (Node) null).ToList()).
                                   ToList();
            var start = Entrance(maze);
            var end = Exit(maze);
            var graph = CreateGraph(start, end);
            CreateNodes(maze, graph, nodes, start, end);
//            Console.WriteLine("Connection along x axis...");
            ConnectAlongXAxis(maze, graph, nodes);
//            Console.WriteLine("Connection along y axis...");
            ConnectAlongYAxis(maze, graph, nodes);
            return graph;
        }

        private static Graph CreateGraph(Point start, Point end)
        {
            var startNode = new Node($"{start.X}, {start.Y}", Distance(start, end));
            var endNode = new Node($"{end.X}, {end.Y}", 0);
            var graph = new Graph(startNode, endNode);
            graph.Add(startNode);
            graph.Add(endNode);
            return graph;
        }

        private static void ConnectAlongYAxis(IMaze maze, Graph graph, IReadOnlyList<List<Node>> nodes)
        {
            for (var x = 1; x < maze.Width - 1; x++)
            {
                Node prevNode = null;
                var prevY = 0;
                for (var y = 0; y < maze.Height; y++)
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
                        var distance = y - prevY;
                        graph.Nodes[node].Add(new Edge {Node = prevNode, Weight = distance});
                        graph.Nodes[prevNode].Add(new Edge {Node = node, Weight = distance});
//                        Console.WriteLine($"Connecting nodes ({prevNode.Name}) and ({node.Name}) with distance {distance}");
                    }
                    prevNode = node;
                    prevY = y;
                }
            }
        }

        private static void ConnectAlongXAxis(IMaze maze, Graph graph, IReadOnlyList<List<Node>> nodes)
        {
            for (var y = 1; y < maze.Height - 1; y++)
            {
                var prevX = 1;
                Node prevNode = null;
                for (var x = 1; x < maze.Width - 1; x++)
                {
                    if (maze.IsWall(x, y))
                    {
                        prevNode = null;
                        prevX = x;
                        continue;
                    }
                    var node = nodes[y][x];
                    if (node == null)
                    {
                        continue;
                    }
                    if (prevNode != null)
                    {
                        var distance = x - prevX;
                        graph.Nodes[node].Add(new Edge {Node = prevNode, Weight = distance});
                        graph.Nodes[prevNode].Add(new Edge {Node = node, Weight = distance});
//                        Console.WriteLine($"Connecting nodes ({prevNode.Name}) and ({node.Name}) with distance {distance}");
                    }
                    prevNode = node;
                    prevX = x;
                }
            }
        }

        private static void CreateNodes(IMaze maze, Graph graph, List<List<Node>> nodes, Point start, Point end)
        {
            Console.WriteLine("Creating nodes...");
            nodes[0][start.X] = graph.Start;
            nodes[maze.Height - 1][end.X] = graph.End;
            for (var y = 1; y < maze.Height - 1; y++)
            for (var x = 1; x < maze.Width - 1; x++)
            {
                if (maze.IsWall(x, y))
                {
                    continue;
                }
                if (maze.IsWall(x - 1, y) && maze.IsWall(x + 1, y))
                {
                    if (maze.IsWall(x, y - 1) == false && maze.IsWall(x, y + 1) == false)
                    {
                        continue;
                    }
                }
                if (maze.IsWall(x, y - 1) && maze.IsWall(x, y + 1))
                {
                    if (maze.IsWall(x - 1, y) == false && maze.IsWall(x + 1, y) == false)
                    {
                        continue;
                    }
                }
                var node = new Node($"{x}, {y}", Distance(new Point {X = x, Y = y}, end));
//                Console.WriteLine($"Creating node at ({x}, {y}) with A* distance {node.DistanceToEnd}");
                nodes[y][x] = node;
                graph.Add(node);
            }
            Console.WriteLine($"Found {graph.Nodes.Count} nodes");
        }

        private static int Distance(Point start, Point end)
        {
            return (start.X - end.X) * (start.X - end.X) + (start.Y - end.Y) * (start.Y - end.Y);
        }

        public static IDisposable ElapsedTime()
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            return new DisposableHelper(() =>
            {
                var elapsedTime = stopWatch.ElapsedMilliseconds / 1000.0f;
                Console.WriteLine($"Elapsed time: {elapsedTime}");
            });
        }

        public static Point Entrance(IMaze maze)
        {
            var (found, entrance) = GetPoint(maze, 0);
            if (found == false)
            {
                throw new InvalidOperationException("Maze has no entrance");
            }
            return entrance;
        }

        public static Point Exit(IMaze maze)
        {
            var (found, entrance) = GetPoint(maze, maze.Height - 1);
            if (found == false)
            {
                throw new InvalidOperationException("Maze has no exit");
            }
            return entrance;
        }

        private static (bool, Point) GetPoint(IMaze maze, int y)
        {
            for (var x = 0; x < maze.Width; x++)
            {
                if (maze.IsWall(x, y) == false)
                {
                    return (true, new Point {X = x, Y = y});
                }
            }
            return (false, new Point());
        }

        public static IEnumerable<Point> NodeToPoint(this IEnumerable<Node> nodes)
        {
            return nodes.Select(n =>
            {
                var cs = n.Name.Split(',');
                var x = int.Parse(cs[0].Trim());
                var y = int.Parse(cs[1].Trim());
                return new Point {X = x, Y = y};
            }).ToList();
        }

        public static IEnumerable<Point> ExtendPath(this IEnumerable<Point> path)
        {
            return path.Zip(path.Skip(1), (prev, current) =>
            {
                if (prev.Y == current.Y)
                {
                    var direction = prev.X < current.X ? 1 : -1;
                    var n = Math.Abs(current.X - prev.X);
                    return Enumerable.Range(0, n).Select(i => new Point {X = prev.X + direction * i, Y = prev.Y});
                }
                if (prev.X == current.X)
                {
                    var direction = prev.Y < current.Y ? 1 : -1;
                    var n = Math.Abs(current.Y - prev.Y);
                    return Enumerable.Range(0, n).Select(i => new Point {X = prev.X, Y = prev.Y + direction * i});
                }
                return new[] {prev};
            }).SelectMany(x => x).Append(path.Last());
        }
    }
}