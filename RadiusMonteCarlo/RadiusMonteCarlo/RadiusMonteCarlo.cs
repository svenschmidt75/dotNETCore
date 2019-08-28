﻿using System;
using System.Collections.Generic;

namespace RadiusMonteCarlo
{
    public class RadiusMonteCarlo
    {
        private static int BinIndex(double min, double max, int nBins, double value)
        {
            var binWidth = (max - min) / nBins;
            var binIndex = (int)((value - min) / binWidth);
            return binIndex;
        }

        public static ProbabilityDistribution Run(int nSamples, int nBins)
        {
            double minR = double.MaxValue;
            double maxR = double.MinValue;

            var rand = new Random(DateTime.Now.Millisecond);

            int nEvents = 0;

            List<double> values = new List<double>(nSamples);

            while (nEvents < nSamples)
            {
                double x = rand.NextDouble();
                double y = rand.NextDouble();
                var r = Math.Sqrt((x - 0.5) * (x - 0.5) + (y - 0.5) * (y - 0.5));
                if (r > 1)
                {
                    continue;
                }
                nEvents++;
                minR = Math.Min(minR, r);
                maxR = Math.Max(maxR, r);
                values.Add(r);
            }

            return CreateProbabilityDistribution(values, nBins, minR, maxR);
        }

        private static ProbabilityDistribution CreateProbabilityDistribution(List<double> values, int nBins, double min, double max)
        {
            int nEvents = values.Count;
            Dictionary<int, int> bins = new Dictionary<int, int>();
            values.ForEach(value =>
            {
                var binIndex = BinIndex(min, max, nBins, value);
                if (bins.ContainsKey(binIndex) == false)
                {
                    bins.Add(binIndex, 0);
                }
                bins[binIndex]++;
            });
            var binWidth = (max - min) / nBins;
            return new ProbabilityDistribution(min, max, binWidth, bins, nBins, nEvents);
        }

    }
}