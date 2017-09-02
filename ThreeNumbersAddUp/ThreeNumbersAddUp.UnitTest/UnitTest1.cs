using System.Linq;
using Xunit;

namespace ThreeNumbersAddUp.UnitTest
{
    public class UnitTest1
    {
        // Test5: a1 + a2 + a3 > value
        // Test6: a1 + a2 + a3 = value
        // Test7: a1 + a2 + a3 < value

        [Fact]
        public void a1_bigger_than_value()
        {
            // Arrange
            var numbers = new[] {1, 2, 5, 6};

            // Act
            var solution = Class1.ThreeNumbers(numbers, 4);

            // Assert
            Assert.False(solution.Any());
        }

        [Fact]
        public void a1_equals_value()
        {
            // Arrange
            var numbers = new[] {1, 2, 5, 6};

            // Act
            var solution = Class1.ThreeNumbers(numbers, 2);

            // Assert
            Assert.False(solution.Any());
        }

        [Fact]
        public void a1_plus_a2_bigger_than_value()
        {
            // Arrange
            var numbers = new[] {1, 2, 5, 6};

            // Act
            var solution = Class1.ThreeNumbers(numbers, 4);

            // Assert
            Assert.False(solution.Any());
        }

        [Fact]
        public void a1_plus_a2_equals_value()
        {
            // Arrange
            var numbers = new[] {1, 2, 5, 6};

            // Act
            var solution = Class1.ThreeNumbers(numbers, 3);

            // Assert
            Assert.False(solution.Any());
        }

        [Fact]
        public void a1_plus_a2_plus_a3_bigger_than_value()
        {
            // Arrange
            var numbers = new[] {1, 2, 5, 6};

            // Act
            var solution = Class1.ThreeNumbers(numbers, 7);

            // Assert
            Assert.False(solution.Any());
        }

        [Theory]
        [InlineData(8, new[] {1, 2, 5})]
        [InlineData(13, new[] {2, 5, 6})]
        public void a1_plus_a2_plus_a3_equals_value(int value, int[] vs)
        {
            // Arrange
            var numbers = new[] {1, 2, 5, 6};

            // Act
            var solution = Class1.ThreeNumbers(numbers, value);

            // Assert
            Assert.Equal(3, solution.Count());
            Assert.Equal(solution, vs);
        }

        [Theory]
        [InlineData(10, new[] {1, 2, 7})]
        [InlineData(11, new[] {1, 6, 4})]
        [InlineData(18, new[] {5, 6, 7})]
        public void a1_plus_a2_plus_a3_equals_value_complex(int value, int[] vs)
        {
            // Arrange
            var numbers = new[] {1, 2, 5, 6, 3, 4, 7};

            // Act
            var solution = Class1.ThreeNumbers(numbers, value);

            // Assert
            Assert.Equal(3, solution.Count());
            Assert.Equal(solution, vs);
        }
    }
}