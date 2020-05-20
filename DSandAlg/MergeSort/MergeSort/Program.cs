using System;
using NUnit.Framework;

namespace MergeSort
{
    public static class MergeSort
    {
        public static void Merge(double[] arr, int s1, int s2, int s3)
        {
            // s1: start index of left half
            // s2: end index of right half (inclusive)
            // s3: end index of right half
            // Note: s2 + 1 is start index of right half
            var i = s1;
            var j = s2 + 1;

            while (i < s3)
            {
                if (arr[i] > arr[j])
                {
                    // swap
                    var tmp = arr[i];
                    arr[i] = arr[j];
                    arr[j] = tmp;
                    i++;
                    continue;
                }

                if (i == j)
                {
                    if (i < s3)
                    {
                        j++;
                        continue;
                    }

                    // done, is sorted
                    break;
                }

                // correct order, advance both
                i++;
                j++;
            }
        }

    }

    [TestFixture]
    public class Test
    {
        [Test]
        public void Test1()
        {
            // Arrange
            var arr = new[] { 27.0, 38.0, 3.0, 43.0};

            // Act
            MergeSort.Merge(arr, 0, 1, 3);

            // Assert
            CollectionAssert.AreEqual(new[] { 3.0, 27.0, 38.0, 43.0 }, arr);
        }

        [Test]
        public void Test2()
        {
            // Arrange
            var arr = new[] { 27.0, 49.0, 3.0, 43.0 };

            // Act
            MergeSort.Merge(arr, 0, 1, 3);

            // Assert
            CollectionAssert.AreEqual(new[] { 3.0, 27.0, 43.0, 49.0 }, arr);
        }

    }
}
