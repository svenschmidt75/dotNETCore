using System;
using Xunit;

namespace Stack.UnitTest
{
    public class UnitTest
    {
        [Fact]
        public void Push()
        {
			// Arrange
			var stack = new Stack<int>();

            // Act
            stack.Push(10);

            // Assert
            Assert.True(stack.Pop() == 10);
        }

        [Fact]
        public void Pop()
        {
            // Arrange
            var stack = new Stack<int>();

            // Act
            stack.Push(10);
            stack.Push(11);

            // Assert
            Assert.True(stack.Pop() == 11);
        }

        [Fact]
        public void Pop_Throws_OnEmpty()
        {
            // Arrange
            var stack = new Stack<int>();

            // Act
            // Assert
            Assert.Throws<InvalidOperationException>(() => stack.Pop());
        }

    }
}