using NUnit.Framework;

namespace Tests
{
    public class Tests
    {
        [Test]
        public void Test()
        {
            // Arrange
            // Act
            var probabilityDistribution = RadiusMonteCarlo.RadiusMonteCarlo.Run(100, 10);

            // Assert
            var probability = probabilityDistribution.Probability(0.1);
        }
    }
}