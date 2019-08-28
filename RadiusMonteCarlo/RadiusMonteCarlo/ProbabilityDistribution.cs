using System.Collections.Generic;

namespace RadiusMonteCarlo
{
    public class ProbabilityDistribution
    {
        private readonly double _min;
        private readonly double _max;
        private readonly double _binWidth;
        private readonly Dictionary<int, int> _bins;
        private readonly int _nEvents;

        public ProbabilityDistribution(double min, double max, double binWidth, Dictionary<int,int> bins, int nEvents)
        {
            _min = min;
            _max = max;
            _binWidth = binWidth;
            _bins = bins;
            _nEvents = nEvents;
        }

        public static void Plot()
        {
            
        }
        
        private static int BinIndex(double min, double max, int nBins, double value)
        {
            var binWidth = (max - min) / nBins;
            var binIndex = (int)((value - min) / binWidth);
            return binIndex;
        }

        public double Probability(double value)
        {
            int binIndex = BinIndex(_min, _max, _bins.Count, value);
            var nEventsInBin = _bins[binIndex];
            var probability = nEventsInBin / _nEvents;
            return probability;
        }
        
    }
}