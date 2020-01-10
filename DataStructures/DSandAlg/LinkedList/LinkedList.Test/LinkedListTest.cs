using NUnit.Framework;

namespace LinkedList.Test
{
    [TestFixture]
    public class LinkedListTest
    {
        [Test]
        public void Prepend()
        {
            // Arrange
            var linkedList = new LinkedList<int>();
            
            // Act
            linkedList.Prepend(10);

            // Assert
            var node = linkedList.At(0);
            Assert.AreEqual(10, node.Value);

        }
        
        [Test]
        public void Append()
        {
            // Arrange
            var linkedList = new LinkedList<int>();
            
            // Act
            linkedList.Append(10);
            linkedList.Append(2);

            // Assert
            var node = linkedList.At(1);
            Assert.AreEqual(2, node.Value);

        }
        
        [Test]
        public void Insert()
        {
            // Arrange
            var linkedList = new LinkedList<int>();
            linkedList.Append(10);
            linkedList.Append(2);
            
            // Act
            linkedList.Insert(1, 15);

            // Assert
            var node = linkedList.At(2);
            Assert.AreEqual(15, node.Value);

        }

        [Test]
        public void RemoveHead()
        {
            // Arrange
            var linkedList = new LinkedList<int>();
            linkedList.Append(10);
            linkedList.Append(2);
            
            // Act
            linkedList.Remove(0);

            // Assert
            var node = linkedList.At(0);
            Assert.AreEqual(2, node.Value);
        }

        [Test]
        public void RemoveTail()
        {
            // Arrange
            var linkedList = new LinkedList<int>();
            linkedList.Append(10);
            linkedList.Append(2);
            
            // Act
            linkedList.Remove(1);

            // Assert
            var node = linkedList.At(0);
            Assert.AreEqual(10, node.Value);
        }

    }
}