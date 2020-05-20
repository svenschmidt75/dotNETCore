using System.Linq;
using NUnit.Framework;

namespace DasHausVomNikolaus.Test
{
    [TestFixture]
    public class DasHausVomNikolausTest
    {
        [Test]
        public void TestNonRecursive()
        {
            // Arrange
            var solver = new Solver();

            // Act
            solver.FindSolutionsNonRecursive();

            // Assert
            Assert.AreEqual(2 * 44, solver.Solutions.Count);
            Assert.AreEqual(44, solver.Solutions.Count(s => s[0] == 0 && s[8] == 1));
            Assert.AreEqual(44, solver.Solutions.Count(s => s[0] == 1 && s[8] == 0));
        }

        [Test]
        public void TestRecursive()
        {
            // Arrange
            var solver = new Solver();

            // Act
            solver.RunRecursive();

            // Assert
            Assert.AreEqual(2 * 44, solver.Solutions.Count);
            Assert.AreEqual(44, solver.Solutions.Count(s => s[0] == 0 && s[8] == 1));
            Assert.AreEqual(44, solver.Solutions.Count(s => s[0] == 1 && s[8] == 0));
        }
    }
}