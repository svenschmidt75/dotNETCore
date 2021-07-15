
using System;
using LeetCode;
using NUnit.Framework;

namespace JohnsProblem
{
    public class Solution2
    {
        public int Solve(double[] people)
        {
            // SS: runtime complexity: O(n log n)
            // space complexity: O(n)

            // SS: calculate the average everybody should pay
            double avg = 0;
            for (int p = 0; p < people.Length; p++)
            {
                avg += people[p];
            }
            avg /= people.Length;

            // SS: O(n log n)
            var payerHeap = BinaryHeap<double>.CreateMaxHeap();
            var payeeHeap = BinaryHeap<double>.CreateMaxHeap();
            for (int p = 0; p < people.Length; p++)
            {
                double d = avg - people[p];
                if (d < 0)
                {
                    // SS: this person payed more than the avg, so it will receive money
                    payeeHeap.Push(-d);
                }
                else if (d > 0)
                {
                    // SS: this person payed less than the avg, so it will pay money
                    payerHeap.Push(d);
                }

                // SS: no need to push on heap if people[p] == avg
            }

            int nChecks = 0;

            // SS: runtime complexity of loop: depends on the specifics of the values,
            // but there is no dependency on the size of the input array, so O(n log n)
            // avg.

            // SS: find the person who owes the most. Then, pay the person who is owed
            // the most, etc. If there is money left over, find the next payee, as the
            // money needs to be spent no matter what.
            while (payeeHeap.IsEmpty == false)
            {
                double amountToSpend = payerHeap.Pop();
                while (Math.Abs(amountToSpend) > 1E-3)
                {
                    nChecks++;

                    double amountToReceive = payeeHeap.Pop();
                    if (amountToSpend - amountToReceive < 0)
                    {
                        // SS: payee cannot be payed completely
                        payeeHeap.Push(amountToReceive - amountToSpend);
                    }

                    amountToSpend = Math.Max(0, amountToSpend - amountToReceive);
                }
            }

            return nChecks;
        }

        [TestFixture]
        public class Tests
        {
            [TestCase(new[] { 5.0, 7, 13, 2, 15 }, 4)]
            [TestCase(new[] { 5.0, 5, 5, 5, 5 }, 0)]
            [TestCase(new[] { 5.0 }, 0)]
            public void Test(double[] people, int expectedResult)
            {
                // Arrange

                // Act
                int nChecks = new Solution2().Solve(people);

                // Assert
                Assert.AreEqual(nChecks, expectedResult);
            }

        }
    }
}
