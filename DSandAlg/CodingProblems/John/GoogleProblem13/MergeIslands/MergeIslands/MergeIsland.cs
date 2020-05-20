#region

using NUnit.Framework;

#endregion

namespace MergeIslands
{
    public class MergeIsland
    {
        private readonly DisjointSet _disjointSet;
        private readonly int[] _grid;
        private readonly int _ncols;
        private readonly int _nrows;

        public MergeIsland(int nrows, int ncols)
        {
            _nrows = nrows;
            _ncols = ncols;
            _grid = new int[nrows * ncols];
            _disjointSet = new DisjointSet();
        }

        public int Add(int row, int col)
        {
            // SS: each add unions at most 4 sets, so O(1) per add
            var index = ToIndex(row, col);

            var setIdx = _disjointSet.MakeSet();
            _grid[index] = setIdx + 1;

            MergeIslands(row, col);

            return _disjointSet.NumberOfNodes;
        }

        private void MergeIslands(int row, int col)
        {
            // SS: runtime is amortized O(1) (runtime of merge, find, ... is O(1)) 
            var index = ToIndex(row, col);

            var neighbors = new[] {(row - 1, col), (row, col - 1), (row, col + 1), (row + 1, col)};

            void Merge(int r, int c)
            {
                var otherIndex = ToIndex(r, c);
                if (otherIndex >= 0 && otherIndex < _nrows * _ncols)
                {
                    var otherSetIdx = _grid[otherIndex] - 1;
                    if (otherSetIdx < 0)
                    {
                        return;
                    }
                    otherSetIdx = _disjointSet.Find(otherSetIdx);

                    var setIdx = _grid[index] - 1;
                    setIdx = _disjointSet.Find(setIdx);

                    if (setIdx != otherSetIdx)
                    {
                        // SS: merge islands
                        var idx = _disjointSet.Merge(setIdx, otherSetIdx);
                        
                        // SS: replace grid index with representative for the merged set
                        _grid[index] = idx + 1;
                    }
                }
            }

            foreach (var neighbor in neighbors)
            {
                Merge(neighbor.Item1, neighbor.Item2);
            }
        }

        private int ToIndex(int row, int col)
        {
            var index = row * _ncols + col;
            return index;
        }
    }

    [TestFixture]
    public class Test
    {
        [Test]
        public void Test1()
        {
            // Arrange
            var mergeIsland = new MergeIsland(6, 6);

            // Act / Assert
            var nIslands = mergeIsland.Add(2, 2);
            Assert.AreEqual(nIslands, 1);

            nIslands = mergeIsland.Add(1, 3);
            Assert.AreEqual(nIslands, 2);

            nIslands = mergeIsland.Add(3, 3);
            Assert.AreEqual(nIslands, 3);

            nIslands = mergeIsland.Add(2, 3);
            Assert.AreEqual(nIslands, 1);
        }
    }
}