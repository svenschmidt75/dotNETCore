using System;
using System.Text.RegularExpressions;
using NUnit.Framework;

namespace Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            // Arrange
            var y1Rows = new[]
            {
                new Matching.Match {Odom = 0, JL = 0,}, new Matching.Match {Odom = 1.85, JL = 2,}, new Matching.Match {Odom = 2.292, JL = 2.992,}, new Matching.Match {Odom = 5.283, JL = 8.542,},
                new Matching.Match {Odom = 13.825, JL = 2.783,}, new Matching.Match {Odom = 16.608, JL = 38.583,}, new Matching.Match {Odom = 55.192, JL = 29.567,},
                new Matching.Match {Odom = 84.758, JL = 2.925,}, new Matching.Match {Odom = 87.683, JL = 2.925,}, new Matching.Match {Odom = 92.475, JL = 4.792,},
                new Matching.Match {Odom = 102.467, JL = 9.992}, new Matching.Match {Odom = 105.442, JL = 2.975}, new Matching.Match {Odom = 110.108, JL = 4.667},
                new Matching.Match {Odom = 129.450, JL = 19.342}, new Matching.Match {Odom = 133.233, JL = 3.783}, new Matching.Match {Odom = 138.033, JL = 4.800},
                new Matching.Match {Odom = 182.367, JL = 44.333}, new Matching.Match {Odom = 202.350, JL = 19.983}
            };

            var y2Rows = new[]
            {
                new Matching.Match {Odom = -55.88, JL = 0,}, new Matching.Match {Odom = -1.92, JL = 3.76,}, new Matching.Match {Odom = 1.85, JL = 12.79,}, new Matching.Match {Odom = 52.44, JL = 29.56,},
                new Matching.Match {Odom = 82.01, JL = 3.18,}, new Matching.Match {Odom = 85.19, JL = 4.56,}, new Matching.Match {Odom = 89.75, JL = 10.34,},
                new Matching.Match {Odom = 100.09, JL = 3.05,}, new Matching.Match {Odom = 103.14, JL = 4.7,}, new Matching.Match {Odom = 107.83, JL = 19.01,},
                new Matching.Match {Odom = 126.84, JL = 3.93,}, new Matching.Match {Odom = 130.78, JL = 4.69,}, new Matching.Match {Odom = 135.46, JL = 44.28,},
                new Matching.Match {Odom = 179.74, JL = 20.02,}, new Matching.Match {Odom = 199.76, JL = 5.41,}, new Matching.Match {Odom = 205.18, JL = 4.81,},
                new Matching.Match {Odom = 209.98, JL = 40.24,}
            };
            var matching = new Matching.Matching();

            // Act
            matching.Match(y2Rows, y1Rows, 0, 50, str => Console.WriteLine(str));

            // Assert
        }
    }
}