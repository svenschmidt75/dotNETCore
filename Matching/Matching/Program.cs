using System;
using System.Collections.Generic;
using System.Linq;

namespace Matching
{
    public static class Matching
    {
        public static void Match(IReadOnlyList<Match> source, IReadOnlyList<Match> target)
        {
            
        }
        
        public static void Match(IReadOnlyList<Match> source, IReadOnlyList<Match> target, double offset, double skipPenalty)
        {
            // penalties as n x m grid for now, later optimize
            var nSource = source.Count;
            var nTarget = target.Count;

            // row: source
            // column: target
            var penalties = new List<double>(Enumerable.Range(0, nSource * nTarget).Select(x => 0.0));
            
            // assign penalties for mapping target0 to input
            for (int sourceId = 0; sourceId < nSource; sourceId++)
            {
                var ownPenalty = Math.Abs(source[sourceId].JointLength - target[0].JointLength + offset);
                penalties[sourceId * nTarget] = ownPenalty;
            }
            
            Console.WriteLine();
            // propagate penalties

            // backtrace from end to start to get exact mapping for choice of offset and skipPenalty
            
        }
        
        
        
        
        
        
    }
}