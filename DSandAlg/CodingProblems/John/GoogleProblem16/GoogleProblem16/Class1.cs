#region

using System.Collections.Generic;
using NUnit.Framework;

#endregion

namespace GoogleProblem16
{
    /// <summary>
    ///     You are given a sequence of boxes, each with a number > 0 of pens in it.
    ///     Given k > 0, choose two non-overlapping intervals of boxes such that
    ///     1. for each interval, the # of pens equals k
    ///     2. the total # of boxes is as small as possible
    /// </summary>
    public class GoogleProblem16
    {
        public static List<(int min, int max)> Find(int[] boxes, int k)
        {
            // SS: sliding window technique, runtime complexity is O(n)

            var result = new List<(int min, int max)>();

            if (boxes.Length < 2 || k == 0)
            {
                return result;
            }

            (int min, int max) i1 = (-1, -1);
            (int min, int max) i2 = (-1, -1);

            var i = 0;
            var j = 0;

            var sum = 0;

            while (i < boxes.Length && j < boxes.Length)
            {
                var pens = boxes[j];
                if (sum + pens < k)
                {
                    // SS: expend window to right
                    sum += pens;
                    j += 1;
                }
                else if (sum + pens > k)
                {
                    // SS: shrink window from left
                    var p = boxes[i];
                    sum -= p;
                    i += 1;
                }
                else
                {
                    // SS: found boxes with k pens
                    var width = j - i + 1;

                    var i1Width = i1.max - i1.min;
                    var i2Width = i2.max - i2.min;

                    if (width < i1Width || i1.min == -1)
                    {
                        i2 = i1;
                        i1 = (i, j + 1);
                    }
                    else if (width < i2Width || i2.min == -1)
                    {
                        i2 = (i, j + 1);
                    }

                    // SS: non-overlapping intervals only
                    i = j + 1;
                    j = i;
                    sum = 0;
                }
            }

            if (i1.min != -1)
            {
                result.Add(i1);
            }

            if (i2.min != -1)
            {
                result.Add(i2);
            }

            return result;
        }
    }

    [TestFixture]
    public class GoogleProblem16Test
    {
        [Test]
        public void NoBoxes()
        {
            // Arrange
            var boxes = new int[0];

            // Act
            var results = GoogleProblem16.Find(boxes, 5);

            // Assert
            Assert.AreEqual(0, results.Count);
        }

        [Test]
        public void NoPens()
        {
            // Arrange
            var boxes = new[] {1, 1, 2, 2, 1, 3, 4, 1, 7, 5};

            // Act
            var results = GoogleProblem16.Find(boxes, 0);

            // Assert
            Assert.AreEqual(0, results.Count);
        }

        [Test]
        public void Test1()
        {
            // Arrange
            var boxes = new[] {1, 1, 2, 2, 1, 3, 4, 1, 7, 5};

            // Act
            var results = GoogleProblem16.Find(boxes, 5);

            // Assert
            Assert.AreEqual((9, 10), results[0]);
            Assert.AreEqual((6, 8), results[1]);
        }
    }
}