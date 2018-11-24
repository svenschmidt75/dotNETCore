using System;
using Xunit;
using Xunit.Abstractions;

namespace Matching.Test
{
    public class UnitTest1
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public UnitTest1(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void Test1()
        {
            // Arrange
            var source = new[]
            {
                new Match {Odom = 10, JL = 1,}, new Match {Odom = 20, JL = 2,}, new Match {Odom = 30, JL = 4,}
            };

            var target = new[]
            {
                new Match {Odom = 10, JL = 1,}, new Match {Odom = 20, JL = 2,}, new Match {Odom = 30, JL = 4,}
            };

            // Act
            Matching.Match(source, target, 0, 1, str => _testOutputHelper.WriteLine(str));

            // Assert
        }

        [Fact]
        public void Test2()
        {
            // Arrange
            var y1Rows = new[]
            {
                new Match {Odom = 0, JL = 0,}, new Match {Odom = 1.85, JL = 2,}, new Match {Odom = 2.292, JL = 2.992,}, new Match {Odom = 5.283, JL = 8.542,},
                new Match {Odom = 13.825, JL = 2.783,}, new Match {Odom = 16.608, JL = 38.583,}, new Match {Odom = 55.192, JL = 29.567,},
                new Match {Odom = 84.758, JL = 2.925,}, new Match {Odom = 87.683, JL = 2.925,}, new Match {Odom = 92.475, JL = 4.792,},
                new Match {Odom = 102.467, JL = 9.992}, new Match {Odom = 105.442, JL = 2.975}, new Match {Odom = 110.108, JL = 4.667},
                new Match {Odom = 129.450, JL = 19.342}, new Match {Odom = 133.233, JL = 3.783}, new Match {Odom = 138.033, JL = 4.800},
                new Match {Odom = 182.367, JL = 44.333}, new Match {Odom = 202.350, JL = 19.983}
            };

            var y2Rows = new[]
            {
                new Match {Odom = -55.88, JL = 0,}, new Match {Odom = -1.92, JL = 3.76,}, new Match {Odom = 1.85, JL = 12.79,}, new Match {Odom = 52.44, JL = 29.56,},
                new Match {Odom = 82.01, JL = 3.18,}, new Match {Odom = 85.19, JL = 4.56,}, new Match {Odom = 89.75, JL = 10.34,},
                new Match {Odom = 100.09, JL = 3.05,}, new Match {Odom = 103.14, JL = 4.7,}, new Match {Odom = 107.83, JL = 19.01,},
                new Match {Odom = 126.84, JL = 3.93,}, new Match {Odom = 130.78, JL = 4.69,}, new Match {Odom = 135.46, JL = 44.28,},
                new Match {Odom = 179.74, JL = 20.02,}, new Match {Odom = 199.76, JL = 5.41,}, new Match {Odom = 205.18, JL = 4.81,},
                new Match {Odom = 209.98, JL = 40.24,}
            };

            // Act
            Matching.Match(y2Rows, y1Rows, 0, 50, str => _testOutputHelper.WriteLine(str));

            // Assert
        }
    }
}