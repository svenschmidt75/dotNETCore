using System;
using Xunit;

namespace HashSet.UnitTest
{
    public class HashSetTest
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
        public void Remove_Existing()
        {
            // Arrange
            var hashSet = new HashSet<int>();
            hashSet.Add(1);

            // Act
            bool isRemoved = hashSet.Remove(1);

            // Assert
            Assert.True(isRemoved);
            Assert.False(hashSet.Contains(1));
            Assert.Equal(0, hashSet.Count);
        }

        [Fact]
        public void Remove_NonExisting()
        {
            // Arrange
            var hashSet = new HashSet<int>();

            // Act
            var isRemoved = hashSet.Remove(2);

            // Assert
            Assert.False(isRemoved);
        }

        [Fact]
        public void Clear()
        {
            // Arrange
            var hashSet = new HashSet<string>{"Test", "Test1"};

            // Act
            hashSet.Clear();

            // Assert
            Assert.Equal(0, hashSet.Count);
        }

    }
}