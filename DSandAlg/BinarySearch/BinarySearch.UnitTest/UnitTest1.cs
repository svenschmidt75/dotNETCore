using System;
using System.Linq;
using Xunit;

namespace BinarySearch.UnitTest
{
    public class UnitTest1
    {
        [Fact]
        public void EmptyArray()
        {
            // Arrange
            var array = new int[0];

            // Act
            var (found, _) = BinarySearch.Search(array, 0);

            // Assert
            Assert.False(found);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(45)]
        [InlineData(19)]
        public void Exists(int item)
        {
            // Arrange
            var array = new int[] {0, 1, 6, 9, 19, 45};

            // Act
            var (found, _) = BinarySearch.Search(array, item);

            // Assert
            Assert.True(found);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(46)]
        [InlineData(10)]
        public void DoesNotExists(int item)
        {
            // Arrange
            var array = new int[] {0, 1, 6, 9, 19, 45};

            // Act
            var (found, _) = BinarySearch.Search(array, item);

            // Assert
            Assert.False(found);
        }

        [Theory]
        [InlineData(128, 0, 7)]
        [InlineData(512, 0, 9)]
        [InlineData(2048, 0, 11)]
        public void MaxNumberOfOperations(int length, int item, int maxSteps)
        {
            // Arrange
            var array = Enumerable.Range(0, length).ToArray();

            // Act
            var (found, n) = BinarySearch.Search(array, item);

            Console.WriteLine(n);

            // Assert
            Assert.True(n <= maxSteps);
        }

    }
}