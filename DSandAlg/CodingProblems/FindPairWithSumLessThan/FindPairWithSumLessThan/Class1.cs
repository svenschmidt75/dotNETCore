
// https://www.quora.com/What-can-I-expect-in-Amazons-SDE-online-assessment-Can-someone-who-has-taken-the-assessment-please-give-me-examples-of-the-types-of-questions-asked

/* The first question was given an unsorted array of floats and a value X,
 * find a pair of elements in that array having sum less than or equal to X.
 */

using System;
using System.Linq;
using NUnit.Framework;

namespace FindPairWithSumLessThan
{
    public static class Class1
    {
        public static (int min, int max) FindPair(float[] input, float x)
        {
            if (input.Length < 2)
            {
                return (-1, -1);
            }    

            // SS: sort at O(n log n)
            var array = input.Select((f, idx) => (idx, f)).OrderBy(t => t.f).ToArray();

            var j = 1;
            var e1 = array[0];
            while (j < input.Length)
            {
                var e2 = array[j];
                if (e1.f + e2.f < x)
                {
                    return (e1.idx, e2.idx);
                }
            }

            return (-1, -1);
        }
        
    }

    [TestFixture]
    public class Test
    {
        [Test]
        public void Test1()
        {
            // Arrange
            var rand = new Random();
            var input = Enumerable.Range(0, 20).Select(i => (float)rand.NextDouble() * 2 - 1).ToArray();
            
            // Act
            const float x = 0.5f;
            var (min, max) = Class1.FindPair(input, x);

            // Assert
            Assert.True(input[min] + input[max] <= x);
        }
    }
}