using System;
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
            
            Dictionary<int, int> bins = new Dictionary<int, int>();
            
            for (int sampleIndex = 0; sampleIndex < nSamples; sampleIndex++)
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
 
                // find bin index
                var binIndex = BinIndex(minR, maxR, nBins, r);
                bins[binIndex]++;
            }

            var binWidth = (maxR - minR) / nBins;
            return new ProbabilityDistribution(minR, maxR, binWidth, bins, nEvents);
        }
        
    }
}