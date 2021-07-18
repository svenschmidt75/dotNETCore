
using System;
using System.Collections.Generic;
using LeetCode;
using NUnit.Framework;

namespace JohnsProblem
{
    public class Solution2
    {
        private int SolveGreedily2(double[] people)
        {
            // SS: runtime complexity: O(n^3)
            // Idea: find pairs (v1, v2) where v1 < - and v2 > 0, and prioritize:
            // best: v1 = -v2
            // 2nd best: v1 + v2 = v3 and v3 is contained in the list
            // else
            // We then transfer values != 0 over to a tmp. array
            // and solve the same problem, but smaller.
            
            // SS: shift the average to be 0
            double avg = 0;
            for (int i = 0; i < people.Length; i++)
            {
                avg += people[i];
            }
            avg /= people.Length;

            var dict = new Dictionary<double, int>();
            
            for (int i = 0; i < people.Length; i++)
            {
                people[i] -= avg;
                
                // SS: when close to 0...
                if (Math.Abs(people[i]) < 1E-10)
                {
                    people[i] = 0;
                }

                if (dict.ContainsKey(people[i]))
                {
                    dict[people[i]]++;
                }
                else
                {
                    dict[people[i]] = 1;
                }
            }

            // SS: sort in increasing order
            Array.Sort(people);

            if (people[0] >= 0)
            {
                return 0;
            }
            
            int i3 = 0;
            while (i3 < people.Length && people[i3] < 0)
            {
                i3++;
            }
            
            int nChecks = 0;
            
            while (people.Length > 0)
            {
                bool foundZero  = false;
                bool foundOpposite = false;
                int fo1 = -1;
                int fo2 = -1;
                
                int i1 = 0;
                while (people[i1] < 0 && i1 < i3)
                {
                    double v1 = people[i1];

                    int i2 = i3;
                    while (i2 < people.Length)
                    {
                        double v2 = people[i2];
                        double delta = v1 + v2;

                        if (delta == 0)
                        {
                            // SS: cancel, best case
                            dict[people[i1]]--;
                            if (dict[people[i1]] == 0)
                            {
                                dict.Remove(people[i1]);
                            }
                            people[i1] = 0;

                            dict[people[i2]]--;
                            if (dict[people[i2]] == 0)
                            {
                                dict.Remove(people[i2]);
                            }
                            people[i2] = 0;

                            foundZero = true;

                            nChecks++;
                            
                            break;
                        }
                        
                        if (dict.ContainsKey(-delta))
                        {
                            // SS: we have the opposite value in the list,
                            // next best case

                            if (v1 == -delta || v2 == -delta)
                            {
                                if (dict[-delta] == 1)
                                {
                                    break;
                                }
                            }
                            
                            foundOpposite = true;
                            fo1 = i1;
                            fo2 = i2;
                        }

                        if (foundOpposite == false)
                        {
                            // SS: take any pair
                            fo1 = i1;
                            fo2 = i2;
                        }
                        
                        i2++;
                    }

                    if (foundZero)
                    {
                        fo1 = -1;
                        fo2 = -1;
                        break;
                    }

                    i1++;
                }

                double value = double.MaxValue;

                if (foundZero == false)
                {
                    value = people[fo1] + people[fo2];

                    dict[people[fo1]]--;
                    if (dict[people[fo1]] == 0)
                    {
                        dict.Remove(people[fo1]);
                    }
                    people[fo1] = 0;

                    dict[people[fo2]]--;
                    if (dict[people[fo2]] == 0)
                    {
                        dict.Remove(people[fo2]);
                    }
                    people[fo2] = value;
                
                    if (dict.ContainsKey(value))
                    {
                        dict[value]++;
                    }
                    else
                    {
                        dict[value] = 1;
                    }
                
                    nChecks++;
                }

                // SS: copy values over
                int count = people.Length - 1;
                if (foundZero)
                {
                    count--;
                }

                i3 = -1;
                
                double[] people2 = new double[count];
                int p2 = 0;
                for (int i = 0; i < people.Length; i++)
                {
                    if (people[i] == 0)
                    {
                        continue;                        
                    }
                    
                    if (value < people[i])
                    {
                        people2[p2] = value;
                        people[fo2] = 0;
                        value = double.MaxValue;

                        if (i3 < 0 && people2[p2] > 0)
                        {
                            i3 = p2;
                        }

                        p2++;
                    }

                    people2[p2] = people[i];

                    if (i == fo2)
                    {
                        value = double.MaxValue;
                    }

                    if (i3 < 0 && people2[p2] > 0)
                    {
                        i3 = p2;
                    }

                    p2++;
                }

                people = people2;
            }

            return nChecks;
        }

        public int Solve(double[] people)
        {
            // return SolveGreedily(people);
            // return SolveBruteForce(people);
            return SolveGreedily2(people);
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
            [TestCase(new[] { -13.0, -11, -10, -7, -7, -5, -3, -2, -1, -1, 17, 20, 8, 9, 3, 2, 1 }, 11)]
            [TestCase(new[] { -13.0, -3, -1, -1, 20 }, 4)]
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
