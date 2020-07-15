#region

using System.Collections.Generic;
using NUnit.Framework;

#endregion

namespace AlgoExpert_AirportConnections
{
    public class Solution
    {
        public int Solve(List<string> airports, List<string[]> connections, string startAirport)
        {
            // SS: runtime complexity: 
            // 1. construct graph - a: add vertices: O(V)
            //                    - b: add edges: O(E)
            // 2. add number of vertices a vertex can read to each vertex: O(V), DF graph traversal
            // 3. create PQ: O(V), Floyd's algorithm
            // 4. count number of connected components: O(V + V log V)
            // total runtime: O(V log V + E)

            // Alternatively, remove cycles (i.e. collapse strongly connected components) and
            // connect the starting vertex to all in degree zero vertices in the compressed graph, i.e.
            // connect to those nodes that that have no incoming edges...
            // The answer then is the number of in-degree 0 nodes that are not the starting vertex!
            
            var graph = new Graph();

            // SS: map airport names to vertices
            var airport2VertexMap = new Dictionary<string, Vertex>();
            for (var i = 0; i < airports.Count; i++)
            {
                var airport = airports[i];
                var vertex = new Vertex {Index = i, Name = airport};
                airport2VertexMap[airport] = vertex;
                graph.AddNode(vertex);
            }

            // SS: create edges (connections)
            for (var i = 0; i < connections.Count; i++)
            {
                var connection = connections[i];

                var fromAirport = connection[0];
                var fromVertex = airport2VertexMap[fromAirport];

                var toAirport = connection[1];
                var toVertex = airport2VertexMap[toAirport];

                graph.AddDirectedEdge(fromVertex, toVertex);
            }

            var startVertex = airport2VertexMap[startAirport];

            // SS: the solution is the number of connected components in the graph, because:
            // We take the vertex with the most reachable vertices and connect it to the
            // vertex in another component that reaches the most vertices in that component.
            // 1. for each vertex, determine the number of nodes it can reach
            // 2. put the node and the number of vertices it can reach (i.e. priority) in a PQ
            // 3. pop pq, if not yet visit, visit node, else next
            // 4. do until no more nodes can be visited from this component and increase counter
            // 5. do until no more nodes (i.e. max heap is empty)

            var reach = CalculateReach(airport2VertexMap.Values, graph);

            // SS: We could use Floyd's algorithm here...
            var pq = PriorityQueue<Vertex>.CreateMaxPriorityQueue();
            var toVisit = new HashSet<Vertex>();

            foreach (var item in reach)
            {
                pq.Enqueue(item.Key, item.Value);
                toVisit.Add(item.Key);
            }

            var maxDistance = 0;
            while (pq.IsEmpty == false)
            {
                var (priority, vertex) = pq.Dequeue();
                if (toVisit.Contains(vertex))
                {
                    toVisit.Remove(vertex);
                    Process(vertex, toVisit, graph);
                    maxDistance++;
                }
            }

            return maxDistance;
        }

        private static void Process(Vertex vertex, HashSet<Vertex> toVisit, Graph graph)
        {
            // SS: paint all reachable nodes
            var neighbors = graph.AdjacencyList[vertex];
            for (var i = 0; i < neighbors.Count; i++)
            {
                var neighbor = neighbors[i];
                if (toVisit.Contains(neighbor))
                {
                    toVisit.Remove(neighbor);
                    Process(neighbor, toVisit, graph);
                }
            }
        }

        private IDictionary<Vertex, int> CalculateReach(IEnumerable<Vertex> vertices, Graph g)
        {
            var reach = new Dictionary<Vertex, int>();
            foreach (var vertex in vertices)
            {
                var visited = new HashSet<Vertex> {vertex};
                CalculateReach(vertex, reach, visited, g);
            }
            return reach;
        }

        private int CalculateReach(Vertex vertex, Dictionary<Vertex, int> reach, HashSet<Vertex> visited, Graph graph)
        {
            // SS: number of vertices reachable from this vertex

            if (reach.TryGetValue(vertex, out var reachable))
            {
                return reachable;
            }

            var neighbors = graph.AdjacencyList[vertex];
            for (var i = 0; i < neighbors.Count; i++)
            {
                var neighbor = neighbors[i];

                if (visited.Contains(neighbor))
                {
                    // SS: do not count again
                    continue;
                }

                if (reach.TryGetValue(neighbor, out int v))
                {
                    reachable += v + 1;
                }
                else
                {
                    visited.Add(neighbor);
                    var r = CalculateReach(neighbor, reach, visited, graph) + 1;
                    reachable += r;
                }
            }

            reach[vertex] = reachable;
            return reachable;
        }
    }

    [TestFixture]
    public class Tests
    {
        [Test]
        public void Test1()
        {
            // Arrange
            var airports = new List<string>
            {
                "BGI", "CDG", "DEL", "DOH", "DSM", "EWR", "EYW", "HND", "ICN", "JFK", "LGA", "LHR", "ORD", "SAN", "SFO"
                , "SIN", "TLV", "BUD"
            };

            var connections = new List<string[]>
            {
                new[] {"DSM", "ORD"}
                , new[] {"ORD", "BGI"}
                , new[] {"BGI", "LGA"}
                , new[] {"SIN", "CDG"}
                , new[] {"CDG", "SIN"}
                , new[] {"CDG", "BUD"}
                , new[] {"DEL", "DOH"}
                , new[] {"DEL", "CDG"}
                , new[] {"TLV", "DEL"}
                , new[] {"EWR", "HND"}
                , new[] {"HND", "ICN"}
                , new[] {"HND", "JFK"}
                , new[] {"ICN", "JFK"}
                , new[] {"JFK", "LGA"}
                , new[] {"EYW", "LHR"}
                , new[] {"LHR", "SFO"}
                , new[] {"SFO", "SAN"}
                , new[] {"SFO", "DSM"}
                , new[] {"SAN", "EYW"}
            };

            var startAirport = "LGA";

            // Act
            var minConnections = new Solution().Solve(airports, connections, startAirport);

            // Assert
            Assert.AreEqual(3, minConnections);
        }
    }
}