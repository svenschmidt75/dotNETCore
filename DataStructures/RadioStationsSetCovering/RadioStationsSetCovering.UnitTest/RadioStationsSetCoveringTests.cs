using System;
using System.Collections.Generic;
using Xunit;

namespace RadioStationsSetCovering.UnitTest
{
    public class RadioStationsSetCoveringTests
    {
        [Fact]
        public void Test1()
        {
            // Arrange
            var set = new Dictionary<int, HashSet<int>>
            {
                {0, new HashSet<int> {1, 2, 3}},
                {1, new HashSet<int> {2, 4}},
                {2, new HashSet<int> {3, 4}},
                {3, new HashSet<int> {4, 5}},
            };

            var universe = new HashSet<int> {1, 2, 3, 4, 5};

            // Act
            var covering = SetCovering.Run(set, universe);

            // Assert
            Assert.Equal(covering, new[] {new HashSet<int> {1, 2, 3}, new HashSet<int> {4, 5}});
        }
    }
}