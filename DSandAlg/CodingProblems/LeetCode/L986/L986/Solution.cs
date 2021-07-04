#region

using System;
using System.Collections.Generic;
using NUnit.Framework;

#endregion

// Problem: 986. Interval List Intersections
// URL: https://leetcode.com/problems/interval-list-intersections/

namespace LeetCode
{
    public class Solution
    {
        public int[][] IntervalIntersection(int[][] firstList, int[][] secondList)
        {
            // return IntervalIntersection1(firstList, secondList);
            return IntervalIntersection2(firstList, secondList);
        }

        private int[][] IntervalIntersection2(int[][] firstList, int[][] secondList)
        {
            // SS: runtime complexity: O(max(n1, n2))
            // space complexity: O(max(n1, n2))
            
            var intersection = new List<int[]>();

            int idx1 = 0;
            int max1Idx = firstList.Length * 2;

            int idx2 = 0;
            int max2Idx = secondList.Length * 2;

            var stack = new Stack<(int type, int value)>();

            while (idx1 < max1Idx && idx2 < max2Idx)
            {
                int state1 = idx1 % 2;
                int v1 = firstList[idx1 / 2][state1];

                int state2 = idx2 % 2;
                int v2 = secondList[idx2 / 2][state2];

                bool push1 = false;
                bool push2 = false;
                bool interval1 = false;
                bool interval2 = false;

                if (v1 < v2)
                {
                    if (state1 == 0)
                    {
                        // SS: start if interval
                        push1 = true;
                    }
                    else
                    {
                        // SS: if there is only 1 item on the stack,
                        // there cannot be an overlap...
                        if (stack.Count > 1)
                        {
                            interval1 = true;
                        }
                        else
                        {
                            stack.Pop();
                            idx1++;
                        }
                    }
                }
                else if (v2 < v1)
                {
                    if (state2 == 0)
                    {
                        // SS: start if interval
                        push2 = true;
                    }
                    else
                    {
                        // SS: if there is only 1 item on the stack,
                        // there cannot be an overlap...
                        if (stack.Count > 1)
                        {
                            interval2 = true;
                        }
                        else
                        {
                            stack.Pop();
                            idx2++;
                        }
                    }
                }
                else
                {
                    // SS: trie, push open first
                    if (state1 == 0)
                    {
                        // SS: start if interval
                        push1 = true;
                    }
                    else if (state2 == 0)
                    {
                        // SS: start if interval
                        push2 = true;
                    }
                    else
                    {
                        // SS: end interval, can use either 1 or 2
                        interval1 = true;
                    }
                }

                if (push1)
                {
                    stack.Push((state1, v1));
                    idx1++;
                }
                else if (interval1)
                {
                    // SS: the top item in the stack MUST be the start of an interval
                    (int s, int v) = stack.Pop();
                    intersection.Add(new[] { v, v1 });
                    idx1++;
                }
                else if (push2)
                {
                    stack.Push((state2, v2));
                    idx2++;
                }
                else if (interval2)
                {
                    // SS: the top item in the stack MUST be the start of an interval
                    (int s, int v) = stack.Pop();
                    intersection.Add(new[] { v, v2 });
                    idx2++;
                }
            }

            return intersection.ToArray();
        }

        private int[][] IntervalIntersection1(int[][] firstList, int[][] secondList)
        {
            // SS: Intersect intervals
            // runtime complexity: O(max(n1, n2))
            // space complexity: O(n1 + n2)
            var intersection = new List<int[]>();

            var serialized = new List<(int state, int value)>();

            var l1State = 0;
            var l2State = 0;

            var l1Idx = 0;
            var l2Idx = 0;

            while (l1Idx < firstList.Length && l2Idx < secondList.Length)
            {
                var v1 = firstList[l1Idx][l1State];
                var v2 = secondList[l2Idx][l2State];

                if (v1 < v2)
                {
                    serialized.Add((l1State, v1));

                    if (l1State == 0)
                    {
                        l1State++;
                    }
                    else
                    {
                        l1State = 0;
                        l1Idx++;
                    }
                }
                else if (v2 < v1)
                {
                    serialized.Add((l2State, v2));

                    if (l2State == 0)
                    {
                        l2State++;
                    }
                    else
                    {
                        l2State = 0;
                        l2Idx++;
                    }
                }
                else
                {
                    // SS: values are equal
                    if (l1State <= l2State)
                    {
                        serialized.Add((l1State, v1));

                        if (l1State == 0)
                        {
                            l1State++;
                        }
                        else
                        {
                            l1State = 0;
                            l1Idx++;
                        }
                    }
                    else if (l2State < l1State)
                    {
                        serialized.Add((l2State, v2));

                        if (l2State == 0)
                        {
                            l2State++;
                        }
                        else
                        {
                            l2State = 0;
                            l2Idx++;
                        }
                    }
                }
            }

            // SS: If one list ends, there can be no more intersections between the two.
            // Is this then needed?
            // while (l1Idx < firstList.Length)
            // {
            //     var v1 = firstList[l1Idx][l1State];
            //
            //     serialized.Add((l1State, v1));
            //
            //     if (l1State == 0)
            //     {
            //         l1State++;
            //     }
            //     else
            //     {
            //         l1State = 0;
            //         l1Idx++;
            //     }
            // }
            //
            // while (l2Idx < secondList.Length)
            // {
            //     var v2 = secondList[l2Idx][l2State];
            //
            //     serialized.Add((l2State, v2));
            //
            //     if (l2State == 0)
            //     {
            //         l2State++;
            //     }
            //     else
            //     {
            //         l2State = 0;
            //         l2Idx++;
            //     }
            // }

            // SS: detect intersections
            var serialized2 = new List<(int state, int value)>();

            var idx1 = 0;
            var idx2 = 0;
            while (idx1 < serialized.Count)
            {
                (var state1, var v1) = serialized[idx1];

                if (state1 == 1)
                {
                    (var state2, var v2) = serialized2[idx2 - 1];
                    if (idx2 > 1)
                    {
                        intersection.Add(new[] { v2, v1 });
                    }

                    serialized2.RemoveAt(idx2 - 1);
                    idx2--;
                }
                else
                {
                    serialized2.Add((state1, v1));
                    idx2++;
                }

                idx1++;
            }

            return intersection.ToArray();
        }

        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1()
            {
                // Arrange
                int[][] firstList = { new[] { 0, 2 }, new[] { 5, 10 }, new[] { 13, 23 }, new[] { 24, 25 } };
                int[][] secondList = { new[] { 1, 5 }, new[] { 8, 12 }, new[] { 15, 24 }, new[] { 25, 26 } };

                // Act
                var result = new Solution().IntervalIntersection(firstList, secondList);

                // Assert
                CollectionAssert.AreEqual(new[] { new[] { 1, 2 }, new[] { 5, 5 }, new[] { 8, 10 }, new[] { 15, 23 }, new[] { 24, 24 }, new[] { 25, 25 } }, result);
            }

            [Test]
            public void Test2()
            {
                // Arrange
                int[][] firstList = { new[] { 1, 3 }, new[] { 5, 9 } };
                var secondList = Array.Empty<int[]>();

                // Act
                var result = new Solution().IntervalIntersection(firstList, secondList);

                // Assert
                CollectionAssert.IsEmpty(result);
            }

            [Test]
            public void Test3()
            {
                // Arrange
                var firstList = Array.Empty<int[]>();
                int[][] secondList = { new[] { 4, 8 }, new[] { 10, 12 } };

                // Act
                var result = new Solution().IntervalIntersection(firstList, secondList);

                // Assert
                CollectionAssert.IsEmpty(result);
            }

            [Test]
            public void Test4()
            {
                // Arrange
                int[][] firstList = { new[] { 1, 7 } };
                int[][] secondList = { new[] { 3, 10 } };

                // Act
                var result = new Solution().IntervalIntersection(firstList, secondList);

                // Assert
                CollectionAssert.AreEqual(new[] { new[] { 3, 7 } }, result);
            }

            [Test]
            public void Test5()
            {
                // Arrange
                int[][] firstList = { new[] { 1, 10 } };
                int[][] secondList = { new[] { 3, 5 }, new[] { 7, 12 } };

                // Act
                var result = new Solution().IntervalIntersection(firstList, secondList);

                // Assert
                CollectionAssert.AreEqual(new[] { new[] { 3, 5 }, new[] { 7, 10 } }, result);
            }

            [Test]
            public void Test6()
            {
                // Arrange
                int[][] firstList = { new[] { 1, 10 } };
                int[][] secondList = { new[] { 10, 12 } };

                // Act
                var result = new Solution().IntervalIntersection(firstList, secondList);

                // Assert
                CollectionAssert.AreEqual(new[] { new[] { 10, 10 } }, result);
            }

            [Test]
            public void Test7()
            {
                // Arrange
                int[][] firstList = { new[] { 3, 10 } };
                int[][] secondList = { new[] { 5, 10 } };

                // Act
                var result = new Solution().IntervalIntersection(firstList, secondList);

                // Assert
                CollectionAssert.AreEqual(new[] { new[] { 5, 10 } }, result);
            }

            [Test]
            public void Test8()
            {
                // Arrange
                int[][] firstList = { new[] { 17, 20 } };
                int[][] secondList = { new[] { 6, 8 }, new[]{12, 14}, new[]{19, 20} };

                // Act
                var result = new Solution().IntervalIntersection(firstList, secondList);

                // Assert
                CollectionAssert.AreEqual(new[] { new[] { 19, 20 } }, result);
            }
            
        }
    }
}