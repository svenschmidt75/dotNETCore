using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace RadiusMonteCarlo
{
    public class ProbabilityDistribution
    {
        private readonly double _min;
        private readonly double _max;
        private readonly double _binWidth;
        private readonly Dictionary<int, int> _bins;
        private readonly int _nBins;
        private readonly int _nEvents;

        public ProbabilityDistribution(double min, double max, double binWidth, Dictionary<int,int> bins, int nBins, int nEvents)
        {
            _min = min;
            _max = max;
            _binWidth = binWidth;
            _bins = bins;
            _nBins = nBins;
            _nEvents = nEvents;
        }

        public void Plot()
        {
            int maxBinIndex = BinIndex(_min, _max, _nBins, _max);
            var binWidth = (_max - _min) / _nBins;
            for (int binIndex = 0; binIndex < maxBinIndex; binIndex++)
            {
                double min = _min + binIndex * binWidth;
                double max = _min + (binIndex + 1) * binWidth;
                double midPoint = (min + max) / 2.0;
                double probability = Probability(midPoint);
                Console.WriteLine($"{midPoint} {probability}");
            }
        }
        
        private static int BinIndex(double min, double max, int nBins, double value)
        {
            var binWidth = (max - min) / nBins;
            var binIndex = (int)((value - min) / binWidth);
            return binIndex;
        }

        public double Probability(double value)
        {
            Debug.Assert(value >= _min);
            Debug.Assert(value <= _max);
            int binIndex = BinIndex(_min, _max, _bins.Count, value);
            int nEventsInBin = 0;
            if (_bins.ContainsKey(binIndex))
            {
                nEventsInBin += _bins[binIndex];
            }
            var probability = (double)nEventsInBin / _nEvents;
            return probability;
        }
        
    }
}