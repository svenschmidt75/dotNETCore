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
    }
}