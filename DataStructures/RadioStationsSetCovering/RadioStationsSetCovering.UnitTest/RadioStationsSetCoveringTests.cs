using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace RadioStationsSetCovering.UnitTest
{
    public class RadioStationsSetCoveringTests
    {
        [Fact]
        public void EmptySet()
        {
            // Arrange
            var set = new Dictionary<int, HashSet<int>>();

            // Act
            var covering = SetCovering.CreatePowerSet(set);

            // Assert
            Assert.Equal(covering, new List<HashSet<int>> {SetCovering.EmptySet});
        }

        [Fact]
        public void SingleSet()
        {
            // Arrange
            var set = new Dictionary<int, HashSet<int>>
            {
                {0, new HashSet<int> {1, 2, 3}},
            };

            // Act
            var covering = SetCovering.CreatePowerSet(set);

            // Assert
            Assert.Equal(covering, new List<HashSet<int>> {SetCovering.EmptySet, new HashSet<int> {0}});
        }

        [Fact]
        public void SetWith2Items()
        {
            // Arrange
            var set = new Dictionary<int, HashSet<int>>
            {
                {0, new HashSet<int> {1, 2, 3}},
                {3, new HashSet<int> {4, 5}},
            };

            // Act
            var covering = SetCovering.CreatePowerSet(set);

            // Assert
            Assert.Equal(covering,
                         new List<HashSet<int>>
                         {
                             SetCovering.EmptySet,
                             new HashSet<int> {0},
                             new HashSet<int> {3},
                             new HashSet<int> {0, 3}
                         });
        }

        [Fact]
        public void SetWith3Items()
        {
            // Arrange
            var set = new Dictionary<int, HashSet<int>>
            {
                {0, new HashSet<int> {1, 2, 3}},
                {1, new HashSet<int> {2, 4}},
                {3, new HashSet<int> {4, 5}},
            };

            // Act
            var covering = SetCovering.CreatePowerSet(set);

            // Assert
            Assert.Equal(covering,
                         new List<HashSet<int>>
                         {
                             SetCovering.EmptySet,
                             new HashSet<int> {0},
                             new HashSet<int> {1},
                             new HashSet<int> {0, 1},
                             new HashSet<int> {3},
                             new HashSet<int> {0, 3},
                             new HashSet<int> {1, 3},
                             new HashSet<int> {0, 1, 3}
                         });
        }

        [Fact]
        public void SetWith4Items()
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
            Assert.True(covering.SetEquals(new HashSet<int> {0, 3}));
        }
    }
}