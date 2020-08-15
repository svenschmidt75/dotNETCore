#region

using System.Collections.Generic;
using NUnit.Framework;

#endregion

namespace GoogleProblem30
{
    public class Solution
    {
        public int[] Solve(int[] input)
        {
            if (input.Length == 0)
            {
                return input;
            }

            var output = new List<int>();

            var carry = 1;

            for (var i = input.Length - 1; i >= 0; i--)
            {
                var digit = input[i];
                if (carry > 0)
                {
                    digit += 1;
                    carry = 0;
                }

                if (digit > 9)
                {
                    digit = 0;
                    carry = 1;
                }

                output.Add(digit);
            }

            if (carry > 0)
            {
                output.Add(1);
            }

            output.Reverse();

            return output.ToArray();
        }

        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1()
            {
                // Arrange
                int[] input = {2, 7, 8, 9};

                // Act
                var output = new Solution().Solve(input);

                // Assert
                CollectionAssert.AreEqual(new[] {2, 7, 9, 0}, output);
            }

            [Test]
            public void Test2()
            {
                // Arrange
                int[] input = {9, 9, 9, 9};

                // Act
                var output = new Solution().Solve(input);

                // Assert
                CollectionAssert.AreEqual(new[] {1, 0, 0, 0, 0}, output);
            }
        }
    }
}