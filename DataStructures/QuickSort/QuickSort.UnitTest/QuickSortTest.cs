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

        [Fact]
        public void Test_Three()
        {
            // Arrange
            var a = new[] {19, 7, 1};

            // Act
            QuickSort.Sort(a);

            // Assert
            Assert.True(IsSorted(a));
        }

    }
}