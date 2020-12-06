#region

using NUnit.Framework;

#endregion

namespace SuffixArray.Test
{
    [TestFixture]
    public class SuffixArrayTests
    {
        [Test]
        public void Create()
        {
            // Arrange
            var text = "camel";

            // Act
            var sa = new SuffixArray(text);

            // Assert
            CollectionAssert.AreEqual(new[] {1, 0, 3, 4, 2}, sa.SA);
        }

        [TestCase(0, 1)]
        [TestCase(1, 0)]
        [TestCase(2, 4)]
        [TestCase(3, 2)]
        [TestCase(4, 3)]
        public void LowerBound(int idx, int expectedIdx)
        {
            // Arrange
            var sa = new SuffixArray("camel");
            var text = "camel".Substring(idx);

            // Act
            var resultIdx = sa.LowerBound(text);

            // Assert
            Assert.AreEqual(expectedIdx, resultIdx);
        }

        [TestCase(0, 1)]
        [TestCase(1, 0)]
        [TestCase(2, 4)]
        [TestCase(3, 2)]
        [TestCase(4, 3)]
        public void UpperBound(int idx, int expectedIdx)
        {
            // Arrange
            var sa = new SuffixArray("camel");
            var text = "camel".Substring(idx);

            // Act
            var resultIdx = sa.UpperBound(text);

            // Assert
            Assert.AreEqual(expectedIdx, resultIdx);
        }

        [Test]
        public void LowerBound2()
        {
            // Arrange
            var sa = new SuffixArray("ABABBAB");

            // Act
            var resultIdx = sa.LowerBound("B");

            // Assert
            Assert.AreEqual(3, resultIdx);
        }

        [Test]
        public void UpperBound2()
        {
            // Arrange
            var sa = new SuffixArray("ABABBAB");

            // Act
            var resultIdx = sa.UpperBound("B");

            // Assert
            Assert.AreEqual(3, resultIdx);
        }

        [Test]
        public void LongestCommonPrefix()
        {
            // Arrange
            var input = "ABABBAB";
            var sa = new SuffixArray(input);

            // Act
            var lcp = sa.CreateLCPArray();

            // Assert
            CollectionAssert.AreEqual(new[] {0, 2, 2, 0, 1, 3, 1}, lcp);
        }
    }
}