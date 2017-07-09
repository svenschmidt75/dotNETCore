using Xunit;

namespace SinglyLinkedList.Test
{
    public class UnitTest1
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
        
    }
}