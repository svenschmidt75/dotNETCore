using System;
using NUnit.Framework;

// Problem:
// URL:

namespace LeetCode
{
    public class FixedPoint
    {
        private int _value;
        private const int FractionalBits = 16;

        public FixedPoint(double d)
        {
            _value = (int)(d * (1 << FractionalBits));
        }

        public FixedPoint() : this(0) {}

        public void Add(FixedPoint b)
        {
            _value += b._value;
        }

        public void Sub(FixedPoint b)
        {
            _value -= b._value;
        }

        public void Mul(FixedPoint b)
        {
            _value *= b._value;
            _value >>= FractionalBits;
        }

        public void Div(FixedPoint b)
        {
            const int tmpFrac = 8;
            
            // SS: temp. increase precision
            _value <<= tmpFrac;
            _value /= b._value;
            _value <<= FractionalBits - tmpFrac;
        }

        public float Float => (float)_value / (1 << FractionalBits);

        public override string ToString()
        {
            return Float.ToString();
        }

        [TestFixture]
        public class Tests
        {
            [TestCase(2.75f, 1.25f)]
            public void TestAdd(float f1, float f2)
            {
                // Arrange
                var fp1 = new FixedPoint(f1);
                var fp2 = new FixedPoint(f2);

                // Act
                fp1.Add(fp2);

                // Assert
                Assert.AreEqual(f1 + f2, fp1.Float);
            }

            [TestCase(2.75f, 1.25f)]
            public void TestMul(float f1, float f2)
            {
                // Arrange
                var fp1 = new FixedPoint(f1);
                var fp2 = new FixedPoint(f2);

                // Act
                fp1.Mul(fp2);

                // Assert
                Assert.AreEqual(f1 * f2, fp1.Float);
            }
            
            [TestCase(2.75f, 1.25f)]
            [TestCase(8.175f, 2.345f)]
            [TestCase(8f, 2f)]
            public void TestDiv(float f1, float f2)
            {
                // Arrange
                var fp1 = new FixedPoint(f1);
                var fp2 = new FixedPoint(f2);

                // Act
                fp1.Div(fp2);

                // Assert
                Assert.AreEqual(f1 / f2, fp1.Float);
            }

        }
    }
}