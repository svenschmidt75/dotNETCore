#region

using System;
using NUnit.Framework;

#endregion

namespace GoogleProblem15
{
    /// <summary>
    ///     Given a sorted list of items (chars, ints, ...). One of the items has at least 25%
    ///     majority, i.e. >= 1/4 of all elements are the same.
    ///     Find the element.
    /// </summary>
    public class GoogleProblem15
    {
        public static int FindItem(int[] input)
        {
            // SS: O(1)
            
            if (input.Length < 4)
            {
                return -1;
            }

            var step = Math.Max(1, (input.Length + 7) / 8);

            var i = step;

            var prev = input[0];

            while (i < input.Length)
            {
                var val = input[i];

                if (prev == val)
                {
                    return val;
                }

                prev = val;
                i = Math.Min(i + step, input.Length - 1);
            }

            return -1;
        }
    }

    [TestFixture]
    public class GoogleProblem15Test
    {
        [Test]
        public void Test4()
        {
            // Arrange
            var input = new[] {1, 2, 2, 3};

            // Act
            var element = GoogleProblem15.FindItem(input);

            // Assert
            Assert.AreEqual(2, element);
        }

        [Test]
        public void Test8()
        {
            // Arrange
            var input = new[] {1, 2, 3, 4, 4, 4, 4, 8};

            // Act
            var element = GoogleProblem15.FindItem(input);

            // Assert
            Assert.AreEqual(4, element);
        }


        [Test]
        public void Test9()
        {
            // Arrange
            var input = new[] {1, 2, 3, 4, 4, 4, 4, 4, 9};

            // Act
            var element = GoogleProblem15.FindItem(input);

            // Assert
            Assert.AreEqual(4, element);
        }
    }
}