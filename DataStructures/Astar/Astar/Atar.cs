using System.Collections.Generic;
using System.Linq;
using Djikstra;

namespace Astar
{
    public static class Astar
    {
        /// <summary>
        /// A* is essentially Djikstra, but adds a heuristic for how far we have
        /// to go. This addresses a weakness of Djikstra, which follows the
        /// shortest path irrespective of the direction it is going.
        /// </summary>
        public static IEnumerable<Node> Run(Graph graph)
        {



            return Enumerable.Empty<Node>();
        }
    }
}
