using System;
using Xunit;

namespace Matching.Test
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            // Arrange
            var source = new[]
            {
                new Match {Odometer = 10, JointLength = 1,}
                , new Match {Odometer = 20, JointLength = 2,}
                , new Match {Odometer = 30, JointLength = 4,}
            };

            var target = new[]
            {
                new Match {Odometer = 10, JointLength = 1,}
                , new Match {Odometer = 20, JointLength = 2,}
                , new Match {Odometer = 30, JointLength = 4,}
            };

            // Act
            Matching.Match(source, target, 0, 1);

            // Assert
        }
    }
}