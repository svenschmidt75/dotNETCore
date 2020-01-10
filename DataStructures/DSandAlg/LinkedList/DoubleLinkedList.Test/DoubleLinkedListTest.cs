using NUnit.Framework;

namespace DoubleLinkedList.Test
{
    [TestFixture]
    public class DoubleLinkedListTest
    {
        [Test]
        public void Append()
        {
            // Arrange
            var dll = new DoublyLinkedList<int>();

            // Act
            dll.Append(2);
            dll.Append(12);

            // Assert
            var head = dll.At(0);
            var next = dll.At(1);

            Assert.AreEqual(2, head.Value);
            Assert.IsNull(head.Prev);
            Assert.AreSame(head.Next, next);

            Assert.AreEqual(12, next.Value);
            Assert.AreSame(next.Prev, head);
            Assert.IsNull(next.Next);
        }

        [Test]
        public void AppendFromEmpty()
        {
            // Arrange
            var dll = new DoublyLinkedList<int>();

            // Act
            dll.Append(2);

            // Assert
            var node = dll.At(0);
            Assert.AreEqual(2, node.Value);
            Assert.IsNull(node.Prev);
            Assert.IsNull(node.Next);
        }

        [Test]
        public void InsertAdjustTail()
        {
            // Arrange
            var dll = new DoublyLinkedList<int>();
            dll.Append(2);
            dll.Append(4);

            // Act
            dll.Insert(1, 6);
            dll.Append(8);

            // Assert
            var head = dll.At(0);
            var n1 = dll.At(1);
            var n2 = dll.At(2);
            var tail = dll.At(3);

            Assert.AreEqual(2, head.Value);
            Assert.IsNull(head.Prev);
            Assert.AreSame(head.Next, n1);

            Assert.AreEqual(4, n1.Value);
            Assert.AreSame(n1.Prev, head);
            Assert.AreSame(n1.Next, n2);

            Assert.AreEqual(6, n2.Value);
            Assert.AreSame(n2.Prev, n1);
            Assert.AreSame(n2.Next, tail);

            Assert.AreEqual(8, tail.Value);
            Assert.AreSame(tail.Prev, n2);
            Assert.IsNull(tail.Next);
        }

        [Test]
        public void InsertAfterHead()
        {
            // Arrange
            var dll = new DoublyLinkedList<int>();
            dll.Append(2);

            // Act
            dll.Insert(0, 12);

            // Assert
            var head = dll.At(0);
            var next = dll.At(1);

            Assert.AreEqual(2, head.Value);
            Assert.IsNull(head.Prev);
            Assert.AreSame(head.Next, next);

            Assert.AreEqual(12, next.Value);
            Assert.AreSame(next.Prev, head);
            Assert.IsNull(next.Next);
        }

        [Test]
        public void Prepend()
        {
            // Arrange
            var dll = new DoublyLinkedList<int>();

            // Act
            dll.Prepend(2);
            dll.Prepend(12);

            // Assert
            var head = dll.At(0);
            var next = dll.At(1);

            Assert.AreEqual(12, head.Value);
            Assert.IsNull(head.Prev);
            Assert.AreSame(head.Next, next);

            Assert.AreEqual(2, next.Value);
            Assert.AreSame(next.Prev, head);
            Assert.IsNull(next.Next);
        }

        [Test]
        public void PrependFromEmpty()
        {
            // Arrange
            var dll = new DoublyLinkedList<int>();

            // Act
            dll.Prepend(2);

            // Assert
            var node = dll.At(0);
            Assert.AreEqual(2, node.Value);
            Assert.IsNull(node.Prev);
            Assert.IsNull(node.Next);
        }

        [Test]
        public void Remove()
        {
            // Arrange
            var dll = new DoublyLinkedList<int>();
            dll.Append(2);
            dll.Append(4);
            dll.Append(6);
            dll.Append(8);

            // Act
            dll.Remove(1);

            // Assert
            var head = dll.At(0);
            var n1 = dll.At(1);
            var tail = dll.At(2);

            Assert.AreEqual(2, head.Value);
            Assert.IsNull(head.Prev);
            Assert.AreSame(head.Next, n1);

            Assert.AreEqual(6, n1.Value);
            Assert.AreSame(n1.Prev, head);
            Assert.AreSame(n1.Next, tail);

            Assert.AreEqual(8, tail.Value);
            Assert.AreSame(tail.Prev, n1);
            Assert.IsNull(tail.Next);
        }

        [Test]
        public void RemoveHead()
        {
            // Arrange
            var dll = new DoublyLinkedList<int>();
            dll.Append(2);
            dll.Append(4);
            dll.Append(6);
            dll.Append(8);

            // Act
            dll.Remove(0);

            // Assert
            var head = dll.At(0);
            var n1 = dll.At(1);
            var tail = dll.At(2);

            Assert.AreEqual(4, head.Value);
            Assert.IsNull(head.Prev);
            Assert.AreSame(head.Next, n1);

            Assert.AreEqual(6, n1.Value);
            Assert.AreSame(n1.Prev, head);
            Assert.AreSame(n1.Next, tail);

            Assert.AreEqual(8, tail.Value);
            Assert.AreSame(tail.Prev, n1);
            Assert.IsNull(tail.Next);
        }

        [Test]
        public void RemoveTail()
        {
            // Arrange
            var dll = new DoublyLinkedList<int>();
            dll.Append(2);
            dll.Append(4);
            dll.Append(6);
            dll.Append(8);

            // Act
            dll.Remove(3);

            // Assert
            var head = dll.At(0);
            var n1 = dll.At(1);
            var tail = dll.At(2);

            Assert.AreEqual(2, head.Value);
            Assert.IsNull(head.Prev);
            Assert.AreSame(head.Next, n1);

            Assert.AreEqual(4, n1.Value);
            Assert.AreSame(n1.Prev, head);
            Assert.AreSame(n1.Next, tail);

            Assert.AreEqual(6, tail.Value);
            Assert.AreSame(tail.Prev, n1);
            Assert.IsNull(tail.Next);
        }
    }
}