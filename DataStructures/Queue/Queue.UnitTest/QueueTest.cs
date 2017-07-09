using System;
using System.Linq;
using Xunit;

namespace Queue.UnitTest
{
    public class QueueTest
    {
        [Theory]
        [InlineData(new[] {10, 10, 11})]
        [InlineData(new[] {13, 13, 11, 11})]
        public void Enqueue(int[] values)
        {
            // Arrange
            var queue = new Queue<int>();

            // Act
            foreach (var value in values.Skip(1))
            {
                queue.Enqueue(value);
            }

            // Assert
            Assert.True(queue.Dequeue() == values.First());
        }

        [Fact]
        public void Dequeue_Throws_OnEmpty()
        {
            // Arrange
            var queue = new Queue<int>();

            // Act
            // Arrange
            Assert.Throws<InvalidOperationException>(() => queue.Dequeue());
        }

    }
}