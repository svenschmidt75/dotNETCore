using System;
using System.IO;
using Xunit;

namespace MazeSolver.UnitTest
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            // Act

            Console.WriteLine($"{Directory.GetCurrentDirectory()}");

            var builder = ImageBasedMaze.Create("../../../../images/Computerphile.jpg");

            // Arrange

            // Assert
        }
    }
}