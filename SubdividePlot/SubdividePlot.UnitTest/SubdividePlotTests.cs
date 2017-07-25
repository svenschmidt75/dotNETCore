using System;
using Xunit;

namespace SubdividePlot.UnitTest
{
    public class SubdividePlotTests
    {
        [Fact]
        public void Test1()
        {
            // Arrange
            int width = 1680;
            int height = 640;

            // Act
            int length = SubdividePlot.Subdivide(width, height);

            // Assert
            Assert.Equal(80, length);
        }
    }
}