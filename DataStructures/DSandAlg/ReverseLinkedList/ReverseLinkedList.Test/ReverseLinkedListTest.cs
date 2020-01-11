using NUnit.Framework;

namespace ReverseLinkedList.Test
{
    [TestFixture]
    public class ReverseLinkedListTest
    {
        [Test]
        public void ReverseRecursive()
        {
            // Arrange
            var linkedList = new LinkedList.LinkedList<int>();
            linkedList.Append(1);
            linkedList.Append(10);
            linkedList.Append(16);
            linkedList.Append(99);
            linkedList.Append(88);
            
            // Act
            var reverse = linkedList.ReverseLinkedList();

            // Assert
        }

        [Test]
        public void ReverseNonRecursive()
        {
            // Arrange
            var linkedList = new LinkedList.LinkedList<int>();
            linkedList.Append(1);
            linkedList.Append(10);
            linkedList.Append(16);
            linkedList.Append(99);
            linkedList.Append(88);
            
            // Act
            linkedList.ReverseNonRecursion();

            // Assert
        }
    }
}