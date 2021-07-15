#region

using System;
using System.Linq;
using NUnit.Framework;

#endregion

namespace JohnsProblem
{
    public class Solution
    {
        public void Solve(double[] people)
        {
            // SS: two-pointer method
            // runtime complexity: O(n)
            // space complexity: O(1)
            // assumptions: people[i] >= 0

            // SS: calculate the average everybody should pay
            double avg = 0;
            for (int p = 0; p < people.Length; p++)
            {
                avg += people[p];
            }
            avg /= people.Length;

            // SS: two-pointer method
            // SS: pointer i points to people that payed less than the average
            // SS: pointer j points to people that payed more than the average
            int i = 0;
            int j = 0;

            while (i < people.Length)
            {
                while (i < people.Length && people[i] >= avg)
                {
                    i++;
                }

                if (i == people.Length)
                {
                    // SS: we are done
                    break;
                }

                while (j < people.Length && people[j] <= avg)
                {
                    j++;
                }

                if (j == people.Length)
                {
                    // SS: we are done
                    break;
                }

                double iAmount = people[i];
                double jAmount = people[j];

                // SS: amount i should pay
                double iGiveAmount = avg - iAmount;

                // SS: amount j should receive
                // cannot exceed what i should pay
                double jReceiveAmount = Math.Min(jAmount - avg, iGiveAmount);

                // SS: i writes j a check
                people[i] += jReceiveAmount;
                people[j] -= jReceiveAmount;
            }
        }

        [TestFixture]
        public class Tests
        {
            [TestCase(new[] {5.0, 7, 13, 2, 15})]
            [TestCase(new[] { 5.0, 5, 5, 5, 5 })]
            [TestCase(new[] { 5.0 })]
            public void Test(double[] people)
            {
                // Arrange

                // Act
                new Solution().Solve(people);

                // Assert
                double avg = people.Sum() / people.Length;
                for (int i = 0; i < people.Length; i++)
                {
                    Assert.That(people[i], Is.EqualTo(avg).Within(1E-8));
                }
            }

        }
    }
}