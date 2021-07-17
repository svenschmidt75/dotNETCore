
using System;
using LeetCode;
using NUnit.Framework;

namespace JohnsProblem
{
    public class Solution2
    {
        public int Solve(double[] people)
        {
            // return SolveGreedily(people);
            return SolveBruteForce(people);
        }

        private int SolveBruteForce(double[] people)
        {
            // SS: brute-force solution, i.e. calculate all solutions
            // and filter (i.e. take the one with the min number of
            // checks)
            // runtime complexity: O(n!)

            // SS: shift the average to be 0
            double avg = 0;
            for (int i = 0; i < people.Length; i++)
            {
                avg += people[i];
            }
            avg /= people.Length;
            
            for (int i = 0; i < people.Length; i++)
            {
                people[i] -= avg;
                
                // SS: when close to 0...
                if (Math.Abs(people[i]) < 1E-10)
                {
                    people[i] = 0;
                }
            }
            
            int nZeros = 0;
            
            int Solve(int state, int idx)
            {
                // SS: base case
                if (nZeros == people.Length)
                {
                    return 0;
                }

                int minChecks = int.MaxValue;

                if (state == 0)
                {
                    // SS: find new unmarked
                    for (int i = 0; i < people.Length; i++)
                    {
                        if (people[i] == 0)
                        {
                            continue;
                        }

                        int nChecks = Solve(1, i);
                        minChecks = Math.Min(minChecks, nChecks);
                    }
                }
                else
                {
                    // SS: 
                    for (int i = 0; i < people.Length; i++)
                    {
                        if (people[i] == 0)
                        {
                            continue;
                        }

                        // SS: set one to the avg, the other one to the difference
                        double v1 = people[idx];
                        double v2 = people[i];
                        
                        // SS: both must have opposite signs, otherwise they cannot (partially)
                        // cancel ech other
                        if (v1 == v2 || v1 * v2 > 0)
                        {
                            continue;
                        }
                        
                        // SS: avg is 0
                        people[idx] = 0;
                        people[i] = v1 + v2;

                        int zeroCount = 1;
                        if (Math.Abs(v1 + v2) < 1E-10)
                        {
                            people[i] = 0;
                            zeroCount++;
                        }

                        nZeros += zeroCount;
                        int nChecks = Solve(0, 0);
                        if (nChecks < int.MaxValue)
                        {
                            nChecks++;
                        }

                        // SS: backtracking
                        nZeros -= zeroCount;
                        people[i] = v2;
                        people[idx] = v1;

                        minChecks = Math.Min(minChecks, nChecks);
                    }
                }

                return minChecks;
            }

            int minChecks = Solve(0, 0);            
            return minChecks < int.MaxValue ? minChecks : 0;
        }

        private static int SolveGreedily(double[] people)
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
            [TestCase(new[] { 5.0, 3, 1, -3, -6 }, 3)]
            [TestCase(new[] { -6.0, -2, -2, 3, 3, 4 }, 4)]
            [TestCase(new[] { 5.0, 5, 5, 5, 5 }, 0)]
            [TestCase(new[] { 5.0 }, 0)]
            public void Test(double[] people, int expectedResult)
            {
                // Arrange

                // Act
                int nChecks = new Solution2().Solve(people);

                // Assert
                Assert.AreEqual(expectedResult, nChecks);
            }

        }
    }
}
