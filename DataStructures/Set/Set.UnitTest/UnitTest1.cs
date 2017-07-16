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
            Assert.Equal(unionData.Length, union1.Count);
            Assert.All(unionData, i => Assert.True(union2.Contains(i)));
            Assert.Equal(unionData.Length, union2.Count);
        }

        [Theory]
        [InlineData(new[] {1, 2, 3}, new[] {2}, new[] {1, 3})]
        [InlineData(new[] {1, 2, 3}, new[] {2, 6}, new[] {1, 3})]
        [InlineData(new[] {1, 2, 3}, new int[0], new[] {1, 2, 3})]
        [InlineData(new[] {1, 2, 3}, new[] {1, 2, 3}, new int[0])]
        public void Difference(int[] setData1, int[] setData2, int[] diffData)
        {
            // Arrange
            var set1 = new Set<int>();
            setData1.ForEach(set1.Add);
            var set2 = new Set<int>();
            setData2.ForEach(set2.Add);

            // Act
            var diff = set1.Difference(set2);

            // Assert
            Assert.All(diffData, i => Assert.True(diff.Contains(i)));
            Assert.Equal(diff.Count, diff.Count);
        }

        [Theory]
        [InlineData(new[] {1, 2, 3}, new[] {2}, new[] {2})]
        [InlineData(new[] {1, 2, 3}, new[] {2, 6}, new[] {2})]
        [InlineData(new[] {1, 2, 3}, new int[0], new int[0])]
        [InlineData(new[] {1, 2, 3}, new[] {1, 2, 3}, new[] {1, 2, 3})]
        public void Intersection(int[] setData1, int[] setData2, int[] intersectData)
        {
            // Arrange
            var set1 = new Set<int>();
            setData1.ForEach(set1.Add);
            var set2 = new Set<int>();
            setData2.ForEach(set2.Add);

            // Act
            var intersect1 = set1.Intersection(set2);
            var intersect2 = set2.Intersection(set1);

            // Assert
            Assert.All(intersectData, i => Assert.True(intersect1.Contains(i)));
            Assert.Equal(intersectData.Length, intersect1.Count);
            Assert.All(intersectData, i => Assert.True(intersect2.Contains(i)));
            Assert.Equal(intersectData.Length, intersect2.Count);
        }

        [Theory]
        [InlineData(new[] {1, 2, 3}, new[] {2}, false)]
        [InlineData(new[] {1, 2, 3}, new[] {1, 2, 3, 6}, true)]
        [InlineData(new[] {1, 2, 3}, new int[0], false)]
        [InlineData(new[] {2, 3}, new[] {1, 2, 3}, true)]
        [InlineData(new[] {1, 2, 3}, new[] {1, 2, 3}, true)]
        public void Subset(int[] setData1, int[] setData2, bool isSubsetExpected)
        {
            // Arrange
            var set1 = new Set<int>();
            setData1.ForEach(set1.Add);
            var set2 = new Set<int>();
            setData2.ForEach(set2.Add);

            // Act
            bool isSubset = set1.Subset(set2);

            // Assert
            Assert.Equal(isSubsetExpected, isSubset);
        }

        [Fact]
        public void SubsetInvariants()
        {
            // Arrange
            var set1 = new Set<int>{1, 2};
            var set2 = new Set<int>{1, 2, 3, 4, 5};

            // Act

            // Assert
            Assert.True(set1.Subset(set2));

            // A \ B = {0}
            Assert.True(!set1.Difference(set2).Any());

            // A intersect B = {A}
            Assert.All(set1.Intersect(set2), i => Assert.True(set1.Contains(i)));
        }

    }
}