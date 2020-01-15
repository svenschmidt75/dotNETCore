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
                // errors going from targetId - 1 to targetId, indexed by sourceId
                var penaltiesAfter = new double[source.Count];
                for (int sourceId = 0; sourceId < source.Count; sourceId++)
                {
                    PrintPenaltyGrid(target.Count, source.Count, penalties, outputText);

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
                        var penaltyGridIndex = sourceId * target.Count + (targetId - 1);
                        var iPredPenalty = penalties[penaltyGridIndex];
                        var penalty = iPredPenalty + skipped * skipPenalty;
                        if (iPred < sourceId)
                        {
                            var odometerDiff = sourceId - targetId;
                            var odomPenalty = Math.Abs(odometerDiff + offset);
                            var jointLengthDiff = source[sourceId].JL - target[targetId].JL;
                            var jlPenalty = Math.Abs(jointLengthDiff);
                            var ownPenalty = odomPenalty + jlPenalty;
                            penalty += ownPenalty;
                        }
                        penalty += skippedAtEnd * skipPenalty;
                        if (penalty < bestPredPenalty)
                        {
                            bestPredPenalty = penalty;
                            bestPredSourceId = iPred;
                        }
                    }
                    penaltiesAfter[sourceId] = bestPredPenalty;
                }
                for (int sourceId = 0; sourceId < source.Count; sourceId++)
                {
                    var penalty = penaltiesAfter[sourceId];
                    var penaltyGridIndex = sourceId * target.Count + targetId;
                    penalties[penaltyGridIndex] = penalty;
                }
            }
            PrintPenaltyGrid(target.Count, source.Count, penalties, outputText);

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
                    var odometerDiff = sourceId - targetId;
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
            strBuilder.AppendLine();
            strBuilder.Append($"{" ",10}");
            for (int i = 0; i < nTarget; i++)
            {
                strBuilder.Append($"{i,9}");
            }

            strBuilder.AppendLine();
            for (int j = 0; j < nSource; j++)
            {
                strBuilder.Append($"  {j,8}");
                for (int i = 0; i < nTarget; i++)
                {
                    int index = j * nTarget + i;
                    var penalty = penalties[index];
                    strBuilder.Append($"  {penalty,7:F3}");
                }
                strBuilder.AppendLine();
            }
            strBuilder.AppendLine();
            outputText(strBuilder.ToString());
        }
    }
}