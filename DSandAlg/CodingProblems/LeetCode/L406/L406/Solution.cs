﻿#region

using System.Linq;
using NUnit.Framework;

#endregion

// 406. Queue Reconstruction by Height
// https://leetcode.com/problems/queue-reconstruction-by-height/

namespace L406
{
    public class Solution
    {
        public int[][] ReconstructQueue(int[][] people)
        {
            // SS: runtime complexity: O(N^2)
            // space complexity: O(N)

            if (people.Length == 0)
            {
                return people;
            }

            // SS: sort by number of taller people in front, O(N log N)
            var sortedPeople = people.OrderBy(p => p[1]).ToArray();

            var root = new Node
            {
                Height = sortedPeople[0][0]
                , TallerOrEqualPeopleInFront = sortedPeople[0][1]
            };

            // SS: 0 taller people in front is a special case
            var i = 1;
            while (i < sortedPeople.Length)
            {
                var person = sortedPeople[i];

                var newNode = new Node
                {
                    Height = person[0]
                    , TallerOrEqualPeopleInFront = person[1]
                };

                var node = root;

                var k = person[1];
                var prev = root;

                // SS: skip over k taller people
                while (k > 0)
                {
                    if (node.Height >= person[0])
                    {
                        k--;
                    }

                    prev = node;
                    node = node.Next;
                }

                // SS: check for violations
                while (node != null && node.Height < person[0])
                {
                    prev = node;
                    node = node.Next;
                }

                // SS: the smaller height needs to come first
                if (node == root)
                {
                    root = newNode;
                    newNode.Next = prev;
                }
                else
                {
                    prev.Next = newNode;
                    newNode.Next = node;
                }

                i++;
            }

            var result = new int[people.Length][];
            var node2 = root;
            var i2 = 0;
            while (node2 != null)
            {
                result[i2] = new[] {node2.Height, node2.TallerOrEqualPeopleInFront};
                i2++;
                node2 = node2.Next;
            }

            return result;
        }

        public class Node
        {
            public int Height { get; set; }
            public int TallerOrEqualPeopleInFront { get; set; }
            public Node Next { get; set; }
        }

        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1()
            {
                // Arrange
                int[][] people =
                {
                    new[] {7, 0}
                    , new[] {4, 4}
                    , new[] {7, 1}
                    , new[] {5, 0}
                    , new[] {6, 1}
                    , new[] {5, 2}
                };

                // Act
                var result = new Solution().ReconstructQueue(people);

                // Assert
                CollectionAssert.AreEqual(new[] {new[] {5, 0}, new[] {7, 0}, new[] {5, 2}, new[] {6, 1}, new[] {4, 4}, new[] {7, 1}}, result);
            }

            [Test]
            public void Test2()
            {
                // Arrange
                int[][] people =
                {
                    new[] {8, 1}
                    , new[] {9, 1}
                    , new[] {6, 3}
                    , new[] {10, 0}
                    , new[] {7, 0}
                };

                // Act
                var result = new Solution().ReconstructQueue(people);

                // Assert
                CollectionAssert.AreEqual(new[] {new[] {7, 0}, new[] {10, 0}, new[] {8, 1}, new[] {6, 3}, new[] {9, 1}}, result);
            }

            [Test]
            public void Test3()
            {
                // Arrange
                int[][] people =
                {
                    new[] {1, 0}
                    , new[] {2, 0}
                };

                // Act
                var result = new Solution().ReconstructQueue(people);

                // Assert
                CollectionAssert.AreEqual(new[] {new[] {1, 0}, new[] {2, 0}}, result);
            }
        }
    }
}