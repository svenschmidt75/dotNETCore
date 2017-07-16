using System;
using System.Linq;
using Xunit;

namespace Set.UnitTest
{
    public class UnitTest1
    {
        [Theory]
        [InlineData(1, true)]
        [InlineData(2, false)]
        public void Remove(int item, bool expected)
        {
            // Arrange
            var set = new Set<int>();

            // Act
            set.Add(1);

            // Assert
            Assert.Equal(expected, set.Remove(item));
        }

        [Theory]
        [InlineData(1, true)]
        [InlineData(2, false)]
        public void Contains(int item, bool expected)
        {
            // Arrange
            var set = new Set<int>();

            // Act
            set.Add(1);

            // Assert
            Assert.Equal(expected, set.Contains(item));
        }

        [Fact]
        public void Add_FromEmpty()
        {
            // Arrange
            var set = new Set<int>();

            // Act
            set.Add(1);

            // Assert
            Assert.Equal(1, set.Count);
        }

        [Fact]
        public void Clear()
        {
            // Arrange
            var set = new Set<int> {1};

            // Act
            set.Clear();

            // Assert
            Assert.Equal(0, set.Count);
        }

        [Theory]
        [InlineData(new[] {1, 2, 3}, new[] {2}, new[] {1, 2, 3})]
        [InlineData(new[] {1, 2, 3}, new[] {2, 6}, new[] {1, 2, 3, 6})]
        [InlineData(new[] {1, 2, 3}, new int[0], new[] {1, 2, 3})]
        public void Union(int[] setData1, int[] setData2, int[] unionData)
        {
            // Arrange
            var set1 = new Set<int>();
            setData1.ForEach(set1.Add);
            var set2 = new Set<int>();
            setData2.ForEach(set2.Add);

            // Act
            var union1 = set1.Union(set2);
            var union2 = set2.Union(set1);

            // Assert
            Assert.All(unionData, i => Assert.True(union1.Contains(i)));
        }
    }
}