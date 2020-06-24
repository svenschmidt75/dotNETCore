using System;
using NUnit.Framework;

namespace GCD
{
    public static class GCD
    {
        public static int Gcd(double a, double b)
        {
            var r1 = Math.Min(a, b);
            var r2 = Math.Max(a, b);

            while (r2 > 0 && r1 > 0)
            {
                var r3 = r2 % r1;
                r2 = r1;
                r1 = r3;
            }

            return (int)r2;
        }
    }

    [TestFixture]
    public class Tests
    {
        [Test]
        public void Test1()
        {
            // Arrange
            
            // Act
            var gcd = GCD.Gcd(1, 1);
            
            // Assert
            Assert.AreEqual(1, gcd);
        }

        [Test]
        public void Test2()
        {
            // Arrange
            
            // Act
            var gcd = GCD.Gcd(6, 3);
            
            // Assert
            Assert.AreEqual(3, gcd);
        }
        
        [Test]
        public void Test3()
        {
            // Arrange
            
            // Act
            var gcd = GCD.Gcd(232, 560);
            
            // Assert
            Assert.AreEqual(8, gcd);
        }

        [Test]
        public void Test4()
        {
            // Arrange
            
            // Act
            var gcd = GCD.Gcd(29, 70);
            
            // Assert
            Assert.AreEqual(1, gcd);
        }

        [Test]
        public void Test5()
        {
            // Arrange
            
            // Act
            var gcd = GCD.Gcd(0, 70);
            
            // Assert
            Assert.AreEqual(70, gcd);
        }

        [Test]
        public void Test6()
        {
            // Arrange
            
            // Act
            var gcd = GCD.Gcd(29, 0);
            
            // Assert
            Assert.AreEqual(29, gcd);
        }

        [Test]
        public void Test7()
        {
            // Arrange
            
            // Act
            var gcd = GCD.Gcd(270, 192);
            
            // Assert
            Assert.AreEqual(6, gcd);
        }

        
    }
    
}