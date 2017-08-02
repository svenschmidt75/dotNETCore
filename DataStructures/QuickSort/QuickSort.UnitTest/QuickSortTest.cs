using System;
using System.Linq;
using Xunit;

namespace QuickSort.UnitTest
{
    public class QuickSortTest
    {
        private static bool IsSorted<T>(T[] sorted) where T : IComparable<T>
        {
            return sorted.Zip(sorted.Skip(1), (arg1, arg2) => new {Item1 = arg1, Item2 = arg2}).
                          All(arg => arg.Item1.CompareTo(arg.Item2) < 1);
        }

        [Fact]
        public void Test_Empty()
        {
            // Arrange
            var a = new int[0];

            // Act
            QuickSort.Sort(a);

            // Assert
            Assert.True(IsSorted(a));
        }

        [Fact]
        public void Test_Single()
        {
            // Arrange
            var a = new[] {1};

            // Act
            QuickSort.Sort(a);

            // Assert
            Assert.True(IsSorted(a));
        }

        [Fact]
        public void Test_Two()
        {
            // Arrange
            var a = new[] {1, 0};

            // Act
            QuickSort.Sort(a);

            // Assert
            Assert.True(IsSorted(a));
        }

        [Theory]
        [InlineData(new[]{1, 7, 19})]
        [InlineData(new[]{1, 19, 7})]
        [InlineData(new[]{7, 1, 19})]
        [InlineData(new[]{7, 19, 1})]
        [InlineData(new[]{19, 7, 1})]
        [InlineData(new[]{19, 1, 7})]
        public void Test_Three(int[] input)
        {
            // Arrange

            // Act
            QuickSort.Sort(input);

            // Assert
            Assert.True(IsSorted(input));
        }

        [Theory]
        [InlineData(new[]{191, 85, 6, 98, 534, 876, -19, 1, 7, 19})]
        public void Test_Many(int[] input)
        {
            // Arrange

            // Act
            QuickSort.Sort(input);

            // Assert
            Assert.True(IsSorted(input));
        }

        [Theory]
        [InlineData(new[]{191, 191, 85, 6, 98, 534, 876, -19, 1, 7, 19})]
        public void Test_WithTwoPivots(int[] input)
        {
            // Arrange

            // Act
            QuickSort.Sort(input);

            // Assert
            Assert.True(IsSorted(input));
        }

    }
}