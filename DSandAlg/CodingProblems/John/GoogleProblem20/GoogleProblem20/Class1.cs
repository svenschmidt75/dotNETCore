#region

using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

#endregion

namespace GoogleProblem20
{
    public class Solution
    {
        private readonly IDictionary<int, List<(int value, long endTime)>> _map =
            new Dictionary<int, List<(int value, long endTime)>>();

        private readonly ITimePiece _timePiece;

        public Solution(ITimePiece timePiece)
        {
            _timePiece = timePiece;
        }

        public void Put(int key, int value, long durationMs)
        {
            var endTime = _timePiece.GetTime() + durationMs;

            List<(int val, long endTime)> values;
            if (_map.TryGetValue(key, out values) == false)
            {
                values = new List<(int value, long endTime)>();
                _map[key] = values;
            }

            values.Add((value, endTime));
        }

        public (bool, int) Get(int key)
        {
            if (_map.TryGetValue(key, out List<(int val, long endTime)> values) == false)
            {
                return (false, -1);
            }

            var newValues = new List<(int val, long endTime)>();
            for (var i = 0; i < values.Count; i++)
            {
                var (val, endTime) = values[i];
                if (_timePiece.GetTime() < endTime)
                {
                    newValues.Add((val, endTime));
                }
            }

            _map[key] = newValues;

            return newValues.Any() ? (true, newValues[0].val) : (false, -1);
        }
    }

    [TestFixture]
    public class Tests
    {
        private class TestTimePiece : ITimePiece
        {
            private long _currentTime;

            public TestTimePiece()
            {
                _currentTime = 0;
            }

            void ITimePiece.Advance(long ms)
            {
                _currentTime += ms;
            }

            long ITimePiece.GetTime()
            {
                return _currentTime;
            }
        }

        [Test]
        public void Test1()
        {
            // Arrange
            var gp20 = new Solution(new TestTimePiece());
            gp20.Put('a', 10, 10);

            // Act
            var (found, value) = gp20.Get('a');

            // Assert
            Assert.IsTrue(found);
            Assert.AreEqual(10, value);
        }

        [Test]
        public void Test2()
        {
            // Arrange
            ITimePiece testTimePiece = new TestTimePiece();
            var gp20 = new Solution(testTimePiece);
            gp20.Put('a', 10, 10);

            // Act
            testTimePiece.Advance(20);
            var (found, value) = gp20.Get('a');

            // Assert
            Assert.IsFalse(found);
        }
    }
}