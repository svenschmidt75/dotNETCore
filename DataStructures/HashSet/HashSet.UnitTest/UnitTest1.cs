using System;
using Xunit;

namespace HashSet.UnitTest
{
    public class UnitTest1
    {
        [Fact]
        public void Add_From_Empty()
        {
            // Arrange
            var hashSet = new HashSet<int>();

            // Act
            hashSet.Add(1);

            // Assert
            Assert.True(hashSet.Contains(1));
            Assert.Equal(1, hashSet.Count);
        }

        [Fact]
        public void Add_Unique()
        {
            // Arrange
            var hashSet = new HashSet<int>();

            // Act
            hashSet.Add(1);
            hashSet.Add(1);

            // Assert
            Assert.True(hashSet.Contains(1));
        }

        [Fact]
        public void Remove()
        {
            // Arrange
            var hashSet = new HashSet<int>();
            hashSet.Add(1);

            // Act
            hashSet.Remove(1);

            // Assert
            Assert.False(hashSet.Contains(1));
            Assert.Equal(0, hashSet.Count);
        }

    }
}