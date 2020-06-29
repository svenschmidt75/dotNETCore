#region

using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

#endregion

namespace GoogleProblem20
{
    public class Solution
    {
        private readonly IDictionary<int, List<(int value, DateTime endTime)>> _map =
            new Dictionary<int, List<(int value, DateTime endTime)>>();

        public void Put(int key, int value, long durationMs)
        {
            var endTime = DateTime.Now + TimeSpan.FromMilliseconds(durationMs);

            List<(int val, DateTime endTime)> values;
            if (_map.TryGetValue(key, out values) == false)
            {
                values = new List<(int value, DateTime endTime)>();
                _map[key] = values;
            }

            values.Add((value, endTime));
        }

        public (bool, int) Get(int key)
        {
            if (_map.TryGetValue(key, out List<(int val, DateTime endTime)> values) == false)
            {
                return (false, -1);
            }

            var newValues = new List<(int val, DateTime endTime)>();
            for (var i = 0; i < values.Count; i++)
            {
                var (val, endTime) = values[i];
                if (DateTime.Now < endTime)
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
        [Test]
        public void Test1()
        {
            // Arrange

            // Act

            // Assert
        }
    }
}