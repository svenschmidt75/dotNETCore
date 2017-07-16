using Xunit;

namespace Set.UnitTest
{
    public class UnitTest1
    {
        [Fact]
        public void Contains()
        {
            // Arrange
            var set = new Set<int>();

            // Act
            set.Add(1);

            // Assert
            Assert.True(set.Contains(1));
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