using System;
using System.Collections.Generic;
using System.Linq;
using MazeSolver;

namespace Astar
{
    public static class Astar
    {
        /// <summary>
        ///     A* is essentially Djikstra, but adds a heuristic for how far we have
        ///     to go. This addresses a weakness of Djikstra, which follows the
        ///     shortest path irrespective of the direction it is going.
        ///     Remember that for each node, Djikstra keeps track of the shortest
        ///     distance to the start node. A* adds a heuristic to also keep track
        ///     of how far each node is from the end node. This is obviously unknown,
        ///     thus the heuristic approach.
        /// </summary>
        public static IEnumerable<Node> Run(Graph graph)
        {
            /* For each node, the 'weights' hash map contains the node's
             * cost from the start node, as well as the combined heuristic.
             * As we move through the graph, we update the cost if we find
             * a cheaper path.
             */
            var weights = new Dictionary<Node, Item>();

            /* Whereas the 'weights' hash map contains the cost, we also
             * need to keep track of the cheapest path as we traverse
             * the graph. This is done with the 'parent' hash map, for
             * each node, we store the node we came from.
            */
            var parents = new Dictionary<Node, Node>();

            // set the parent for the nodes that can be reached from the start node
            var edges = graph[graph.Start];
            edges.ForEach(edge => parents[edge.Node] = graph.Start);

            // initialize node weights
            graph.Nodes.ForEach(node =>
            {
                weights[node.Key] = new Item
                {
                    DistanceFromStart = int.MaxValue,
                    CombinedHeuristic = int.MaxValue
                };
            });
            var startNode = graph.Start;
            weights[startNode] = new Item {DistanceFromStart = 0, CombinedHeuristic = startNode.DistanceToEnd};

            var toProcess = new HashSet<Node>();
            graph.Nodes.ForEach(edge => toProcess.Add(edge.Key));
            while (toProcess.Any())
            {
                // This is where Djikstra and A* differ: While Djikstra simply walks along
                // the shortests weights, A* prioritizes nodes that have a shorter distance
                // to the end node (the heuristic).
                // Note that we only track the weights for the actual path length, so this
                // is the same as Djikstra.
                var currentNode = FindCheapestNode(toProcess, weights);
                if (currentNode == graph.End)
                {
                    Console.WriteLine("End node reached");
                    toProcess.Clear();
                }
                toProcess.Remove(currentNode);
                Console.WriteLine($"At current node {currentNode.Name}");
                var toNodes = graph[currentNode];
                toNodes.ForEach(currentNodeEdge =>
                {
                    var n2 = currentNodeEdge.Node;
                    var weight = currentNodeEdge.Weight;
                    Console.WriteLine($"Edge ({currentNode.Name}, {n2.Name}) = {weight}");
                    if (toProcess.Contains(n2) == false)
                    {
                        // we have already processed n2
                        Console.WriteLine($"Skipping {n2.Name}...");
                        return;
                    }
                    var newWeight = weights[currentNode].DistanceFromStart + weight;
                    if (weights[n2].DistanceFromStart > newWeight)
                    {
                        Console.WriteLine($"Edge ({currentNode.Name}, {n2.Name}) = {newWeight}");
                        weights[n2] = new Item
                        {
                            DistanceFromStart = newWeight,
                            CombinedHeuristic = newWeight + n2.DistanceToEnd
                        };
                        parents[n2] = currentNode;
                    }
                });
            }
            var shortestPath = GetShortestPath(graph, parents);
            return shortestPath;
        }

        private static Node FindCheapestNode(HashSet<Node> toProcess, Dictionary<Node, Item> weights)
        {
            // TODO SS: We should use a priority queue...

            // A*: order by combined heuristic, i.e. distance from start node + distance to end
            // Djikstra: order only by distance from start node
            var orderedByWeight =
                weights.OrderBy(w => w.Value.CombinedHeuristic).Where(w2 => toProcess.Contains(w2.Key));
            return orderedByWeight.First().Key;
        }

        private static IEnumerable<Node> GetShortestPath(Graph graph, IReadOnlyDictionary<Node, Node> parents)
        {
            var shortestPath = new List<Node>();
            var n = graph.End;
            while (n != graph.Start)
            {
                shortestPath.Add(n);
                n = parents[n];
            }
            shortestPath.Add(graph.Start);
            shortestPath.Reverse();
            return shortestPath;
        }

        public struct Item
        {
            public int DistanceFromStart { get; set; }
            public int CombinedHeuristic { get; set; }
        }
    }
}