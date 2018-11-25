using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Matching
{
    public class Matching
    {
        private static int _startTargetId;

        public static void Match(IReadOnlyList<Match> target, IReadOnlyList<Match> source)
        {
        }

        public void Match(IReadOnlyList<Match> target, IReadOnlyList<Match> source, double offset, double skipPenalty, Action<string> outputText)
        {
            // penalties as n x m grid for now, later optimize
            var nSource = source.Count;
            var nTarget = target.Count;

            // row: source
            // column: target
            var penalties = new double[nSource * nTarget];

            StartMapping(target, source, offset, skipPenalty, penalties);

            PrintPenaltyGrid(target.Count, source.Count, penalties, outputText);

            for (int targetId = _startTargetId; targetId < target.Count; targetId++)
            {

                PrintPenaltyGrid(target.Count, source.Count, penalties, outputText);

                for (int sourceId = 0; sourceId < source.Count; sourceId++)
                {

                    double bestPredPenalty = double.MaxValue;
                    int bestPredSourceId = -1;
                    int skippedAtEnd = 0;
                    if (targetId == target.Count - 1)
                    {
                        // last target element to match
                        skippedAtEnd = source.Count - 1 - sourceId;
                    }

                    for (int iPred = 0; iPred <= sourceId; iPred++)
                    {
                        var skipped = Math.Abs(iPred + 1 - sourceId);
                        var penaltyGridIndex = iPred * target.Count + targetId;
                        var penalty = penalties[penaltyGridIndex] + skipped * skipPenalty;
                        if (iPred < sourceId)
                        {
                            var odometerDiff = source[sourceId].Odom - target[targetId].Odom;
                            var odomPenalty = Math.Abs(odometerDiff + offset);
                            var jointLengthDiff = source[sourceId].JL - target[targetId].JL;
                            var jlPenalty = Math.Abs(jointLengthDiff);
                            var ownPenalty = odomPenalty + jlPenalty;
                            penalty += ownPenalty;
                        }
                        if (penalty < bestPredPenalty)
                        {
                            bestPredPenalty = penalty;
                            bestPredSourceId = iPred;
                        }
                    }
                    var penaltyGridIndex2 = sourceId * target.Count + targetId;
                    penalties[penaltyGridIndex2] = bestPredPenalty;
                }
            }

            Console.WriteLine();
            // propagate penalties

            // backtrace from end to start to get exact mapping for choice of offset and skipPenalty
        }

        private static void StartMapping(IReadOnlyList<Match> target, IReadOnlyList<Match> source, double offset, double skipPenalty, double[] penalties)
        {
            var first = new bool[source.Count];
            int targetId = 0;
            do
            {
                // assign penalties for mapping target0 to input
                for (int sourceId = 0; sourceId < source.Count; sourceId++)
                {
                    var odometerDiff = source[sourceId].Odom - target[targetId].Odom;
                    var odomPenalty = Math.Abs(odometerDiff + offset);
                    var jointLengthDiff = source[sourceId].JL - target[targetId].JL;
                    var jlPenalty = Math.Abs(jointLengthDiff);
                    
                    // TODO SS: Penalize jl differences more than skipping an element
                    var ownPenalty = odomPenalty + jlPenalty;
                    var suitable = ownPenalty <= skipPenalty;
                    if (suitable == false)
                    {
                        // sourceId unsuitable for match with targetId=0
                        ownPenalty = skipPenalty;
                    }

                    first[sourceId] = suitable;
                    penalties[sourceId * target.Count + targetId] = ownPenalty;
                }

                targetId++;
            } while (targetId < target.Count && first.All(x => x == false));

            _startTargetId = targetId;
        }


        public static void PrintPenaltyGrid(int nTarget, int nSource, double[] penalties, Action<string> outputText)
        {
            var strBuilder = new StringBuilder();
            strBuilder.Append($"{" ",6}");
            for (int i = 0; i < nTarget; i++)
            {
                strBuilder.Append($"{i,7}");
            }

            strBuilder.AppendLine();
            for (int j = 0; j < nSource; j++)
            {
                strBuilder.Append($"  {j,7}");
                for (int i = 0; i < nTarget; i++)
                {
                    int index = j * nTarget + i;
                    var penalty = penalties[index];
                    strBuilder.Append($"  {penalty:F3}");
                }
                strBuilder.AppendLine();
            }
            strBuilder.AppendLine();
            outputText(strBuilder.ToString());
        }
    }
}