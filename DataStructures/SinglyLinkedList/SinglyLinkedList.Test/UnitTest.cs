using Xunit;

namespace SinglyLinkedList.Test
{
    public class UnitTest
    {
        [Fact]
        public void Create()
        {
            // Arrange
            
            // Act
            var list = new SinglyLinkedList<int>(1);
            
            // Assert
            Assert.NotNull(list);
        }

        [Fact]
        public void Add()
        {
            // Arrange
            var list = new SinglyLinkedList<int>(1);

            // Act
            var newList = list.Cons(2);

            // Assert
            Assert.Null(list.Next);
            Assert.Same(newList.Next, list);
        }

        [Fact]
        public void Find_Existing()
        {
            // Arrange
            var list = new SinglyLinkedList<int>(1).Cons(2);

            // Act
            var (found, item) = list.Find(2);

            // Assert
            Assert.True(found);
            Assert.NotNull(item);
        }
        
        [Fact]
        public void Find_NonExisting()
        {
            // Arrange
            var list = new SinglyLinkedList<int>(1).Cons(2);

            // Act
            var (found, item) = list.Find(3);

            // Assert
            Assert.False(found);
        }
        
        [Fact]
        public void Remove_Existing_FromHead()
        {
            // Arrange
            var list = new SinglyLinkedList<int>(1).Cons(2);

            // Act
            var item = list.Remove(2);

            // Assert
            Assert.True(item.Value == 1);
        }
        
        [Fact]
        public void Remove_Existing_FromTail()
        {
            // Arrange
            var list = new SinglyLinkedList<int>(1).Cons(2);

            // Act
            var item = list.Remove(1);

            // Assert
            Assert.True(item.Value == 2);
            Assert.Same(list, item);
        }
        
        [Fact]
        public void Remove_Existing()
        {
            // Arrange
            var list = new SinglyLinkedList<int>(1).Cons(2).Cons(7);

            // Act
            var item = list.Remove(2);

            // Assert
            Assert.True(item.Value == 7);
        }

        [Fact]
        public void Remove_NonExisting()
        {
            // Arrange
            var list = new SinglyLinkedList<int>(1).Cons(2);

            // Act
            var item = list.Remove(3);

            // Assert
            Assert.True(item.Value == 2);
        }

        // Length
        
        
    }
}